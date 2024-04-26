using UnityEngine;

namespace Internal.Scopes.Abstract.Scenes
{
    [CreateAssetMenu(fileName = "Scene_", menuName = "Internal/Scene")]
    public class SceneData : ScriptableObject
    {
        [SerializeField] private SceneField _scene;

        public SceneField Scene => _scene;
    }
}