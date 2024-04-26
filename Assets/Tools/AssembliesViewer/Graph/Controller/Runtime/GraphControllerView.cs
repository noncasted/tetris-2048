using Sirenix.OdinInspector;
using Tools.AssembliesViewer.Graph.Controller.Abstract;
using UnityEngine;
using VContainer;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime
{
    [DisallowMultipleComponent]
    public class GraphControllerView : MonoBehaviour
    {
        private IGraphControllerInterceptor _interceptor;

        [Inject]
        private void Construct(IGraphControllerInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        [Button]
        private void SavePositions()
        {
            _interceptor.SavePositions();
        }
        
        [Button]
        private void Redraw()
        {
            _interceptor.Redraw();
        }
    }
}