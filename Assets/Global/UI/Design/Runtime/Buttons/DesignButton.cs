using Common.DataTypes.Runtime.Reactive;
using Global.UI.Design.Abstract.Buttons;
using Global.UI.Design.Abstract.Elements;
using Global.UI.Design.Runtime.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Global.UI.Design.Runtime.Buttons
{
    [RequireComponent(typeof(Button))]
    public class DesignButton :
        MonoBehaviour,
        IDesignButton,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler
    {
        [SerializeField] private DesignElement _element;

        private readonly ViewableDelegate _clicked = new();

        private Button _button;
        private bool _isLocked;

        public IViewableDelegate Clicked => _clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _element.SetState(DesignElementState.Idle);
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        private void OnClicked()
        {
            if (_isLocked == true)
                return;

            _clicked.Invoke();
        }

        private void SetState(DesignElementState state)
        {
            _element.SetState(state);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetState(DesignElementState.Hovered);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetState(DesignElementState.Idle);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetState(DesignElementState.Pressed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetState(DesignElementState.Hovered);
        }

        private void OnValidate()
        {
            if (_element != null)
                return;

            _element = GetComponent<DesignElement>();

            if (_element == null)
                _element = gameObject.AddComponent<DesignElement>();
        }
    }
}