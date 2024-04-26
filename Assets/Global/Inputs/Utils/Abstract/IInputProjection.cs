using UnityEngine;

namespace Global.Inputs.Utils.Abstract
{
    public interface IInputProjection
    {
        float GetAngleFrom(Vector3 from);
        Vector3 GetDirectionFrom(Vector3 from);
    }
}