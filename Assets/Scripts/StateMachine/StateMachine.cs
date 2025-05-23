using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected IState currentState;


    public virtual void ChangeState(IState newstate)
    {
        if (currentState == newstate) return;

        currentState?.Exit();
        currentState = newstate;
        currentState?.Enter();
    }


    public void HandleInput()
    {
        currentState?.HandleInput();
    }


    public void Update()
    {
        currentState.Update();
    }


    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
