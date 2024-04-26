using System;

namespace Global.Publisher.Abstract.Callbacks
{
    public interface IJsErrorCallback
    {
        event Action<string> Exception; 
    }
}