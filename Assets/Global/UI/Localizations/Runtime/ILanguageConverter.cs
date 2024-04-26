using Global.UI.Localizations.Definition;

namespace Global.UI.Localizations.Runtime
{
    public interface ILanguageConverter
    {
        string ToString(Language language);
    }
}