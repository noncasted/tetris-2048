using Global.UI.StateMachines.Abstract;
using Menu.Common;
using Menu.Main.Abstract;
using UnityEngine;

namespace Menu.Main.Runtime
{
    public class MenuMain : MonoBehaviour, IMenuMain
    {
        [SerializeField] private TranslucentImageUpdater _translucentImage;
        
        public IUIConstraints Constraints => new UIConstraints();
        public string Name => "Main";
        
        public void Enter(IStateHandle handle)
        {
            _translucentImage.Hide();
        }
    }
}