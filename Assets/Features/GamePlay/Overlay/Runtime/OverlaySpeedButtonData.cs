using System;
using Global.UI.Design.Runtime.Buttons;
using UnityEngine;

namespace Features.GamePlay.Overlay.Runtime
{
    [Serializable]
    public class OverlaySpeedButtonData
    {
        [SerializeField] private DesignButton _button;
        [SerializeField] private Transform _target;

        public DesignButton Button => _button;
        public Transform Target => _target;
    }
}