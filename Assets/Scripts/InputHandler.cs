using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private OpeningMenu openingMenu;
    
    public delegate void ShiftLaneDelegate(int shiftDirection);
    public event ShiftLaneDelegate OnShiftLane;
    
    private PlayerInputActions playerInputActions;
    

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActions.Shift.performed += OnShiftPressed;
        playerInputActions.PlayerActions.Enable();
        player.SubscribeToInputHandler(this);
        openingMenu.SubscribeToInputHandler(this);
    }
    
    private void OnShiftPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        OnShiftLane?.Invoke((int)value);
    }
}
