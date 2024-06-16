using UnityEngine.Scripting.APIUpdating;

public class PlayerGroundState : PlayerBaseState
{
    protected float moveSpeed;
    public PlayerGroundState(PlayerStateMachine _stateMachine) : base(_stateMachine)
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
        Move(stateMachine.Player.Data.PlayerGroundData.PlayerWalkSpeed);
        //if (InBattleField()) Attack();
    }

    void Move(float moveSpeed)
    {
        stateMachine.Player.Controller.SetSpeed(moveSpeed);
    }

    void Attack()
    {
        stateMachine.ChangeState(stateMachine.BaseAttackState);
        //stateMachine.Player.Controller.SetAttackState(true);
    }

    bool InBattleField()
    {
        return true;
    }
}
