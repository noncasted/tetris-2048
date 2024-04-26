using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tools.AssembliesViewer.Common;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Tree
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GraphTreeSave", menuName = AssemblyViewerRoutes.Root + "Config/TreeSave")]
    public class GraphTreeSave : ScriptableObject
    {
        [SerializeField] private GraphTreeActiveSaveDictionary _groups;
        [SerializeField] private GraphTreeActiveSaveDictionary _assemblies;

        public Dictionary<string, bool> Groups => _groups;  
        public Dictionary<string, bool> Assemblies => _assemblies;  
    }
}