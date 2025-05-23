using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly GroundData groundData;


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.groundData = stateMachine.Player.Data.GroundData;
    }


    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }


    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }


    public virtual void Update()
    {
        Move();
    }


    public virtual void PhysicsUpdate()
    {

    }


    public virtual void HandleInput()
    {
        ReadMovementInput();
    }


    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }


    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;

        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Aim.started += OnAimStarted;
        input.PlayerActions.Aim.canceled += OnAimCanceled;

        input.PlayerActions.Fire.started += OnFireStarted;
    }


    public virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;

        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;
        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Aim.started -= OnAimStarted;
        input.PlayerActions.Aim.canceled -= OnAimCanceled;

        input.PlayerActions.Fire.started -= OnFireStarted;
    }


    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }


    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }


    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }


    protected virtual void OnAimStarted(InputAction.CallbackContext context)
    {

    }


    protected virtual void OnAimCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnFireStarted(InputAction.CallbackContext context)
    {

    }


    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        MoveCharacter(movementDirection);
    }


    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return (forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x).normalized;
    }


    protected virtual void Rotate(Vector3 movementDirection)
    {

        Transform playerTransform = stateMachine.Player.transform;
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }


    private void MoveCharacter(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.Player.Controller.Move(((movementDirection * movementSpeed)
            + stateMachine.Player.ForceHandler.Movement) * Time.deltaTime);
    }


    protected void ForceMove()
    {
        stateMachine.Player.Controller.Move(
            (
                stateMachine.Player.ForceHandler.Movement
            )
            * Time.deltaTime);
    }


    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }


    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }


    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }
}
