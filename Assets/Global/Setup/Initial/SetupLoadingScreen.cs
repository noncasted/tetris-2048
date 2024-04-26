﻿using UnityEngine;

namespace Global.Setup.Initial
{
    [DisallowMultipleComponent]
    public class SetupLoadingScreen : MonoBehaviour
    {
        [SerializeField] private float _disposeTime = 1f;
        [SerializeField] private float _startTime = 0.2f;
        [SerializeField] private CanvasGroup _group;

        private float _currentTime;
        private bool _isActive;

        public void Dispose()
        {
            _currentTime = -_startTime;
            _isActive = true;
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            _currentTime += Time.deltaTime;
            var progressTime = _currentTime / _disposeTime;
            _group.alpha = 1f - progressTime;
            
            if (progressTime < 1f)
                return;
            
            Destroy(gameObject);
        }
    }
}