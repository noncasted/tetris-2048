using System;

namespace Global.Publisher.Abstract.DataStorages
{
    public interface IStorageEntrySerializer
    {
        string SaveKey { get; }
        Type ValueType { get; }

        string Serialize();
        void Deserialize(string raw);

        void Set<T>(T data);
        T Get<T>();
    }
}