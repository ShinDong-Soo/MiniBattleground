using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerGroundState
{
    public PlayerAimState(PlayerStateMachine stateMachine) : base(stateMachine)
    { }


    public override void Enter()
    {
        base.Enter();
        stateMachine.MovementSpeedModifier = 0.3f;
        StartAnimation(stateMachine.Player.AnimationData.AimParameterHash);

    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AimParameterHash);
    }


    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput == Vector2.zero)
            stateMachine.Player.Animator.SetFloat("moveSpeed", 0);
        else
            stateMachine.Player.Animator.SetFloat("moveSpeed", 1);
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
