using UnityEngine;

namespace Global.Inputs.Utils.Abstract
{
    public interface IInputConversion
    {
        Vector2 ScreenToWorld(Vector2 position);
    }
}