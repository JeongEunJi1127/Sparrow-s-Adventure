public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ¸÷ ½ºÆù
        //ObjectPoolManager.Instance.SpawnFromPool("Enemy");
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash); 
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
}
