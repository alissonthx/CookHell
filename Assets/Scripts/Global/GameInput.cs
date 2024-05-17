using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private InputActions playerControlls;


    // public event EventHandler OnBindingRebind;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private void Awake()
    {
        Instance = this;

        playerControlls = new InputActions();
        OnEnable();

        playerControlls.Player.Interact.performed += Interact_performed;
        playerControlls.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerControlls.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerControlls.Player.Interact.performed -= Interact_performed;
        playerControlls.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerControlls.Player.Pause.performed -= Pause_performed;

        playerControlls.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerControlls.Player.Movement.ReadValue<Vector2>();

        return inputVector;
    }

    private void OnEnable()
    {
        playerControlls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControlls.Player.Disable();
    }


    // Perform Interactive Rebinding
    // OnBindingRebind?.Invoke(This, EventArgs.Empty);
}
