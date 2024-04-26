using Global.System.ApplicationProxies.Abstract;
using UnityEngine;

namespace Global.System.ApplicationProxies.Runtime
{
    public class ApplicationProxy : IApplicationFlow, IScreen
    {
        public ApplicationProxy()
        {
        }

        public Vector2 Resolution => new(Screen.width, Screen.height);

        public ScreenMode ScreenMode => GetScreenMode();

        public void Quit()
        {
            Application.Quit();
        }

        private ScreenMode GetScreenMode()
        {
            if (Screen.height > Screen.width)
                return ScreenMode.Vertical;

            return ScreenMode.Horizontal;
        }
    }
}