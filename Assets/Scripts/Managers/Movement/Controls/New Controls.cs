// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Managers/Movement/Controls/New Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @NewControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6229866e-a0eb-4397-b57e-d71c937c0e83"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ade3f9cb-3044-40c0-806e-7c09de895a4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlaceItem"",
                    ""type"": ""Button"",
                    ""id"": ""69824281-5636-4902-954e-a8b10d9ccf58"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapItem"",
                    ""type"": ""Button"",
                    ""id"": ""e0cd72a8-500b-476c-8521-8f502a046066"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DebugSpawnAI"",
                    ""type"": ""Button"",
                    ""id"": ""cb2578bc-a9bc-4cfb-b76b-b0ff9d146327"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DebugTime"",
                    ""type"": ""Button"",
                    ""id"": ""340baf6e-575b-489c-a86e-d2748aa2f22d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7e5d34c0-9d7c-4d9b-be13-db7ae9c6acac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9695cf70-2338-49b4-8787-4e8c93be1555"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""362a7008-5236-4b62-a910-55304cf3d392"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""be206a6b-65f0-4296-a9f8-7769409a06c0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b3a4c265-6da4-45a6-8aae-e486106609fe"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1dcbd8e0-6c3d-4d71-bdea-2b922a7f836b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e1bbf5ac-d081-45ed-8324-02837a776616"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b0b1c76d-f952-458b-a9f8-cbcb0bc46545"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8c86637b-4522-4c7f-95b3-4f10044821a6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c1e50287-8480-4967-8190-5aa850d8bfd8"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0788f36a-6798-42c6-aba0-2f7724be0a0f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7c26638e-7251-449c-9963-2f64b90ee8dc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""PlaceItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c06a1efd-89a6-4c17-ba06-b567ebf58152"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PlaceItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0e1c501-594b-422a-a6fe-b8dffb3d3f9a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SwapItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15844557-a907-403a-90dc-8effd8335e10"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SwapItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86099218-9540-492b-8527-dd1406e773f4"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""DebugSpawnAI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40707131-e8ab-4686-8411-a14afcc5532b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""DebugSpawnAI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""310e3dc6-5c24-418f-9e4e-9008908564a6"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""DebugTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f412cb7-f42c-431a-b77e-23c3bd28b044"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""DebugTime"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a20e880c-734d-47c8-997e-277cf1442871"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81b82fdf-12f6-4464-8634-295cb7ed798f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_PlaceItem = m_Player.FindAction("PlaceItem", throwIfNotFound: true);
        m_Player_SwapItem = m_Player.FindAction("SwapItem", throwIfNotFound: true);
        m_Player_DebugSpawnAI = m_Player.FindAction("DebugSpawnAI", throwIfNotFound: true);
        m_Player_DebugTime = m_Player.FindAction("DebugTime", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_PlaceItem;
    private readonly InputAction m_Player_SwapItem;
    private readonly InputAction m_Player_DebugSpawnAI;
    private readonly InputAction m_Player_DebugTime;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @NewControls m_Wrapper;
        public PlayerActions(@NewControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @PlaceItem => m_Wrapper.m_Player_PlaceItem;
        public InputAction @SwapItem => m_Wrapper.m_Player_SwapItem;
        public InputAction @DebugSpawnAI => m_Wrapper.m_Player_DebugSpawnAI;
        public InputAction @DebugTime => m_Wrapper.m_Player_DebugTime;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @PlaceItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlaceItem;
                @PlaceItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlaceItem;
                @PlaceItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlaceItem;
                @SwapItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapItem;
                @SwapItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapItem;
                @SwapItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapItem;
                @DebugSpawnAI.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugSpawnAI;
                @DebugSpawnAI.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugSpawnAI;
                @DebugSpawnAI.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugSpawnAI;
                @DebugTime.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugTime;
                @DebugTime.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugTime;
                @DebugTime.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDebugTime;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PlaceItem.started += instance.OnPlaceItem;
                @PlaceItem.performed += instance.OnPlaceItem;
                @PlaceItem.canceled += instance.OnPlaceItem;
                @SwapItem.started += instance.OnSwapItem;
                @SwapItem.performed += instance.OnSwapItem;
                @SwapItem.canceled += instance.OnSwapItem;
                @DebugSpawnAI.started += instance.OnDebugSpawnAI;
                @DebugSpawnAI.performed += instance.OnDebugSpawnAI;
                @DebugSpawnAI.canceled += instance.OnDebugSpawnAI;
                @DebugTime.started += instance.OnDebugTime;
                @DebugTime.performed += instance.OnDebugTime;
                @DebugTime.canceled += instance.OnDebugTime;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPlaceItem(InputAction.CallbackContext context);
        void OnSwapItem(InputAction.CallbackContext context);
        void OnDebugSpawnAI(InputAction.CallbackContext context);
        void OnDebugTime(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
