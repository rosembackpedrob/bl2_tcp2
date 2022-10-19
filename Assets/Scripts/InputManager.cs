using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerMovActions playerMov;

    [SerializeField] private PlayerMotor motor;
    [SerializeField] private PlayerLook look;
    [SerializeField] private Gun gun;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerMov = playerInput.PlayerMov;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //syntax similar to events (=> is a pointer), ctx: callback context
        playerMov.Jump.performed += ctx => motor.Jump();

        playerMov.Crouch.performed += ctx => motor.Crouch();
        playerMov.Sprint.performed += ctx => motor.Sprint();

        playerMov.Shoot.performed += _ => gun.Shoot();
    }

    // FixedUpdate is to physics
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from movement action.
        motor.ProcessMove(playerMov.Movement.ReadValue<Vector2>());
    }

    void LateUpdate() 
    {
        look.ProcessLook(playerMov.Look.ReadValue<Vector2>());
    }

    private void OnEnable() 
    {
        playerMov.Enable();
    }
    private void OnDisable() 
    {
        playerMov.Disable();
    }
}
