//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.1
//     from Assets/Game/ActionAsset.inputactions
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

public partial class @ActionDefinition: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ActionDefinition()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ActionAsset"",
    ""maps"": [
        {
            ""name"": ""playerMovement"",
            ""id"": ""be587494-fb2d-4a93-a7a7-e3ff70488041"",
            ""actions"": [
                {
                    ""name"": ""up"",
                    ""type"": ""Value"",
                    ""id"": ""01839de7-a52e-45f2-98ea-41a819c21963"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""left"",
                    ""type"": ""Button"",
                    ""id"": ""4383d79a-b117-4c82-ac35-cdd7be87692f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""down"",
                    ""type"": ""Button"",
                    ""id"": ""b2ad85e0-e89a-4adb-954a-19ef0a4f6c95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""right"",
                    ""type"": ""Button"",
                    ""id"": ""3b5b7ca0-9e82-4ebb-8b0a-eae39d0ad196"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d312e8f1-cd3b-449e-a8b5-cef2d9dbd334"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4bcd015-e930-4953-8c30-887639e94358"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe997d6e-21f4-4f6e-92b9-c6cfafbccae0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af53d0e2-b270-48a4-ad46-1e3102927b1f"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b1e2c18-a799-41cf-9063-7eee037f05bc"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee2063bf-189b-4ba0-b18a-11e255346eab"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e2121bd-15b9-483c-80a6-ce2855a9b2b2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""846a338a-3947-4855-9a37-78323f232147"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac0739d0-f811-46c4-948d-eb1df638ac94"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e643cd43-c15e-40a5-8680-257c93a98e7d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4889cb1-18db-4942-ae16-14e128fe72d2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caaf918b-c739-4ec5-8f84-b315bcef629d"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4e21dbc-9459-4b1f-9d9d-d3c47aaa4d07"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c616f529-3c08-4fc4-92b7-7d32c1b7b3f7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efdc6394-6bc6-4632-8eba-998340a3a1b6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7501e42-92df-4d63-8c3d-edb5afd1980b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""playerActions"",
            ""id"": ""842cd1f7-fde4-4205-8b52-1be6463ec206"",
            ""actions"": [
                {
                    ""name"": ""layDropping"",
                    ""type"": ""Button"",
                    ""id"": ""2952023e-7bc1-4fa7-b3c3-6d381889926f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a468295-5a9d-48c0-91b3-f09591bd2e2e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""layDropping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b27a2cf6-8d24-4667-98fd-ad28a68bd214"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""layDropping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // playerMovement
        m_playerMovement = asset.FindActionMap("playerMovement", throwIfNotFound: true);
        m_playerMovement_up = m_playerMovement.FindAction("up", throwIfNotFound: true);
        m_playerMovement_left = m_playerMovement.FindAction("left", throwIfNotFound: true);
        m_playerMovement_down = m_playerMovement.FindAction("down", throwIfNotFound: true);
        m_playerMovement_right = m_playerMovement.FindAction("right", throwIfNotFound: true);
        // playerActions
        m_playerActions = asset.FindActionMap("playerActions", throwIfNotFound: true);
        m_playerActions_layDropping = m_playerActions.FindAction("layDropping", throwIfNotFound: true);
    }

    ~@ActionDefinition()
    {
        UnityEngine.Debug.Assert(!m_playerMovement.enabled, "This will cause a leak and performance issues, ActionDefinition.playerMovement.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_playerActions.enabled, "This will cause a leak and performance issues, ActionDefinition.playerActions.Disable() has not been called.");
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

    // playerMovement
    private readonly InputActionMap m_playerMovement;
    private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
    private readonly InputAction m_playerMovement_up;
    private readonly InputAction m_playerMovement_left;
    private readonly InputAction m_playerMovement_down;
    private readonly InputAction m_playerMovement_right;
    public struct PlayerMovementActions
    {
        private @ActionDefinition m_Wrapper;
        public PlayerMovementActions(@ActionDefinition wrapper) { m_Wrapper = wrapper; }
        public InputAction @up => m_Wrapper.m_playerMovement_up;
        public InputAction @left => m_Wrapper.m_playerMovement_left;
        public InputAction @down => m_Wrapper.m_playerMovement_down;
        public InputAction @right => m_Wrapper.m_playerMovement_right;
        public InputActionMap Get() { return m_Wrapper.m_playerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
            @up.started += instance.OnUp;
            @up.performed += instance.OnUp;
            @up.canceled += instance.OnUp;
            @left.started += instance.OnLeft;
            @left.performed += instance.OnLeft;
            @left.canceled += instance.OnLeft;
            @down.started += instance.OnDown;
            @down.performed += instance.OnDown;
            @down.canceled += instance.OnDown;
            @right.started += instance.OnRight;
            @right.performed += instance.OnRight;
            @right.canceled += instance.OnRight;
        }

        private void UnregisterCallbacks(IPlayerMovementActions instance)
        {
            @up.started -= instance.OnUp;
            @up.performed -= instance.OnUp;
            @up.canceled -= instance.OnUp;
            @left.started -= instance.OnLeft;
            @left.performed -= instance.OnLeft;
            @left.canceled -= instance.OnLeft;
            @down.started -= instance.OnDown;
            @down.performed -= instance.OnDown;
            @down.canceled -= instance.OnDown;
            @right.started -= instance.OnRight;
            @right.performed -= instance.OnRight;
            @right.canceled -= instance.OnRight;
        }

        public void RemoveCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementActions @playerMovement => new PlayerMovementActions(this);

    // playerActions
    private readonly InputActionMap m_playerActions;
    private List<IPlayerActionsActions> m_PlayerActionsActionsCallbackInterfaces = new List<IPlayerActionsActions>();
    private readonly InputAction m_playerActions_layDropping;
    public struct PlayerActionsActions
    {
        private @ActionDefinition m_Wrapper;
        public PlayerActionsActions(@ActionDefinition wrapper) { m_Wrapper = wrapper; }
        public InputAction @layDropping => m_Wrapper.m_playerActions_layDropping;
        public InputActionMap Get() { return m_Wrapper.m_playerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Add(instance);
            @layDropping.started += instance.OnLayDropping;
            @layDropping.performed += instance.OnLayDropping;
            @layDropping.canceled += instance.OnLayDropping;
        }

        private void UnregisterCallbacks(IPlayerActionsActions instance)
        {
            @layDropping.started -= instance.OnLayDropping;
            @layDropping.performed -= instance.OnLayDropping;
            @layDropping.canceled -= instance.OnLayDropping;
        }

        public void RemoveCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActionsActions @playerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnLayDropping(InputAction.CallbackContext context);
    }
}
