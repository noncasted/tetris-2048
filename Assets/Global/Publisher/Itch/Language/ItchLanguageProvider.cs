using Global.Publisher.Abstract.Languages;

namespace Global.Publisher.Itch.Language
{
    public class ItchLanguageProvider : ISystemLanguageProvider
    {
        public ItchLanguageProvider(IItchLanguageAPI api)
        {
            _externAPI = api;
        }

        private readonly IItchLanguageAPI _externAPI;

        private bool _isLanguageReceived;
        private UI.Localizations.Definition.Language _selected;

        public UI.Localizations.Definition.Language GetLanguage()
        {
            if (_isLanguageReceived == true)
                return _selected;

            var raw = _externAPI.GetLanguage_Internal();
            _isLanguageReceived = true;

            return raw switch
            {
                "ru" => UI.Localizations.Definition.Language.Ru,
                "en" => UI.Localizations.Definition.Language.Eng,
                _ => UI.Localizations.Definition.Language.Ru
            };
        }
    }
}