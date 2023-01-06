//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputAction/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""30f5ead3-d948-4c6d-a7d5-67b9001585ec"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e8fd8ff2-65d7-44d1-8dae-01ddf3c23cc4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""afc3e2e1-9edb-4d43-b592-c13e64b52a07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""13228eee-aea2-4664-8fbf-d24f1f07c0c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TempItemUse"",
                    ""type"": ""Button"",
                    ""id"": ""1fe494bc-c607-4f40-b727-3b417af9237b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TakeItem"",
                    ""type"": ""Button"",
                    ""id"": ""fc9000cd-ddca-42b9-84d0-31df0c7d8b77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TestMakeItem"",
                    ""type"": ""Button"",
                    ""id"": ""76518a53-7e29-4ad4-b675-a9a25734c8e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4781acbb-145a-4f01-aa7c-2903d649b8d1"",
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
                    ""id"": ""b8155a9e-bfa7-43e4-b4c8-12b768e85415"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e8935b68-feec-46fe-a9ef-e90d81559a9c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""095d0fa3-a748-4746-9680-4cb4baba4f89"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62c1889e-9789-40f6-b056-64d34a8fbf37"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8a847583-bf92-4a71-84fb-4206519513da"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5a9feb9-b280-41d5-b699-fddc6762a19a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1004cfaf-fd86-4848-920d-cf193e3fa389"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TempItemUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dda013b-bc44-4459-b3d9-70eef89b8e3e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae2f8c73-ea97-4947-81cc-5c3c090d5440"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestMakeItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""c6136a03-b10e-4638-8431-feefc00506e7"",
            ""actions"": [
                {
                    ""name"": ""InventoryOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""1025edbf-0a0a-4aea-b6ee-e78012e6359d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InventoryItemUse"",
                    ""type"": ""Button"",
                    ""id"": ""bb1cb9c1-712e-42b3-be28-f9c537c3dfbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8287b1d8-19c6-45d2-aad6-efe8642a520c"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InventoryOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db3e1b6a-d0e6-461f-8b5e-ff8b1b83d184"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InventoryItemUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Equipment"",
            ""id"": ""8f3042f4-179a-485c-ab60-517739098c4f"",
            ""actions"": [
                {
                    ""name"": ""EquipmentOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""90c77fbd-3e62-4037-bb61-6aae4efb806f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9b5495a3-07cb-4a85-88d1-fd25a38b3ceb"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EquipmentOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Skill"",
            ""id"": ""460549ee-e10f-4b0b-9a55-f5ac34724e92"",
            ""actions"": [
                {
                    ""name"": ""SkillWindowOnOff"",
                    ""type"": ""Button"",
                    ""id"": ""c6b9f93c-d559-4988-a57a-913cb3f4ef73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""97428b72-9de3-4903-a221-c348819d8595"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillWindowOnOff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_TempItemUse = m_Player.FindAction("TempItemUse", throwIfNotFound: true);
        m_Player_TakeItem = m_Player.FindAction("TakeItem", throwIfNotFound: true);
        m_Player_TestMakeItem = m_Player.FindAction("TestMakeItem", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_InventoryOnOff = m_Inventory.FindAction("InventoryOnOff", throwIfNotFound: true);
        m_Inventory_InventoryItemUse = m_Inventory.FindAction("InventoryItemUse", throwIfNotFound: true);
        // Equipment
        m_Equipment = asset.FindActionMap("Equipment", throwIfNotFound: true);
        m_Equipment_EquipmentOnOff = m_Equipment.FindAction("EquipmentOnOff", throwIfNotFound: true);
        // Skill
        m_Skill = asset.FindActionMap("Skill", throwIfNotFound: true);
        m_Skill_SkillWindowOnOff = m_Skill.FindAction("SkillWindowOnOff", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_TempItemUse;
    private readonly InputAction m_Player_TakeItem;
    private readonly InputAction m_Player_TestMakeItem;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @TempItemUse => m_Wrapper.m_Player_TempItemUse;
        public InputAction @TakeItem => m_Wrapper.m_Player_TakeItem;
        public InputAction @TestMakeItem => m_Wrapper.m_Player_TestMakeItem;
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
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @TempItemUse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTempItemUse;
                @TempItemUse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTempItemUse;
                @TempItemUse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTempItemUse;
                @TakeItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeItem;
                @TakeItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeItem;
                @TakeItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTakeItem;
                @TestMakeItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTestMakeItem;
                @TestMakeItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTestMakeItem;
                @TestMakeItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTestMakeItem;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @TempItemUse.started += instance.OnTempItemUse;
                @TempItemUse.performed += instance.OnTempItemUse;
                @TempItemUse.canceled += instance.OnTempItemUse;
                @TakeItem.started += instance.OnTakeItem;
                @TakeItem.performed += instance.OnTakeItem;
                @TakeItem.canceled += instance.OnTakeItem;
                @TestMakeItem.started += instance.OnTestMakeItem;
                @TestMakeItem.performed += instance.OnTestMakeItem;
                @TestMakeItem.canceled += instance.OnTestMakeItem;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_InventoryOnOff;
    private readonly InputAction m_Inventory_InventoryItemUse;
    public struct InventoryActions
    {
        private @PlayerInput m_Wrapper;
        public InventoryActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryOnOff => m_Wrapper.m_Inventory_InventoryOnOff;
        public InputAction @InventoryItemUse => m_Wrapper.m_Inventory_InventoryItemUse;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @InventoryOnOff.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryOnOff;
                @InventoryOnOff.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryOnOff;
                @InventoryItemUse.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryItemUse;
                @InventoryItemUse.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryItemUse;
                @InventoryItemUse.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventoryItemUse;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InventoryOnOff.started += instance.OnInventoryOnOff;
                @InventoryOnOff.performed += instance.OnInventoryOnOff;
                @InventoryOnOff.canceled += instance.OnInventoryOnOff;
                @InventoryItemUse.started += instance.OnInventoryItemUse;
                @InventoryItemUse.performed += instance.OnInventoryItemUse;
                @InventoryItemUse.canceled += instance.OnInventoryItemUse;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // Equipment
    private readonly InputActionMap m_Equipment;
    private IEquipmentActions m_EquipmentActionsCallbackInterface;
    private readonly InputAction m_Equipment_EquipmentOnOff;
    public struct EquipmentActions
    {
        private @PlayerInput m_Wrapper;
        public EquipmentActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @EquipmentOnOff => m_Wrapper.m_Equipment_EquipmentOnOff;
        public InputActionMap Get() { return m_Wrapper.m_Equipment; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EquipmentActions set) { return set.Get(); }
        public void SetCallbacks(IEquipmentActions instance)
        {
            if (m_Wrapper.m_EquipmentActionsCallbackInterface != null)
            {
                @EquipmentOnOff.started -= m_Wrapper.m_EquipmentActionsCallbackInterface.OnEquipmentOnOff;
                @EquipmentOnOff.performed -= m_Wrapper.m_EquipmentActionsCallbackInterface.OnEquipmentOnOff;
                @EquipmentOnOff.canceled -= m_Wrapper.m_EquipmentActionsCallbackInterface.OnEquipmentOnOff;
            }
            m_Wrapper.m_EquipmentActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EquipmentOnOff.started += instance.OnEquipmentOnOff;
                @EquipmentOnOff.performed += instance.OnEquipmentOnOff;
                @EquipmentOnOff.canceled += instance.OnEquipmentOnOff;
            }
        }
    }
    public EquipmentActions @Equipment => new EquipmentActions(this);

    // Skill
    private readonly InputActionMap m_Skill;
    private ISkillActions m_SkillActionsCallbackInterface;
    private readonly InputAction m_Skill_SkillWindowOnOff;
    public struct SkillActions
    {
        private @PlayerInput m_Wrapper;
        public SkillActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SkillWindowOnOff => m_Wrapper.m_Skill_SkillWindowOnOff;
        public InputActionMap Get() { return m_Wrapper.m_Skill; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkillActions set) { return set.Get(); }
        public void SetCallbacks(ISkillActions instance)
        {
            if (m_Wrapper.m_SkillActionsCallbackInterface != null)
            {
                @SkillWindowOnOff.started -= m_Wrapper.m_SkillActionsCallbackInterface.OnSkillWindowOnOff;
                @SkillWindowOnOff.performed -= m_Wrapper.m_SkillActionsCallbackInterface.OnSkillWindowOnOff;
                @SkillWindowOnOff.canceled -= m_Wrapper.m_SkillActionsCallbackInterface.OnSkillWindowOnOff;
            }
            m_Wrapper.m_SkillActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SkillWindowOnOff.started += instance.OnSkillWindowOnOff;
                @SkillWindowOnOff.performed += instance.OnSkillWindowOnOff;
                @SkillWindowOnOff.canceled += instance.OnSkillWindowOnOff;
            }
        }
    }
    public SkillActions @Skill => new SkillActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnTempItemUse(InputAction.CallbackContext context);
        void OnTakeItem(InputAction.CallbackContext context);
        void OnTestMakeItem(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnInventoryOnOff(InputAction.CallbackContext context);
        void OnInventoryItemUse(InputAction.CallbackContext context);
    }
    public interface IEquipmentActions
    {
        void OnEquipmentOnOff(InputAction.CallbackContext context);
    }
    public interface ISkillActions
    {
        void OnSkillWindowOnOff(InputAction.CallbackContext context);
    }
}
