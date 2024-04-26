using System;
using Global.Publisher.Abstract.DataStorages;

namespace Global.Audio.Player.Runtime
{
    
    [Serializable]
    public class VolumeSave
    {
        public float MusicVolume { get; set; } = 0.5f;
        public float SoundVolume { get; set; } = 0.5f;
    }
    
    public class VolumeSaveSerializer : StorageEntrySerializer<VolumeSave>
    {
        public VolumeSaveSerializer() : base("sound", new VolumeSave())
        {
        }
    }
}