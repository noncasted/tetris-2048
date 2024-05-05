﻿using Common.Components.Runtime.ObjectLifetime;
using Global.UI.Localizations.Abstract;
using TMPro;
using UnityEngine;

namespace Global.UI.Localizations.Texts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private LocalizationEntry _data;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            var lifetime = this.GetObjectLifetime();
            _data.Text.View(lifetime, (_, text) => OnLanguageChanged(text));
        }

        private void OnLanguageChanged(string text)
        {
            _text.text = text;
        }
    }
}