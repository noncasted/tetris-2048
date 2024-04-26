using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.DataStorages;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Newtonsoft.Json;
using UnityEngine;

namespace Global.Publisher.Web.DataStorages
{
    public class WebDataStorage : IDataStorage, IScopeLifetimeListener
    {
        public WebDataStorage(IReadOnlyList<IStorageEntrySerializer> entries)
        {
            foreach (var entry in entries)
            {
                _typeToSerializer.Add(entry.ValueType, entry);
                _keyToSerializer.Add(entry.SaveKey, entry);
            }
        }

        private const string Key = "save";
        private readonly Dictionary<Type, IStorageEntrySerializer> _typeToSerializer = new();
        private readonly Dictionary<string, IStorageEntrySerializer> _keyToSerializer = new();

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            if (PlayerPrefs.HasKey(Key) == true)
            {
                var raw = PlayerPrefs.GetString(Key);
                var rawEntries = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);

                foreach (var (key, rawData) in rawEntries)
                    _keyToSerializer[key].Deserialize(rawData);
            }

            foreach (var (_, serializer) in _keyToSerializer)
                serializer.ReSerialized.Listen(lifetime, OnEntryChanged);
        }

        public UniTask<T> GetEntry<T>() where T : class
        {
            var type = typeof(T);
            var entry = _typeToSerializer[type].Get<T>();

            return UniTask.FromResult(entry);
        }

        public UniTask Save<T>(T data)
        {
            var type = typeof(T);
            _typeToSerializer[type].Set(data);

            var save = new Dictionary<string, string>();

            foreach (var (key, entry) in _keyToSerializer)
            {
                var rawEntry = entry.Serialize();
                save[key] = rawEntry;
            }

            var json = JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(Key, json);

            return UniTask.CompletedTask;
        }

        private void OnEntryChanged(string _0, string _1)
        {
            var save = new Dictionary<string, string>();

            foreach (var (key, entry) in _keyToSerializer)
            {
                var rawEntry = entry.Serialize();
                save[key] = rawEntry;
            }

            var json = JsonConvert.SerializeObject(save);
            PlayerPrefs.SetString(Key, json);
        }
    }
}