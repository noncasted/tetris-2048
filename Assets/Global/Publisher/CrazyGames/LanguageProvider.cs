using Global.Publisher.Abstract.Languages;

namespace Global.Publisher.CrazyGames
{
    public class LanguageProvider : ISystemLanguageProvider
    {
        public Language GetLanguage()
        {
            return Language.Eng;
        }
    }
}