using System;
using Global.Publisher.Abstract.DataStorages;

namespace GamePlay.Save.Abstract
{
    [Serializable]
    public class TutorialSave
    {
        public TutorialSave()
        {
            IsTutorialPassed = false;
        }

        public TutorialSave(bool isTutorialPassed)
        {
            IsTutorialPassed = isTutorialPassed;
        }

        public readonly bool IsTutorialPassed;
    }

    public class TutorialSaveSerializer : StorageEntrySerializer<TutorialSave>
    {
        public TutorialSaveSerializer() : base("tutorial", new TutorialSave())
        {
        }
    }
}