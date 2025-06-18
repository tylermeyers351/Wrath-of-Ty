using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public Vector2 MovementValue { get; private set; }

    public bool IsAttacking { get; private set; }

    public bool IsSprinting { get; private set; }

    public event Action JumpEvent;

    public event Action DodgeEvent;

    public event Action TargetEvent;

    public event Action CancelEvent;


    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }
    
    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Cinemachine is using this method.
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        TargetEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        CancelEvent?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSprinting = true;
        }
        else if (context.canceled)
        {
            IsSprinting = false;
        }
    }
}
