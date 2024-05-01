using System;
using System.Collections.Generic;
using Global.Publisher.Abstract.DataStorages;

namespace Global.Saves
{
    [Serializable]
    public class PurchasesSave
    {
        public List<string> Purchases { get; } = new List<string>();
    }
    
    public class PurchasesSaveSerializer : StorageEntrySerializer<PurchasesSave>
    {
        public PurchasesSaveSerializer() : base("purchases", new PurchasesSave())
        {
        }
    }
}