using System;
using Common.DataTypes.Runtime.Attributes;
using Common.DataTypes.Runtime.Reactive;
using Global.UI.Localizations.Abstract;
using Global.UI.Localizations.Definition;
using Internal.Scopes.Abstract.Options;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Localizations.Texts
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "LanguageText", menuName = "UI/Localization/Text")]
    public class LanguageTextData : LocalizationEntry
    {
        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _eng;

        [SerializeField] [NestedScriptableObjectField] [Indent]
        private LanguageEntry _ru;
        
        private readonly ViewableProperty<string> _text = new();

        public override IViewableProperty<string> Text => _text;
        
        public override void Construct(IOptions options)
        {
            SetLanguage(Language.Eng);
        }

        public override void SetLanguage(Language language)
        {
            var text = language switch
            {
                Language.Ru => _ru.Text,
                Language.Eng => _eng.Text,
                _ => throw new ArgumentOutOfRangeException()
            };

            _text.Set(text);
        }
    }
}