using Cysharp.Threading.Tasks;
using Global.UI.Localizations.Texts;
using Global.UI.Overlays.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.UI.Overlays.Runtime
{
    [InlineEditor]
    public class GlobalOverlayFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private SceneData _scene;
        [SerializeField] private LanguageTextData _localization;
        
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            // var result = await utils.SceneLoader.LoadTyped<GlobalOverlayView>(_scene);
            //
            // var view = result.Searched;

            services.Register<GlobalExceptionController>()
//                .WithParameter(view.ExceptionView)
                .WithParameter(_localization)
                .As<IGlobalExceptionController>();
            
            return UniTask.CompletedTask;
        }
    }
}