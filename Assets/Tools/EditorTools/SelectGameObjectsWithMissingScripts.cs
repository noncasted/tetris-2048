using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.EditorTools
{
    public class SelectGameObjectsWithMissingScripts : Editor
    {
        [MenuItem("Tools/Select missing references")]
        private static void SelectGameObjects()
        {
            var countLoaded = SceneManager.sceneCount;
            var loadedScenes = new Scene[countLoaded];

            for (var i = 0; i < countLoaded; i++)
                loadedScenes[i] = SceneManager.GetSceneAt(i);

            foreach (var scene in loadedScenes)
            {
                var rootObjects = scene.GetRootGameObjects();

                foreach (var g in rootObjects)
                {
                    for (var i = 0; i < g.transform.childCount; i++)
                    {
                        var childTransform = g.transform.GetChild(i);
                        var child = childTransform.gameObject;
                        ProcessObject(child);
                    }

                    ProcessObject(g);
                }

                Debug.Log($"Scene: {scene.name} checked");
            }
        }

        private static void ProcessObject(GameObject gameObject)
        {
            var components = gameObject.GetComponents<Component>();

            foreach (var currentComponent in components)
            {
                Debug.Log($"object: {gameObject.name} checked");

                if (currentComponent != null)
                    continue;

                Selection.activeGameObject = gameObject;
                Debug.Log(gameObject + " has a missing script!");
            }
        }
    }
}