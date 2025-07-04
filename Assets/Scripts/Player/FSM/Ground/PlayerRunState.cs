using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        stateMachine.MovementSpeedModifier =groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);

        //if (stateMachine.HasGun && !(stateMachine.CurrentState is PlayerAimState))
        //{
        //    stateMachine.PreviousState = this;
        //    stateMachine.ChangeState(stateMachine.AimState);
        //}
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }
}
