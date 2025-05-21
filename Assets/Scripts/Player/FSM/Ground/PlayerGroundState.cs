using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }


    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }


    public override void Update()
    {
        base.Update();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Fall Check
        if (!stateMachine.Player.Controller.isGrounded
            && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }


    // Walk - Idle
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)
            return;

        stateMachine.ChangeState(stateMachine.IdleState);
        stateMachine.IsRunning = false;

        base.OnMovementCanceled(context);
    }


    // Idle - Walk
    protected virtual void OnMove()
    {
        if (!stateMachine.IsRunning)
            stateMachine.ChangeState(stateMachine.WalkState);
        else
            stateMachine.ChangeState(stateMachine.RunState);
    }


    // Ground - Jump - Run or Walk
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }


    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);

        stateMachine.IsRunning = true;
    }
}
