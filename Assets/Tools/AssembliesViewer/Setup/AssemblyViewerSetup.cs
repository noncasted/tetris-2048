using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Common.Mocks.Runtime;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Common;
using Tools.AssembliesViewer.Graph.Controller.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Runtime;
using Tools.AssembliesViewer.Graph.Controller.Runtime.Inputs;
using Tools.AssembliesViewer.Graph.Controller.Runtime.Save;
using Tools.AssembliesViewer.Graph.Tree;
using Tools.AssembliesViewer.Graph.View.Abstract;
using Tools.AssembliesViewer.Graph.View.Runtime;
using Tools.AssembliesViewer.Services.DomainProvider.Abstract;
using Tools.AssembliesViewer.Services.DomainProvider.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tools.AssembliesViewer.Setup
{
    [DisallowMultipleComponent]
    public class AssemblyViewerSetup : MockBase, IServiceScopeConfig, IServiceFactory
    {
        [SerializeField] private AssemblyViewerScope _scopePrefab;
        [SerializeField] private SceneData _servicesScene;

        [SerializeField] private AssemblyGraphView _view;
        [SerializeField] private NodesRootView _nodesRoot;
        [SerializeField] private GraphMover _mover;
        [SerializeField] private GraphMoverConfig _moverConfig;
        [SerializeField] private NodeFactoryConfig _factoryConfig;
        [SerializeField] private GraphSave _save;
        [SerializeField] private GraphControllerView _controllerView;
        [SerializeField] private GraphTree _tree;
        [SerializeField] private Camera _camera;
        
        public LifetimeScope ScopePrefab => _scopePrefab;
        public SceneData ServicesScene => _servicesScene;
        public bool IsMock => true;
        public IReadOnlyList<IServiceFactory> Services => new[] { this };
        public IReadOnlyList<IServicesCompose> Composes => ArraySegment<IServicesCompose>.Empty;

        public override async UniTaskVoid Process()
        {
            var result = await BootstrapGlobal();
            var resolver = result.Scope.Container;
            
            var scopeLoaderFactory = resolver.Resolve<IServiceScopeLoader>();
            var scope = await scopeLoaderFactory.Load(result.Scope, this);
            
            await scope.Callbacks.RunConstruct();
        }

        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var domainConstructor = new DomainConstructor();
            var assemblies = domainConstructor.Construct();

            utils.AddServiceCallbacks();

            services.Inject(_controllerView);
            
            services.Register<AssemblyDomain>()
                .WithParameter(assemblies)
                .As<IAssemblyDomain>()
                .AsCallbackListener();

            services.RegisterComponent(_view)
                .As<IAssemblyGraphView>();

            services.RegisterComponent(_nodesRoot)
                .As<INodesRootView>();

            services.Register<GraphInput>()
                .As<IGraphInput>()
                .AsCallbackListener();
 
            services.Register<GraphMover>()
                .WithParameter(_moverConfig);

            services.Register<GraphController>()
                .As<IGraphControllerInterceptor>()
                .AsCallbackListener();

            services.RegisterComponent(_save);
            
            services.Register<GraphNodeFactory>()
                .WithParameter(_factoryConfig);

            services.RegisterInstance(_tree);
        }
    }
}