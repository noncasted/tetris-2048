using System.Collections.Generic;
using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;
using TMPro;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Tree
{
    [DisallowMultipleComponent]
    public class GraphTreeFoldout : MonoBehaviour, IGraphTreeEntry
    {
        [SerializeField] private TMP_Text _groupNameText;

        //[SerializeField] private UIBlock2D _block;
        [SerializeField] private float _baseSize;

        //[SerializeField] private UIButton _expandButton;
        [SerializeField] private TMP_Text _expandText;
        //[SerializeField] private UIBlock _entriesRoot;
        // [SerializeField] private Toggle _toggle;

        private bool _isExpanded;

        private readonly List<IGraphTreeEntry> _entries = new();
        private readonly ViewableProperty<float> _size = new();
        private readonly ViewableProperty<bool> _isToggled = new();

        // public UIBlock2D Block => _block;
        public IViewableProperty<float> Size => _size;
        public IViewableProperty<bool> IsToggled => _isToggled;

        private void OnEnable()
        {
            //  _expandButton.Clicked += OnExpandClicked;
            //_toggle.OnToggled.AddListener(OnToggleClicked);
        }

        private void OnDisable()
        {
            // _expandButton.Clicked -= OnExpandClicked;
            //  _toggle.OnToggled.RemoveListener(OnToggleClicked);
        }

        public void Construct(string groupName, bool isToggled)
        {
            name = groupName;
            _groupNameText.text = groupName;
            // _toggle.ToggledOn = isToggled;
            _isToggled.Set(isToggled);
        }

        public void AddEntry(ILifetime lifetime, IGraphTreeEntry entry)
        {
            entry.Size.View(lifetime, Recalculate);
            //entry.Block.transform.parent = _entriesRoot.transform;
            _entries.Add(entry);

            Recalculate();
        }

        private void Recalculate()
        {
            if (_isExpanded == true)
            {
                _expandText.text = ">";
                //  _entriesRoot.gameObject.SetActive(true);
                var size = 0f;

                foreach (var entry in _entries)
                    size += entry.Size.Value;

                // _entriesRoot.Size.Value = new Vector3(_entriesRoot.Size.Value.x, size, 0f);
                // _block.Size.Value = new Vector3(_block.Size.Value.x, size + _baseSize, 0f);

                _size.Set(size + _baseSize);
            }
            else
            {
                _expandText.text = "v";
                //  _entriesRoot.gameObject.SetActive(false);
                _size.Set(_baseSize);
                // _block.Size.Value = new Vector3(_block.Size.Value.x, _baseSize, 0f);
            }
        }

        private void OnExpandClicked()
        {
            _isExpanded = !_isExpanded;
            Recalculate();
        }

        private void OnToggleClicked(bool value)
        {
            _isToggled.Set(value);
        }
    }
}