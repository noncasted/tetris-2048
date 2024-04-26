using Common.DataTypes.Runtime.Reactive;
using Global.UI.Design.Abstract.CheckBoxes;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Global.UI.Design.Runtime.CheckBoxes
{
    [DisallowMultipleComponent]
    public class DesignCheckBoxMark : DesignCheckBoxBehaviour
    {
        [SerializeField] private GameObject _markObject;

        public override void Construct(IDesignCheckBox checkBox, IReadOnlyLifetime lifetime)
        {
            checkBox.IsChecked.View(lifetime, OnStateChanged);
        }

        private void OnStateChanged(bool isChecked)
        {
            _markObject.SetActive(isChecked);
        }
    }
}