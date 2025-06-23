using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;

        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

        //if (stateMachine.HasGun && !(stateMachine.CurrentState is PlayerAimState))
        //{
        //    stateMachine.PreviousState = this;
        //    stateMachine.ChangeState(stateMachine.AimState);
        //}
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }


    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);
        stateMachine.ChangeState(stateMachine.RunState);
    }


    protected override void OnAimStarted(InputAction.CallbackContext context)
    {
        //base.OnAimStarted(context);
    }
}
