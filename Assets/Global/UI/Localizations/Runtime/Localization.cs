using System;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Languages;
using Global.Saves;
using Global.UI.Localizations.Abstract;
using Global.UI.Localizations.Definition;
using Internal.Scopes.Abstract.Instances.Services;

namespace Global.UI.Localizations.Runtime
{
    public class Localization : IScopeEnableAsyncListener, ILocalization
    {
        public Localization(
            ILocalizationStorage storage,
            ISystemLanguageProvider systemLanguageProvider,
            IDataStorage dataStorage)
        {
            _storage = storage;
            _systemLanguageProvider = systemLanguageProvider;
            _dataStorage = dataStorage;
        }

        private readonly ILocalizationStorage _storage;
        private readonly ISystemLanguageProvider _systemLanguageProvider;
        private readonly IDataStorage _dataStorage;

        private Language _language;

        public Language Language => _language;

        public async UniTask OnEnabledAsync()
        {
            var saves = await _dataStorage.GetEntry<LanguageSave>();

            if (saves.IsOverriden == true)
                _language = saves.Language;
            else
                _language = _systemLanguageProvider.GetLanguage();

            var datas = _storage.GetDatas();

            foreach (var data in datas)
                data.SelectLanguage(_language);
        }

        public void Set(Language language)
        {
            _language = language;

            _dataStorage.Save(new LanguageSave()
            {
                IsOverriden = true,
                Language = language
            });

            var datas = _storage.GetDatas();

            foreach (var data in datas)
                data.SelectLanguage(_language);
        }

        public Language GetNext(Language language)
        {
            return language switch
            {
                Language.Ru => Language.Eng,
                Language.Eng => Language.Ru,
                _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
            };
        }
    }
}