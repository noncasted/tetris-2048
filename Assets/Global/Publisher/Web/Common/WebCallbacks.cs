using System;
using Global.Publisher.Abstract.Callbacks;
using UnityEngine;

namespace Global.Publisher.Web.Common
{
    [DisallowMultipleComponent]
    public class WebCallbacks : MonoBehaviour, IJsErrorCallback
    {
        public event Action<string> Exception; 
        
        public void OnException(string exception)
        {
            Exception?.Invoke(exception);
        }
    }
}