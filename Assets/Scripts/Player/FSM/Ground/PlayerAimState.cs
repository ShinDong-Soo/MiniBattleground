using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerGroundState
{
    private bool isCurrentlyRunning;

    public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        base.Enter();
        //stateMachine.MovementSpeedModifier = 0.3f;

        isCurrentlyRunning = false;
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

        if (isMoving && !isCurrentlyRunning)
        {
            // Idle - Run
            StopAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
            isCurrentlyRunning=true;
        }
        else if (!isMoving && isCurrentlyRunning)
        {
            // Run - Idle
            StopAnimation(stateMachine.Player.AnimationData.IsAimingRunParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.IsAimingIdleParameterHash);
            isCurrentlyRunning = false;
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
