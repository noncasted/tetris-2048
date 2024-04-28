using System;
using Common.DataTypes.Runtime.Collections;
using Global.UI.Design.Runtime.Buttons;
using Menu.Navigation.Abstract;

namespace Menu.Navigation.Runtime
{
    [Serializable]
    public class MenuNavigationDictionary : SerializableDictionary<DesignButton, MenuNavigationTarget>
    {
        
    }
}