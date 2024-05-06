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
        private Abstract.Languages.Language _selected;

        public Abstract.Languages.Language GetLanguage()
        {
            if (_isLanguageReceived == true)
                return _selected;

            var raw = _externAPI.GetLanguage_Internal();
            _isLanguageReceived = true;

            return raw switch
            {
                "ru" => Abstract.Languages.Language.Ru,
                "en" => Abstract.Languages.Language.Eng,
                _ => Abstract.Languages.Language.Ru
            };
        }
    }
}