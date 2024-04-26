using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ServiceScopeBinder : IServiceScopeBinder
    {
        private readonly Scene _scene;

        public ServiceScopeBinder(Scene scene)
        {
            _scene = scene;
        }

        public void MoveToModules(MonoBehaviour service)
        {
            SceneManager.MoveGameObjectToScene(service.gameObject, _scene);
        }

        public void MoveToModules(GameObject gameObject)
        {
            SceneManager.MoveGameObjectToScene(gameObject, _scene);
        }

        public void MoveToModules(Transform transform)
        {
            SceneManager.MoveGameObjectToScene(transform.gameObject, _scene);
        }
    }
}