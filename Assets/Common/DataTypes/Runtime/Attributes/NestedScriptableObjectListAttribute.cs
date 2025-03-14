﻿#if UNITY_EDITOR
using System.Collections;
using System.IO;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using System.Linq;
#endif
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.DataTypes.Runtime.Attributes
{
    [IncludeMyAttributes]

#if UNITY_EDITOR
    [ListDrawerSettings(
        CustomRemoveElementFunction =
            "@$property.GetAttribute<NestedScriptableObjectListAttribute>().RemoveObject($removeElement, $property)",
        Expanded = true)]
    [ValueDropdown("@$property.GetAttribute<NestedScriptableObjectListAttribute>().GetAllObjectsOfType()",
        FlattenTreeView = true)]
    [OnCollectionChanged("@$property.GetAttribute<NestedScriptableObjectListAttribute>().OnCollectionChange($info)")]
#endif
    public class NestedScriptableObjectListAttribute : Attribute
    {
        private readonly List<ScriptableObject> _create = new();
        private readonly List<Object> _remove = new();

        public Type Type;

        public IReadOnlyList<ScriptableObject> ObjectsToCreate => _create;
        public IReadOnlyList<Object> ObjectsToRemove => _remove;

        public void OnCreated(ScriptableObject created)
        {
            _create.Remove(created);
        }

        public void OnRemoved(Object removed)
        {
            _remove.Remove(removed);
        }

#if UNITY_EDITOR
        protected void RemoveObject(Object objectToRemove, InspectorProperty property)
        {
            _remove.Add(objectToRemove);
        }

        protected IEnumerable GetAllObjectsOfType()
        {
            var items =
                AssetDatabase.FindAssets("t:Monoscript", new[] { "Assets/Features" })
                    .Select(x => AssetDatabase.GUIDToAssetPath(x))
                    .Where(x => IsCorrectType(AssetDatabase.LoadAssetAtPath<MonoScript>(x)))
                    .Select(x => new ValueDropdownItem(Path.GetFileName(x),
                        ScriptableObject.CreateInstance(AssetDatabase.LoadAssetAtPath<MonoScript>(x).GetClass())));

            var allObjectsOfType = items.ToList();

            return allObjectsOfType;
        }

        protected bool IsCorrectType(MonoScript script)
        {
            if (script != null)
            {
                var scriptType = script.GetClass();
                if (scriptType != null && (scriptType.Equals(Type) || scriptType.IsSubclassOf(Type)) &&
                    !scriptType.IsAbstract) return true;
            }

            return false;
        }

        protected void OnCollectionChange(CollectionChangeInfo info)
        {
            if (info.ChangeType != CollectionChangeType.Add)
                return;

            if (info.Value is not ScriptableObject scriptableObject)
                return;

            if (scriptableObject.name.Contains("EmptyEntry"))
            {
                _create.Add(null);
                return;
            }

            _create.Add(scriptableObject);
        }
#endif
    }
}