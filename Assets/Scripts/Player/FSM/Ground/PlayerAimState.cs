using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerGroundState
{
    private bool isRunning;

    public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        base.Enter();
        //stateMachine.MovementSpeedModifier = 0.3f;

        isRunning = false;
        StartAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);

    }


    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
    }


    public override void Update()
    {
        base.Update();

        bool isMoving = stateMachine.MovementInput != Vector2.zero;

        if (isMoving && !isRunning)
        {
            // Idle - Run
            StopAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
            isRunning =true;
        }
        else if (!isMoving && isRunning)
        {
            // Run - Idle
            StopAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
            isRunning = false;
        }

    }


    protected override void Rotate(Vector3 movementDirection)
    {
        Transform playerTransform = stateMachine.Player.transform;
        Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }


    protected override void OnFireStarted(InputAction.CallbackContext context)
    {

    }


    protected override void OnAimCanceled(InputAction.CallbackContext context)
    {
        stateMachine.CameraSwitcher?.SwitchToHipFire();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
