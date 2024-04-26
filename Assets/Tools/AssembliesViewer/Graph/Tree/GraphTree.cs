using System.Collections.Generic;
using Internal.Scopes.Abstract.Lifetimes;
using Tools.AssembliesViewer.Domain.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Tree
{
    [DisallowMultipleComponent]
    public class GraphTree : MonoBehaviour
    {
        [SerializeField] private Transform _entriesRoot;
        [SerializeField] private GraphTreeFoldout _foldoutPrefab;
        [SerializeField] private GraphTreeAssembly _assemblyPrefab;
        [SerializeField] private GraphTreeSave _save;

        private IGraphControllerInterceptor _interceptor;
        private readonly Dictionary<IAssembly, TreeGroup> _assemblyToGroup = new();
        private readonly Dictionary<TreeGroup, IAssembly> _groupToAssembly = new();

        public void Construct(
            ILifetime lifetime,
            IReadOnlyList<IAssembly> assemblies,
            IGraphControllerInterceptor interceptor)
        {
            _interceptor = interceptor;
            var baseGroup = new TreeGroup("", "", 100000000, null);
            baseGroup.SetActive(true);
            
            foreach (var assembly in assemblies)
            {
                _save.Assemblies.TryAdd(assembly.Path.Name, true);
                var group = GetOrCreate(baseGroup, assembly.Path.FullPathName);
                group.Assemblies.Add(assembly);
            }

            foreach (var (_, subGroup) in baseGroup.SubGroups)
                InstantiateGroup(lifetime, subGroup, null);

            foreach (var (_, group) in baseGroup.SubGroups)
                OnGroupToggle(group, group.PathName, group.IsActive);
        }

        private void InstantiateGroup(ILifetime lifetime, TreeGroup group, GraphTreeFoldout root)
        {
            var foldout = Instantiate(_foldoutPrefab, _entriesRoot);
            var isActive = _save.Groups.GetValueOrDefault(group.PathName, true);

            foldout.Construct(group.Name, isActive);
            foldout.IsToggled.View(lifetime, (_, value) => OnGroupToggle(group, group.PathName, value));

            if (root != null)
                root.AddEntry(lifetime, foldout);

            foreach (var (_, subGroup) in group.SubGroups)
                InstantiateGroup(lifetime, subGroup, foldout);

            foreach (var assembly in group.Assemblies)
            {
                var assemblyEntry = Instantiate(_assemblyPrefab, _entriesRoot);
                var isEntryActive = _save.Assemblies.GetValueOrDefault(assembly.Path.Name, true);
                assemblyEntry.Construct(assembly, isEntryActive);
                foldout.AddEntry(lifetime, assemblyEntry);

                _assemblyToGroup.Add(assembly, group);

                assemblyEntry.IsToggled.View(lifetime,
                    (_, value) => OnAssemblyToggle(assembly, assembly.Path.Name, value));
            }
        }

        private TreeGroup GetOrCreate(TreeGroup group, string assemblyName)
        {
            var pathEntries = assemblyName.Split("/");

            for (var i = 0; i < pathEntries.Length - 2; i++)
            {
                var entryName = pathEntries[i];

                if (group.SubGroups.TryGetValue(entryName, out var nextGroup) == false)
                {
                    nextGroup = new TreeGroup(
                        entryName,
                        group.PathName + $"/{entryName}",
                        group.Priority / 10,
                        group);
                    group.SubGroups.Add(entryName, nextGroup);
                    group = nextGroup;
                }
                else
                {
                    group = nextGroup;
                }
            }

            return group;
        }

        private void OnGroupToggle(TreeGroup group, string save, bool value)
        {
            if (ValidateState(value) == false)
                return;

            _save.Groups.TryAdd(save, true);
            _save.Groups[save] = value;
            
            group.SetActive(value);
            
            ToggleGroup(group);

            void ToggleGroup(TreeGroup targetGroup)
            {
                foreach (var (_, subGroup) in targetGroup.SubGroups)
                    ToggleGroup(subGroup);

                foreach (var assembly in targetGroup.Assemblies)
                {
                    var enableAssembly = value;
                    
                    if (_save.Assemblies[assembly.Path.Name] == false)
                        enableAssembly = false;

                    if (enableAssembly == true)
                        _interceptor.EnableAssembly(assembly);
                    else
                        _interceptor.DisableAssembly(assembly);
                }
            }

            bool ValidateState(bool state)
            {
                if (state == false)
                    return true;

                var parentGroup = group.Parent;
                
                while (parentGroup != null)
                {
                    if (parentGroup.IsActive == false)
                        return false;
                    
                    parentGroup = parentGroup.Parent;
                }

                return true;
            }
        }

        private void OnAssemblyToggle(IAssembly assembly, string save, bool value)
        {
            if (_assemblyToGroup[assembly].IsActive == false)
            {
                _interceptor.DisableAssembly(assembly);
                return;
            }

            if (_save.Assemblies[assembly.Path.Name] == false)
                value = false;

            _save.Assemblies[save] = value;

            if (value == true)
                _interceptor.EnableAssembly(assembly);
            else
                _interceptor.DisableAssembly(assembly);
        }

        public class TreeGroup
        {
            public TreeGroup(
                string name,
                string pathName,
                int priority,
                TreeGroup parent)
            {
                Parent = parent;
                Priority = priority;
                PathName = pathName;
                Name = name;
            }

            public readonly string Name;
            public readonly string PathName;
            public readonly int Priority;
            public readonly Dictionary<string, TreeGroup> SubGroups = new();
            public readonly List<IAssembly> Assemblies = new();
            public readonly TreeGroup Parent;

            private bool _isActive;
            private int _counter;

            public bool IsActive => _isActive;
            public int Counter => _counter;

            public void SetActive(bool isActive)
            {
                _isActive = isActive;
            }
        }
    }
}