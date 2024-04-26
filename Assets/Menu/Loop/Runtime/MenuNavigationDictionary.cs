using System;
using Common.DataTypes.Runtime.Collections;
using Global.UI.Design.Runtime.Buttons;
using Menu.Loop.Abstract;

namespace Menu.Loop.Runtime
{
    [Serializable]
    public class MenuNavigationDictionary : SerializableDictionary<DesignButton, MenuNavigationTarget>
    {
        
    }
}