using Global.Publisher.Abstract.Languages;

namespace Global.UI.Localizations.Runtime
{
    public interface ILanguageConverter
    {
        string ToString(Language language);
    }
}