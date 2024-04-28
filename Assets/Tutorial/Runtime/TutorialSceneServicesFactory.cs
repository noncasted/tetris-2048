using Common.Tools.SceneServices;
using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace Tutorial.Runtime
{
    [DisallowMultipleComponent]
    public class TutorialSceneServicesFactory : SceneServicesFactory
    {
        protected override void CollectServices(IServiceCollection services)
        {
        }
    }
}