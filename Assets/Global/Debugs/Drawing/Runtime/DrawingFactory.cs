using Cysharp.Threading.Tasks;
using Global.Debugs.Drawing.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Debugs.Drawing.Runtime
{
    [InlineEditor]
    public class DrawingFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<ShapeDrawer>()
                .As<IShapeDrawer>();
            
            return UniTask.CompletedTask;
        }
    }
}