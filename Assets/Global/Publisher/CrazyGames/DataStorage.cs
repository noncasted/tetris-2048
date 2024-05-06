using System;
using System.Collections.Generic;
using CrazyGames;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.DataStorages;
using Internal.Scopes.Abstract.Lifetimes;
using Newtonsoft.Json;
using UnityEngine;

namespace Global.Publisher.CrazyGames
{
    public class DataStorage : IDataStorage
    {
        public DataStorage(IReadOnlyList<IStorageEntrySerializer> entries)
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
                var raw = CrazySDK.Data.GetString(Key, string.Empty);
                var rawEntries = JsonConvert.DeserializeObject<Dictionary<string, string>>(raw);

                foreach (var (key, rawData) in rawEntries)
                    _keyToSerializer[key].Deserialize(rawData);
            }
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
            CrazySDK.Data.SetString(Key, json);

            return UniTask.CompletedTask;
        }
    }
}