using System.Runtime.InteropServices;
using UnityEngine;

namespace Global.Publisher.Yandex.DataStorages
{
    public class StorageExternAPI : IStorageAPI
    {
        [DllImport("__Internal")]
        private static extern void GetUserData();

        [DllImport("__Internal")]
        private static extern void SaveUserData(string data);

        public void Get_Internal()
        {
            GetUserData();
        }

        public void Set_Internal(string data)
        {
            Debug.Log($"Save internal: {data}");
            SaveUserData(data);
        }
    }
}