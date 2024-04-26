using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tools.AssembliesViewer.Common;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime.Save
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GraphSave", menuName = AssemblyViewerRoutes.Root + "Save")]
    public class GraphSave : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<string, Vector2> _nodesSave;
        [SerializeField] private Dictionary<string, float> _x;
        [SerializeField] private Dictionary<string, float> _y;

        public Dictionary<string, Vector2> NodesSave => _nodesSave;
        public Dictionary<string, float> X => _x;
        public Dictionary<string, float> Y => _y;
    }
}