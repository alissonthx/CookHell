using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    #region Variables
    private InputActions playerControlls;
    private InputActionReference CatchingFood, GetingFood;

    [Header("Player Stats")]

    [SerializeField]
    private CharacterController controller;
    [Space]    
    private Vector3 movement = Vector3.zero;
    public Vector3 _movement => this.movement;

    #endregion

    private void Awake()
    {
        playerControlls = new InputActions();
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
}
