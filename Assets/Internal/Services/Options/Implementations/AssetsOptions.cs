﻿using Internal.Scopes.Abstract.Options;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Services.Options.Implementations
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "Options_Assets", menuName = "Internal/Options/Assets")]
    public class AssetsOptions : OptionsEntry
    {
        [SerializeField] private bool _useAddressables;

        public bool UseAddressables => _useAddressables;
    }
}