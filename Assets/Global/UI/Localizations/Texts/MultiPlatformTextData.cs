using System;
using Common.DataTypes.Runtime.Attributes;
using Common.DataTypes.Runtime.Reactive;
using Global.Publisher.Abstract.Languages;
using Global.UI.Localizations.Abstract;
using Internal.Scopes.Abstract.Options;
using Internal.Services.Options.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Localizations.Texts
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "PlatformLanguageText", menuName = "UI/Localization/PlatformText")]
    public class MultiPlatformTextData : LocalizationEntry
    {
        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _engMobile;

        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _engDesktop;

        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _ruMobile;

        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _ruDesktop;

        private readonly ViewableProperty<string> _text = new();

        private PlatformOptions _platformOptions;

        public override IViewableProperty<string> Text => _text;

        public override void Construct(IOptions options)
        {
            _platformOptions = options.GetOptions<PlatformOptions>();
        }

        public override void SetLanguage(Language language)
        {
            var entry = GetEntry();
            _text.Set(entry.Text);

            return;

            LanguageEntry GetEntry()
            {
                var isMobile = _platformOptions.IsMobile;

                if (isMobile == true)
                {
                    return language switch
                    {
                        Language.Eng => _engMobile,
                        Language.Ru => _ruMobile,
                        _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
                    };
                }
                else
                {
                    return language switch
                    {
                        Language.Eng => _engDesktop,
                        Language.Ru => _ruDesktop,
                        _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
                    };
                }
            }
        }
    }
}