using Tools.AssembliesViewer.Graph.View.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.View.Runtime
{
    [DisallowMultipleComponent]
    public class AssemblyGraphView : MonoBehaviour, IAssemblyGraphView
    {
     //   [SerializeField] private UIBlock2D _block;

        public float Scale => transform.localScale.x;
        public Vector2 Position => Vector2.zero; //=> _block.Position.Value;
        
        public void SetPosition(Vector2 position)
        {
      //      _block.Position.Value = position;
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;
        }
    }
}