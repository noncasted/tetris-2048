using System;
using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Global.Publisher.CrazyGames;
using Global.Publisher.Itch;
using Global.Publisher.Yandex;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Environments;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Services.Options.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Publisher.Setup
{
    [InlineEditor]
    public class PublisherSetup : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private ItchSetup _itch;
        [SerializeField] [CreateSO] private YandexSetup _yandex;
        [SerializeField] [CreateSO] private CrazyGamesSetup _crazyGames;
        
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var platformOptions = utils.Options.GetOptions<PlatformOptions>();

            switch (platformOptions.PlatformType)
            {
                case PlatformType.ItchIO:
                    return _itch.Create(services, utils);
                case PlatformType.Yandex:
                    return _yandex.Create(services, utils);
                case PlatformType.IOS:
                    break;
                case PlatformType.Android:
                    break;
                case PlatformType.CrazyGames:
                    return _crazyGames.Create(services, utils);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return UniTask.CompletedTask;
        }
    }
}