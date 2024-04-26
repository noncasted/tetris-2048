using System.Collections.Generic;
using Global.Debugs.Drawing.Abstract;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Tools.AssembliesViewer.Domain.Abstract;
using Tools.AssembliesViewer.Graph.Connections.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Runtime.Save;
using Tools.AssembliesViewer.Graph.Nodes.Abstract;
using Tools.AssembliesViewer.Graph.Tree;
using Tools.AssembliesViewer.Services.DomainProvider.Abstract;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime
{
    public class GraphController :
        IGraphControllerInterceptor,
        IScopeLifetimeListener,
        IUpdatable
    {
        public GraphController(
            IUpdater updater,
            IShapeDrawer drawer,
            GraphMover mover,
            GraphNodeFactory nodeFactory,
            GraphSave save,
            GraphTree tree,
            IAssemblyDomain domain)
        {
            _updater = updater;
            _drawer = drawer;
            _mover = mover;
            _nodeFactory = nodeFactory;
            _save = save;
            _tree = tree;
            _domain = domain;
        }

        private readonly IUpdater _updater;
        private readonly IShapeDrawer _drawer;
        private readonly GraphMover _mover;
        private readonly GraphNodeFactory _nodeFactory;
        private readonly GraphSave _save;
        private readonly GraphTree _tree;
        private readonly IAssemblyDomain _domain;

        private readonly Dictionary<IAssembly, IAssemblyNodeView> _nodes = new();
        private readonly List<INodeConnection> _connections = new();
        private readonly Dictionary<IAssembly, List<INodeConnection>> _fromConnections = new();
        private readonly Dictionary<IAssembly, List<INodeConnection>> _toConnections = new();
        private IAssemblyNodeView _selection;

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _mover.Start();

            foreach (var assembly in _domain.Assemblies)
            {
                var node = _nodeFactory.CreateNode(assembly, this);
                _nodes.Add(assembly, node);

                _fromConnections.Add(assembly, new List<INodeConnection>());
                _toConnections.Add(assembly, new List<INodeConnection>());
            }

            foreach (var (assembly, view) in _nodes)
            {
                foreach (var referencedAssembly in assembly.References)
                {
                    if (_nodes.TryGetValue(referencedAssembly, out var referencedNode) == false)
                        continue;

                    var connection = _nodeFactory.CreateConnection(referencedNode, view);
                    _fromConnections[assembly].Add(connection);
                    _toConnections[referencedAssembly].Add(connection);
                    _connections.Add(connection);
                }
            }

            _updater.Add(lifetime, this);
            _tree.Construct(lifetime, _domain.Assemblies, this);

            foreach (var connection in _connections)
                connection.Initialize();

            Redraw();
        }

        public void SavePositions()
        {
            foreach (var (assembly, node) in _nodes)
            {
                var id = assembly.Id;

                _save.X[id] = node.Position.x;
                _save.Y[id] = node.Position.y;
            }
        }

        public void Redraw()
        {
            foreach (var connection in _connections)
                connection.Draw(_drawer);
        }

        public void EnableAssembly(IAssembly assembly)
        {
            _nodes[assembly].Enable();

            foreach (var connection in _connections)
                connection.Draw(_drawer);
        }

        public void Select(IAssemblyNodeView view)
        {
            if (_selection == view)
                return;

            _selection?.ResetSelection();

            _selection = view;
            _mover.OnSelected(view);
            SavePositions();
        }

        public void DisableAssembly(IAssembly assembly)
        {
            _nodes[assembly].Disable();

            foreach (var connection in _connections)
                connection.Draw(_drawer);
        }

        public void OnUpdate(float delta)
        {
            if (_selection != null && _selection.IsDirty)
            {
                foreach (var connection in _fromConnections[_selection.Assembly])
                    connection.Draw(_drawer);
                
                foreach (var connection in _toConnections[_selection.Assembly])
                    connection.Draw(_drawer);
            }
        }
    }
}