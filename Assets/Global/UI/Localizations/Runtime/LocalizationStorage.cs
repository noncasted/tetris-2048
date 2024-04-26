using System.Collections.Generic;
using Common.DataTypes.Runtime.Attributes;
using Global.UI.Localizations.Texts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Localizations.Runtime
{
    [InlineEditor]
    public class LocalizationStorage : ScriptableObject, ILocalizationStorage
    {
        [SerializeField] [CreateSO] private LanguageTextDataRegistry _registry;

        public IReadOnlyList<LanguageTextData> GetDatas()
        {
            return _registry.Objects;
        }
    }
}