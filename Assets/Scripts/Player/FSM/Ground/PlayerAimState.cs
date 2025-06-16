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

        stateMachine.MovementSpeedModifier = 0.3f;
        isRunning = false;

        SetExclusiveAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
        stateMachine.CameraSwitcher?.SwitchToShoulderAim();

    }


    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
        StopAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
        stateMachine.CameraSwitcher?.SwitchToHipFire();
    }


    public override void Update()
    {
        base.Update();
        HandleMovementAnimation();
    }


    private void HandleMovementAnimation()
    {
        bool isCurrentlyMoving = stateMachine.MovementInput != Vector2.zero;

        if (isCurrentlyMoving && !isRunning)
        {
            SetExclusiveAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
            isRunning = true;
        }
        else if (!isCurrentlyMoving && isRunning)
        {
            SetExclusiveAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
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


    protected override void OnFireStarted(InputAction.CallbackContext context) { }


    protected override void OnAimStarted(InputAction.CallbackContext context)
    {
        stateMachine.CameraSwitcher?.SwitchToShoulderAim();
    }


    protected override void OnAimCanceled(InputAction.CallbackContext context)
    {
        stateMachine.CameraSwitcher?.SwitchToHipFire();
        stateMachine.ChangeState(stateMachine.IdleState);
    }


    private void SetExclusiveAnimation(int activeHash)
    {
        var anim = stateMachine.Player.Animator;

        anim.SetBool(stateMachine.Player.AnimationData.IdleParameterHash, false);
        anim.SetBool(stateMachine.Player.AnimationData.IsAimingIdleParameterHash, false);
        anim.SetBool(stateMachine.Player.AnimationData.IsAimingRunParameterHash, false);

        anim.SetBool(activeHash, true);
    }
}
