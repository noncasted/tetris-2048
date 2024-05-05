//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Global/Inputs/View/Config/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""98d3b2ff-9a15-42b1-bc36-b64f77d5f962"",
            ""actions"": [
                {
                    ""name"": ""Swipe"",
                    ""type"": ""Value"",
                    ""id"": ""f0a92838-9cdf-423e-a96a-cece06e79c3f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ae5fc23c-f72c-4480-bd53-74e0ab260846"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f1357352-c0b9-4d32-bc4f-177be3b48be7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6c7c55b7-fa82-43e4-ac4c-6606de9f0931"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b23a55e8-8a29-42d1-b7eb-614747721862"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d5c4c8cd-4e7d-47e5-aa93-228eb054980a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c7d1fcde-ed19-464f-9b2a-9458ace2115e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""25352e39-2e0d-49aa-a094-a4d455470c2e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7d18979f-ce39-4345-945e-587d2fe9bc49"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b5e4d26e-da0e-466c-a6f1-2906986b788e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swipe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""AssemblyGraph"",
            ""id"": ""8f9630b7-87f4-4de0-8531-8972f0d42074"",
            ""actions"": [
                {
                    ""name"": ""RightMouseButton"",
                    ""type"": ""Button"",
                    ""id"": ""d0ac26c1-ed68-47cf-b719-074fb56932b8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftMouseButton"",
                    ""type"": ""Button"",
                    ""id"": ""2ad081e7-9190-47b1-858a-af8672dcf93e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""Value"",
                    ""id"": ""93753e2d-dfb8-4314-8b7e-a8e6a0e7dbfa"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba491d0c-4d25-4ae3-9741-0ea49c1def10"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0af2b41-883f-4e16-8a27-34a56fb9b8b1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c525a9b-8612-448d-9951-668051c4bc48"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Swipe = m_GamePlay.FindAction("Swipe", throwIfNotFound: true);
        // AssemblyGraph
        m_AssemblyGraph = asset.FindActionMap("AssemblyGraph", throwIfNotFound: true);
        m_AssemblyGraph_RightMouseButton = m_AssemblyGraph.FindAction("RightMouseButton", throwIfNotFound: true);
        m_AssemblyGraph_LeftMouseButton = m_AssemblyGraph.FindAction("LeftMouseButton", throwIfNotFound: true);
        m_AssemblyGraph_Scroll = m_AssemblyGraph.FindAction("Scroll", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private List<IGamePlayActions> m_GamePlayActionsCallbackInterfaces = new List<IGamePlayActions>();
    private readonly InputAction m_GamePlay_Swipe;
    public struct GamePlayActions
    {
        private @Controls m_Wrapper;
        public GamePlayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Swipe => m_Wrapper.m_GamePlay_Swipe;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void AddCallbacks(IGamePlayActions instance)
        {
            if (instance == null || m_Wrapper.m_GamePlayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GamePlayActionsCallbackInterfaces.Add(instance);
            @Swipe.started += instance.OnSwipe;
            @Swipe.performed += instance.OnSwipe;
            @Swipe.canceled += instance.OnSwipe;
        }

        private void UnregisterCallbacks(IGamePlayActions instance)
        {
            @Swipe.started -= instance.OnSwipe;
            @Swipe.performed -= instance.OnSwipe;
            @Swipe.canceled -= instance.OnSwipe;
        }

        public void RemoveCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGamePlayActions instance)
        {
            foreach (var item in m_Wrapper.m_GamePlayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GamePlayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);

    // AssemblyGraph
    private readonly InputActionMap m_AssemblyGraph;
    private List<IAssemblyGraphActions> m_AssemblyGraphActionsCallbackInterfaces = new List<IAssemblyGraphActions>();
    private readonly InputAction m_AssemblyGraph_RightMouseButton;
    private readonly InputAction m_AssemblyGraph_LeftMouseButton;
    private readonly InputAction m_AssemblyGraph_Scroll;
    public struct AssemblyGraphActions
    {
        private @Controls m_Wrapper;
        public AssemblyGraphActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightMouseButton => m_Wrapper.m_AssemblyGraph_RightMouseButton;
        public InputAction @LeftMouseButton => m_Wrapper.m_AssemblyGraph_LeftMouseButton;
        public InputAction @Scroll => m_Wrapper.m_AssemblyGraph_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_AssemblyGraph; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AssemblyGraphActions set) { return set.Get(); }
        public void AddCallbacks(IAssemblyGraphActions instance)
        {
            if (instance == null || m_Wrapper.m_AssemblyGraphActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AssemblyGraphActionsCallbackInterfaces.Add(instance);
            @RightMouseButton.started += instance.OnRightMouseButton;
            @RightMouseButton.performed += instance.OnRightMouseButton;
            @RightMouseButton.canceled += instance.OnRightMouseButton;
            @LeftMouseButton.started += instance.OnLeftMouseButton;
            @LeftMouseButton.performed += instance.OnLeftMouseButton;
            @LeftMouseButton.canceled += instance.OnLeftMouseButton;
            @Scroll.started += instance.OnScroll;
            @Scroll.performed += instance.OnScroll;
            @Scroll.canceled += instance.OnScroll;
        }

        private void UnregisterCallbacks(IAssemblyGraphActions instance)
        {
            @RightMouseButton.started -= instance.OnRightMouseButton;
            @RightMouseButton.performed -= instance.OnRightMouseButton;
            @RightMouseButton.canceled -= instance.OnRightMouseButton;
            @LeftMouseButton.started -= instance.OnLeftMouseButton;
            @LeftMouseButton.performed -= instance.OnLeftMouseButton;
            @LeftMouseButton.canceled -= instance.OnLeftMouseButton;
            @Scroll.started -= instance.OnScroll;
            @Scroll.performed -= instance.OnScroll;
            @Scroll.canceled -= instance.OnScroll;
        }

        public void RemoveCallbacks(IAssemblyGraphActions instance)
        {
            if (m_Wrapper.m_AssemblyGraphActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAssemblyGraphActions instance)
        {
            foreach (var item in m_Wrapper.m_AssemblyGraphActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AssemblyGraphActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AssemblyGraphActions @AssemblyGraph => new AssemblyGraphActions(this);
    public interface IGamePlayActions
    {
        void OnSwipe(InputAction.CallbackContext context);
    }
    public interface IAssemblyGraphActions
    {
        void OnRightMouseButton(InputAction.CallbackContext context);
        void OnLeftMouseButton(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
}
