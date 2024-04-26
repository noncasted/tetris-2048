using System;
using Common.DataTypes.Runtime.Reactive;
using Newtonsoft.Json;

namespace Global.Publisher.Abstract.DataStorages
{
    public class StorageEntrySerializer<T> : IStorageEntrySerializer
    {
        public StorageEntrySerializer(string key, T value)
        {
            _value = value;
            SaveKey = key;
            ValueType = typeof(T);
        }

        private readonly ViewableDelegate<string, string> _reSerialized = new();

        private T _value;

        public string SaveKey { get; }
        public Type ValueType { get; }
        public IViewableDelegate<string, string> ReSerialized => _reSerialized;

        public void Save()
        {
            var raw = JsonConvert.SerializeObject(_value);

            _reSerialized.Invoke(SaveKey, raw);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(_value);
        }

        public void Deserialize(string raw)
        {
            _value = JsonConvert.DeserializeObject<T>(raw);
        }

        public void Set<TData>(TData data)
        {
            if (data is not T value)
                throw new Exception();

            _value = value;
        }

        public TData Get<TData>()
        {
            if (_value is not TData value)
                throw new Exception();
            
            return value;
        }
    }
}