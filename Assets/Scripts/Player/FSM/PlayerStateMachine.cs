using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }
    public AimCameraSwitcher CameraSwitcher { get; }

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float JumpForce { get; set; }
    public bool IsRunning { get; set; } = false;

    // Test
    public bool HasGun { get; set; } = false;

    public Transform MainCameraTransform { get; set; }



    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }
    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }
    public PlayerAimState AimState { get; }


    public PlayerBaseState CurrentState { get; private set; }
    public PlayerBaseState PreviousState { get; set; }



    public PlayerStateMachine(Player player)
    {
        Player = player;
        CameraSwitcher = GameObject.FindObjectOfType<AimCameraSwitcher>();
        MainCameraTransform = Camera.main.transform;


        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        AimState = new PlayerAimState(this);
    }


    public override void ChangeState(IState newstate)
    {
        if (currentState == newstate) return;

        currentState?.Exit ();

        PreviousState = CurrentState;
        CurrentState = newstate as PlayerBaseState;

        currentState = newstate;
        currentState?.Enter ();
    }
}
