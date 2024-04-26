﻿using Global.UI.Design.Runtime.Buttons;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Global.UI.Design.Runtime.Elements
{
    [DisallowMultipleComponent]
    public class DesignTextColorSetter : MonoBehaviour
    {
        [SerializeField] private DesignColor _color;
        [SerializeField] private TMP_Text _text;

        [Button("Apply")]
        private void OnValidate()
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();

            _text.color = _color.Color;
        }
    }
}