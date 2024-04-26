using Common.Tools.SceneServices;
using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace GamePlay.Overlay.Runtime
{
    [DisallowMultipleComponent]
    public class OverlaySceneServicesFactory : SceneServicesFactory
    {
        protected override void CollectServices(IServiceCollection services)
        {
        }
    }
}