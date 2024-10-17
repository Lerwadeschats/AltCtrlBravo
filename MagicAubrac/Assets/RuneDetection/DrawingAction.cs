//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/RuneDetection/DrawingAction.inputactions
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

public partial class @DrawingAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DrawingAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DrawingAction"",
    ""maps"": [
        {
            ""name"": ""DrawingMap"",
            ""id"": ""c478f9f3-a8d5-460a-adb5-89beb479a994"",
            ""actions"": [
                {
                    ""name"": ""Draw"",
                    ""type"": ""Button"",
                    ""id"": ""118c9ef3-f22f-40b3-938d-8af475edd903"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Validate"",
                    ""type"": ""Button"",
                    ""id"": ""8ef8ec40-a7b8-4fb3-8111-57c43c091130"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RuneActivation"",
                    ""type"": ""Button"",
                    ""id"": ""2bf2f155-4c43-45e0-b113-788a23059b4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectA1"",
                    ""type"": ""Button"",
                    ""id"": ""6dab8e1f-ae1c-4699-9cf8-960ea358a22a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectB1"",
                    ""type"": ""Button"",
                    ""id"": ""4fb2a59d-6ef2-4db7-bec1-bceadc820686"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectA2"",
                    ""type"": ""Button"",
                    ""id"": ""909ba24f-6b69-4f15-a97e-381b5fe69564"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectB2"",
                    ""type"": ""Button"",
                    ""id"": ""20005daf-6aac-46b4-8c3f-c850601ca753"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectA3"",
                    ""type"": ""Button"",
                    ""id"": ""4ca6f9df-4624-4b15-bcce-9377da54aa07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkSelectB3"",
                    ""type"": ""Button"",
                    ""id"": ""5568f2dc-47bf-495a-8461-ed40f5ede3d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkPour1"",
                    ""type"": ""Button"",
                    ""id"": ""1ed7f9cc-8b71-4a6e-96ba-0419667063d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkPour2"",
                    ""type"": ""Button"",
                    ""id"": ""98b6e5a6-9192-4350-96b0-d579fc3f3ebf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DrinkPour3"",
                    ""type"": ""Button"",
                    ""id"": ""6614277c-bc0b-423f-8106-d4fd46fed6a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EmptyDrink"",
                    ""type"": ""Button"",
                    ""id"": ""e8796c66-bcbf-4115-9561-d21df9909c43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee931497-82e0-4def-bb5a-3db56f5b5362"",
                    ""path"": ""<Pen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Draw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49591d21-a3f0-468d-8fdb-fca4ce633d3c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Draw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb98524a-cc77-4068-ad30-1127e35bdb74"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Validate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2be9d33a-203f-4bbc-9c94-e45d5516c870"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RuneActivation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d67f38af-8433-4ce4-92f5-26d964e39417"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectA1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""909790df-2cc7-4868-ad37-234b87095abf"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectA2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4dfebfd-f727-41a9-990a-b70698193151"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectB3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e1e4ffe-39e0-4bf3-a803-694d0beb3d12"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkPour1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47b8431a-113f-4989-a880-3df861d709c3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkPour2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17afb9fa-a877-4e70-9d9e-b19b028cad95"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkPour3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab4878a3-aeaa-40e6-85c1-081e80dd42b5"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EmptyDrink"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51bcc167-b659-45cf-b97b-c964ab3a5107"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectB1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73000d83-3f1c-4056-9cca-c28547520e71"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectA3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3540a57-06a3-419e-8eca-767764e5d0ad"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrinkSelectB2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DrawingMap
        m_DrawingMap = asset.FindActionMap("DrawingMap", throwIfNotFound: true);
        m_DrawingMap_Draw = m_DrawingMap.FindAction("Draw", throwIfNotFound: true);
        m_DrawingMap_Validate = m_DrawingMap.FindAction("Validate", throwIfNotFound: true);
        m_DrawingMap_RuneActivation = m_DrawingMap.FindAction("RuneActivation", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectA1 = m_DrawingMap.FindAction("DrinkSelectA1", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectB1 = m_DrawingMap.FindAction("DrinkSelectB1", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectA2 = m_DrawingMap.FindAction("DrinkSelectA2", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectB2 = m_DrawingMap.FindAction("DrinkSelectB2", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectA3 = m_DrawingMap.FindAction("DrinkSelectA3", throwIfNotFound: true);
        m_DrawingMap_DrinkSelectB3 = m_DrawingMap.FindAction("DrinkSelectB3", throwIfNotFound: true);
        m_DrawingMap_DrinkPour1 = m_DrawingMap.FindAction("DrinkPour1", throwIfNotFound: true);
        m_DrawingMap_DrinkPour2 = m_DrawingMap.FindAction("DrinkPour2", throwIfNotFound: true);
        m_DrawingMap_DrinkPour3 = m_DrawingMap.FindAction("DrinkPour3", throwIfNotFound: true);
        m_DrawingMap_EmptyDrink = m_DrawingMap.FindAction("EmptyDrink", throwIfNotFound: true);
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

    // DrawingMap
    private readonly InputActionMap m_DrawingMap;
    private List<IDrawingMapActions> m_DrawingMapActionsCallbackInterfaces = new List<IDrawingMapActions>();
    private readonly InputAction m_DrawingMap_Draw;
    private readonly InputAction m_DrawingMap_Validate;
    private readonly InputAction m_DrawingMap_RuneActivation;
    private readonly InputAction m_DrawingMap_DrinkSelectA1;
    private readonly InputAction m_DrawingMap_DrinkSelectB1;
    private readonly InputAction m_DrawingMap_DrinkSelectA2;
    private readonly InputAction m_DrawingMap_DrinkSelectB2;
    private readonly InputAction m_DrawingMap_DrinkSelectA3;
    private readonly InputAction m_DrawingMap_DrinkSelectB3;
    private readonly InputAction m_DrawingMap_DrinkPour1;
    private readonly InputAction m_DrawingMap_DrinkPour2;
    private readonly InputAction m_DrawingMap_DrinkPour3;
    private readonly InputAction m_DrawingMap_EmptyDrink;
    public struct DrawingMapActions
    {
        private @DrawingAction m_Wrapper;
        public DrawingMapActions(@DrawingAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Draw => m_Wrapper.m_DrawingMap_Draw;
        public InputAction @Validate => m_Wrapper.m_DrawingMap_Validate;
        public InputAction @RuneActivation => m_Wrapper.m_DrawingMap_RuneActivation;
        public InputAction @DrinkSelectA1 => m_Wrapper.m_DrawingMap_DrinkSelectA1;
        public InputAction @DrinkSelectB1 => m_Wrapper.m_DrawingMap_DrinkSelectB1;
        public InputAction @DrinkSelectA2 => m_Wrapper.m_DrawingMap_DrinkSelectA2;
        public InputAction @DrinkSelectB2 => m_Wrapper.m_DrawingMap_DrinkSelectB2;
        public InputAction @DrinkSelectA3 => m_Wrapper.m_DrawingMap_DrinkSelectA3;
        public InputAction @DrinkSelectB3 => m_Wrapper.m_DrawingMap_DrinkSelectB3;
        public InputAction @DrinkPour1 => m_Wrapper.m_DrawingMap_DrinkPour1;
        public InputAction @DrinkPour2 => m_Wrapper.m_DrawingMap_DrinkPour2;
        public InputAction @DrinkPour3 => m_Wrapper.m_DrawingMap_DrinkPour3;
        public InputAction @EmptyDrink => m_Wrapper.m_DrawingMap_EmptyDrink;
        public InputActionMap Get() { return m_Wrapper.m_DrawingMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DrawingMapActions set) { return set.Get(); }
        public void AddCallbacks(IDrawingMapActions instance)
        {
            if (instance == null || m_Wrapper.m_DrawingMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DrawingMapActionsCallbackInterfaces.Add(instance);
            @Draw.started += instance.OnDraw;
            @Draw.performed += instance.OnDraw;
            @Draw.canceled += instance.OnDraw;
            @Validate.started += instance.OnValidate;
            @Validate.performed += instance.OnValidate;
            @Validate.canceled += instance.OnValidate;
            @RuneActivation.started += instance.OnRuneActivation;
            @RuneActivation.performed += instance.OnRuneActivation;
            @RuneActivation.canceled += instance.OnRuneActivation;
            @DrinkSelectA1.started += instance.OnDrinkSelectA1;
            @DrinkSelectA1.performed += instance.OnDrinkSelectA1;
            @DrinkSelectA1.canceled += instance.OnDrinkSelectA1;
            @DrinkSelectB1.started += instance.OnDrinkSelectB1;
            @DrinkSelectB1.performed += instance.OnDrinkSelectB1;
            @DrinkSelectB1.canceled += instance.OnDrinkSelectB1;
            @DrinkSelectA2.started += instance.OnDrinkSelectA2;
            @DrinkSelectA2.performed += instance.OnDrinkSelectA2;
            @DrinkSelectA2.canceled += instance.OnDrinkSelectA2;
            @DrinkSelectB2.started += instance.OnDrinkSelectB2;
            @DrinkSelectB2.performed += instance.OnDrinkSelectB2;
            @DrinkSelectB2.canceled += instance.OnDrinkSelectB2;
            @DrinkSelectA3.started += instance.OnDrinkSelectA3;
            @DrinkSelectA3.performed += instance.OnDrinkSelectA3;
            @DrinkSelectA3.canceled += instance.OnDrinkSelectA3;
            @DrinkSelectB3.started += instance.OnDrinkSelectB3;
            @DrinkSelectB3.performed += instance.OnDrinkSelectB3;
            @DrinkSelectB3.canceled += instance.OnDrinkSelectB3;
            @DrinkPour1.started += instance.OnDrinkPour1;
            @DrinkPour1.performed += instance.OnDrinkPour1;
            @DrinkPour1.canceled += instance.OnDrinkPour1;
            @DrinkPour2.started += instance.OnDrinkPour2;
            @DrinkPour2.performed += instance.OnDrinkPour2;
            @DrinkPour2.canceled += instance.OnDrinkPour2;
            @DrinkPour3.started += instance.OnDrinkPour3;
            @DrinkPour3.performed += instance.OnDrinkPour3;
            @DrinkPour3.canceled += instance.OnDrinkPour3;
            @EmptyDrink.started += instance.OnEmptyDrink;
            @EmptyDrink.performed += instance.OnEmptyDrink;
            @EmptyDrink.canceled += instance.OnEmptyDrink;
        }

        private void UnregisterCallbacks(IDrawingMapActions instance)
        {
            @Draw.started -= instance.OnDraw;
            @Draw.performed -= instance.OnDraw;
            @Draw.canceled -= instance.OnDraw;
            @Validate.started -= instance.OnValidate;
            @Validate.performed -= instance.OnValidate;
            @Validate.canceled -= instance.OnValidate;
            @RuneActivation.started -= instance.OnRuneActivation;
            @RuneActivation.performed -= instance.OnRuneActivation;
            @RuneActivation.canceled -= instance.OnRuneActivation;
            @DrinkSelectA1.started -= instance.OnDrinkSelectA1;
            @DrinkSelectA1.performed -= instance.OnDrinkSelectA1;
            @DrinkSelectA1.canceled -= instance.OnDrinkSelectA1;
            @DrinkSelectB1.started -= instance.OnDrinkSelectB1;
            @DrinkSelectB1.performed -= instance.OnDrinkSelectB1;
            @DrinkSelectB1.canceled -= instance.OnDrinkSelectB1;
            @DrinkSelectA2.started -= instance.OnDrinkSelectA2;
            @DrinkSelectA2.performed -= instance.OnDrinkSelectA2;
            @DrinkSelectA2.canceled -= instance.OnDrinkSelectA2;
            @DrinkSelectB2.started -= instance.OnDrinkSelectB2;
            @DrinkSelectB2.performed -= instance.OnDrinkSelectB2;
            @DrinkSelectB2.canceled -= instance.OnDrinkSelectB2;
            @DrinkSelectA3.started -= instance.OnDrinkSelectA3;
            @DrinkSelectA3.performed -= instance.OnDrinkSelectA3;
            @DrinkSelectA3.canceled -= instance.OnDrinkSelectA3;
            @DrinkSelectB3.started -= instance.OnDrinkSelectB3;
            @DrinkSelectB3.performed -= instance.OnDrinkSelectB3;
            @DrinkSelectB3.canceled -= instance.OnDrinkSelectB3;
            @DrinkPour1.started -= instance.OnDrinkPour1;
            @DrinkPour1.performed -= instance.OnDrinkPour1;
            @DrinkPour1.canceled -= instance.OnDrinkPour1;
            @DrinkPour2.started -= instance.OnDrinkPour2;
            @DrinkPour2.performed -= instance.OnDrinkPour2;
            @DrinkPour2.canceled -= instance.OnDrinkPour2;
            @DrinkPour3.started -= instance.OnDrinkPour3;
            @DrinkPour3.performed -= instance.OnDrinkPour3;
            @DrinkPour3.canceled -= instance.OnDrinkPour3;
            @EmptyDrink.started -= instance.OnEmptyDrink;
            @EmptyDrink.performed -= instance.OnEmptyDrink;
            @EmptyDrink.canceled -= instance.OnEmptyDrink;
        }

        public void RemoveCallbacks(IDrawingMapActions instance)
        {
            if (m_Wrapper.m_DrawingMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDrawingMapActions instance)
        {
            foreach (var item in m_Wrapper.m_DrawingMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DrawingMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DrawingMapActions @DrawingMap => new DrawingMapActions(this);
    public interface IDrawingMapActions
    {
        void OnDraw(InputAction.CallbackContext context);
        void OnValidate(InputAction.CallbackContext context);
        void OnRuneActivation(InputAction.CallbackContext context);
        void OnDrinkSelectA1(InputAction.CallbackContext context);
        void OnDrinkSelectB1(InputAction.CallbackContext context);
        void OnDrinkSelectA2(InputAction.CallbackContext context);
        void OnDrinkSelectB2(InputAction.CallbackContext context);
        void OnDrinkSelectA3(InputAction.CallbackContext context);
        void OnDrinkSelectB3(InputAction.CallbackContext context);
        void OnDrinkPour1(InputAction.CallbackContext context);
        void OnDrinkPour2(InputAction.CallbackContext context);
        void OnDrinkPour3(InputAction.CallbackContext context);
        void OnEmptyDrink(InputAction.CallbackContext context);
    }
}
