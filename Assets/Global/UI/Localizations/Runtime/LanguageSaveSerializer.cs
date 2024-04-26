using System;
using Global.Publisher.Abstract.DataStorages;
using Global.UI.Localizations.Definition;

namespace Global.UI.Localizations.Runtime
{
    [Serializable]
    public class LanguageSave
    {
        public bool IsOverriden { get; set; } = false;
        public Language Language { get; set; } = Language.Ru;
    }

    public class LanguageSaveSerializer : StorageEntrySerializer<LanguageSave>
    {
        public LanguageSaveSerializer() : base("language", new LanguageSave())
        {
        }
    }
}