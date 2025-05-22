using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerIdleState : PlayerGroundState
{

    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        base.Enter();
        stateMachine.MovementSpeedModifier = 0;
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }


    public override void Update()
    {
        base.Update();

         // Run or Walk Transition
        if (stateMachine.MovementInput != Vector2.zero)
        {
            OnMove();
            return;
        }
    }


}
