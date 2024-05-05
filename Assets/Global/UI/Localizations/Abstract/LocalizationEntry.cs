using Common.DataTypes.Runtime.Reactive;
using Global.UI.Localizations.Definition;
using Internal.Scopes.Abstract.Options;
using UnityEngine;

namespace Global.UI.Localizations.Abstract
{
    public abstract class LocalizationEntry : ScriptableObject
    {
        public abstract void Construct(IOptions options);
        public abstract IViewableProperty<string> Text { get; }

        public abstract void SetLanguage(Language language);
    }
}