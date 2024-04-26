﻿using System;
using Global.Publisher.Abstract.DataStorages;

namespace Global.Publisher.Abstract.Advertisment
{
    [Serializable]
    public class AdsSave
    {
        public bool IsDisabled { get; set; } = false;
    }

    public class AdsSaveSerializer : StorageEntrySerializer<AdsSave>
    {
        public AdsSaveSerializer() : base("ads", new AdsSave())
        {
        }
    }
}