using UnityEngine;

public abstract class StateMachine
{
    protected IState curState;

    public void ChangeState(IState state)
    {
        curState?.Exit();
        curState = state;
        curState?.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }

    public void PhysicsUpdate()
    {
        curState?.PhysicsUpdate();
    }
}
