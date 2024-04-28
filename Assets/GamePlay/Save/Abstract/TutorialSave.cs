using System;
using Global.Publisher.Abstract.DataStorages;

namespace GamePlay.Save.Abstract
{
    [Serializable]
    public class TutorialSave
    {
        public bool IsTutorialPassed { get; set; }
    }

    public class TutorialSaveSerializer : StorageEntrySerializer<TutorialSave>
    {
        public TutorialSaveSerializer() : base("tutorial", new TutorialSave())
        {
        }
    }
}