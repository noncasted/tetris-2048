using System;
using Global.Publisher.Abstract.Callbacks;
using UnityEngine;

namespace Global.Publisher.Itch.Common
{
    [DisallowMultipleComponent]
    public class ItchCallbacks : MonoBehaviour, IJsErrorCallback
    {
        public event Action<string> Exception; 
        
        public void OnException(string exception)
        {
            Exception?.Invoke(exception);
        }
    }
}