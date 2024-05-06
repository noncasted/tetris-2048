using Features.GamePlay.Loop.Scores.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using TMPro;
using UnityEngine;
using VContainer;

namespace Features.GamePlay.Overlay.Runtime
{
    [DisallowMultipleComponent]
    public class OverlayScores : MonoBehaviour, IScopeLifetimeListener
    {
        [SerializeField] private TMP_Text _current;
        [SerializeField] private TMP_Text _max;
        
        private IScore _score;

        [Inject]
        private void Construct(IScore score)
        {
            _score = score;
        }

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _score.Current.View(lifetime, OnCurrentChanged);
            _score.Max.View(lifetime, OnMaxChanged);
        }

        private void OnCurrentChanged(IReadOnlyLifetime _, int current)
        {
            _current.text = current.ToString();
        }
        
        private void OnMaxChanged(IReadOnlyLifetime _, int max)
        {
            _max.text = max.ToString();
        }
    }
}