using System;
using System.Collections.Generic;
using Internal.Scopes.Abstract.Environments;
using Internal.Scopes.Abstract.Options;
using Internal.Services.Options.Common;
using Internal.Services.Options.Implementations;
using Internal.Setup.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Services.Options.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "Options", menuName = OptionRoutes.RootPath)]
    public class Options : SerializedScriptableObject, IOptions, IOptionsSetup
    {
        [SerializeField] private PlatformType _currentPlatform;

        [SerializeField] private Dictionary<PlatformType, OptionsRegistry> _registries;

        public void Setup()
        {
            _registries[_currentPlatform].CacheRegistry();
            _registries[_currentPlatform].AddOptions(new PlatformOptions(_currentPlatform, Application.isMobilePlatform));
        }

        public T GetOptions<T>() where T : class, IOptionsEntry
        {
            var currentEnvironment = _registries[_currentPlatform];

            if (currentEnvironment.TryGetEntry<T>(out var environmentEntry) == true)
                return environmentEntry;

            throw new NullReferenceException();
        }
    }
}