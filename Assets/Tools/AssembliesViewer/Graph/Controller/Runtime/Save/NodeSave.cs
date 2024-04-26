using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime.Save
{
    [Serializable]
    public class NodeSave
    {
        [SerializeField] public Dictionary<string, Vector2> Value;
    }
}