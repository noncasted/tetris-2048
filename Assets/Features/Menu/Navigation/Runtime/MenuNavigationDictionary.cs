using System;
using Common.DataTypes.Runtime.Collections;
using Features.Menu.Navigation.Abstract;
using Global.UI.Design.Runtime.Buttons;

namespace Features.Menu.Navigation.Runtime
{
    [Serializable]
    public class MenuNavigationDictionary : SerializableDictionary<DesignButton, MenuNavigationTarget>
    {
        
    }
}