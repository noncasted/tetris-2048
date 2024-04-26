using Tools.AssembliesViewer.Common;
using Tools.AssembliesViewer.Graph.Connections.Runtime;
using Tools.AssembliesViewer.Graph.Nodes.Runtime;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime
{
    [CreateAssetMenu(fileName = "NodeFactoryConfig", menuName = AssemblyViewerRoutes.Root + "Config/NodeFactory")]
    public class NodeFactoryConfig : ScriptableObject
    {
        [SerializeField] private AssemblyNodeView _prefab;
        [SerializeField] private NodeConnection _connectionPrefab;

        public AssemblyNodeView Prefab => _prefab;
        public NodeConnection ConnectionPrefab => _connectionPrefab;
    }
}