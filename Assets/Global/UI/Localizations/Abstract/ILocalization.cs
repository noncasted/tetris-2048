using Global.Publisher.Abstract.Languages;

namespace Global.UI.Localizations.Abstract
{
    public interface ILocalization
    {
        Language Language { get; }
        void Set(Language language);
        Language GetNext(Language language);
    }
}