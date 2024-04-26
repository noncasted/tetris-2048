using UnityEngine;

namespace Global.System.ApplicationProxies.Abstract
{
    public interface IScreen
    {
        ScreenMode ScreenMode { get; }
        Vector2 Resolution { get; }
    }
}