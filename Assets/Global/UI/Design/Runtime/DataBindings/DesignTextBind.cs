using System;
using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;
using TMPro;
using UnityEngine;

namespace Global.UI.Design.Runtime.DataBindings
{
    [DisallowMultipleComponent]
    public class DesignTextBind : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void Construct<T>(IReadOnlyLifetime lifetime, IViewableProperty<T> property, Func<T, string> converter)
        {
            property.View(lifetime, OnValueChanged);

            return;

            void OnValueChanged(T value)
            {
                var text = converter.Invoke(value);
                _text.text = text;
            }
        }

        private void OnValidate()
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();
        }
    }
}