public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

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

    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);  
    }
}
