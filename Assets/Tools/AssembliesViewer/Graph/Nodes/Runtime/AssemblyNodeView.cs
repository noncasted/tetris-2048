using TMPro;
using Tools.AssembliesViewer.Domain.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Abstract;
using Tools.AssembliesViewer.Graph.Nodes.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Nodes.Runtime
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class AssemblyNodeView : MonoBehaviour, IAssemblyNodeView
    {
       // [SerializeField] private UIBlock2D _block;
        [SerializeField] private TMP_Text _text;
        //[SerializeField] private UIButton _selectionButton;
        [SerializeField] private GameObject _selection;
        
        private IAssembly _assembly;
        private IGraphControllerInterceptor _interceptor;
        private bool _isDirty;

       // public UIBlock2D Block => _block;
        public Transform Transform => transform;
        public Vector2 Position => Vector2.down;
        public Vector3 WorldPosition => transform.position;
        
        public bool IsActive => gameObject.activeSelf;
        public bool IsPressed => false; //=> _selectionButton.IsPressed;
        public bool IsDirty => _isDirty;
        public IAssembly Assembly => _assembly;

        public void Enable()
        {
            gameObject.SetActive(true);
            //_selectionButton.Clicked += OnSelectionClicked;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
           // _selectionButton.Clicked -= OnSelectionClicked;
        }

        public void SetPosition(Vector2 position)
        {
            //_block.Position.Value = position;
            _isDirty = true;
        }

        public void OnMoved()
        {
            _isDirty = false;
        }

        public void ResetSelection()
        {
            _selection.SetActive(false);
        }

        public void Construct(IAssembly assembly, IGraphControllerInterceptor interceptor)
        {
            _interceptor = interceptor;
            name = assembly.Path.Name;
            _assembly = assembly;
            _text.text = assembly.Path.Name;
        }

        private void OnSelectionClicked()
        {
            _selection.SetActive(true);
            _interceptor.Select(this);
        }
    }
}