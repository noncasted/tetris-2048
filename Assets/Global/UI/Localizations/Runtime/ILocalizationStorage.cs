using System.Collections.Generic;
using Global.UI.Localizations.Texts;

namespace Global.UI.Localizations.Runtime
{
    public interface ILocalizationStorage
    {
        IReadOnlyList<LanguageTextData> GetDatas();
    }
}