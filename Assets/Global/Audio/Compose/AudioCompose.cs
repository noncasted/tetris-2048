using System.Collections.Generic;
using Global.Audio.Listener.Runtime;
using Global.Audio.Player.Runtime;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Audio.Compose
{
    [InlineEditor]
    public class AudioCompose : ScriptableObject, IServicesCompose
    {
        [SerializeField] private GlobalAudioListenerFactory _listener;
        [SerializeField] private GlobalAudioPlayerFactory _player;

        public IReadOnlyList<IServiceFactory> Factories => new IServiceFactory[]
        {
            _listener,
            _player
        };
    }
}