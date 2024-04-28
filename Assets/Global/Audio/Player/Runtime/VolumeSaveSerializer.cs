﻿using System;
using Global.Publisher.Abstract.DataStorages;

namespace Global.Audio.Player.Runtime
{
    
    [Serializable]
    public class VolumeSave
    {
        public float MusicVolume { get; set; } = 3;
        public float SoundVolume { get; set; } = 3;
    }
    
    public class VolumeSaveSerializer : StorageEntrySerializer<VolumeSave>
    {
        public VolumeSaveSerializer() : base("sound", new VolumeSave())
        {
        }
    }
}