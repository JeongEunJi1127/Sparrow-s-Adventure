using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    protected float posZ;

    public PlayerBaseState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        CheckPlayerZPos();
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    protected virtual void CheckPlayerZPos()
    {
        posZ = Mathf.Repeat(stateMachine.Player.transform.position.z, 135f);
    }

    protected void SetSpeed(float speed)
    {
        stateMachine.Player.Controller.SetSpeed(speed);
    }
}
