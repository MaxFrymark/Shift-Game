using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private PlayerInputActions playerInputActions;
    

    private void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActions.Shift.performed += OnShiftPressed;
        playerInputActions.PlayerActions.Enable();
    }
    
    private void OnShiftPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        int shiftDirection = (int)value;
        player.ShiftLane(shiftDirection);
    }
}
