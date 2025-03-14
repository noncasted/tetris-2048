﻿using Common.DataTypes.Runtime.Reactive;
using Global.UI.Design.Runtime.Buttons;
using Internal.Services.Options.Implementations;
using UnityEngine;

namespace Features.Tutorial.Runtime.Steps.EndConditions
{
    [DisallowMultipleComponent]
    public class TutorialStep_EndConditions_UI : TutorialStepUI
    {
        [SerializeField] private DesignButton _button;
        [SerializeField] private GameObject _desktop;
        [SerializeField] private GameObject _mobile;

        public IViewableDelegate Clicked => _button.Clicked;

        public void SetPlatform(PlatformOptions options)
        {
            if (options.IsMobile == true)
            {
                _desktop.SetActive(false);
                _mobile.SetActive(true);
            }
            else
            {
                _desktop.SetActive(true);
                _mobile.SetActive(false);
            }
        }
    }
}