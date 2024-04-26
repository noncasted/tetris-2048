using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common.DataTypes.Runtime.Collections
{
    public abstract class ScriptableRegistry<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField] private T[] _objects;

        public IReadOnlyList<T> Objects => _objects;

        [Button]
        private void Scan()
        {
#if UNITY_EDITOR
            var definitions = new List<T>();
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset == null)
                    continue;

                if (IsObjectValid(asset) == false)
                    continue;

                definitions.Add(asset);
            }

            OnRegistryValidation(definitions);

            AssetDatabase.SaveAssets();

            Undo.RecordObject(this, "Assign objects");

            _objects = definitions.ToArray();

            Undo.RecordObject(this, "Assign objects");
#endif
        }

        protected virtual void OnRegistryValidation(IReadOnlyList<T> objects)
        {
        }

        protected virtual bool IsObjectValid(T target)
        {
            return true;
        }
    }
}