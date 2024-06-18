public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        // 몹 스폰 - 위치 변경
        ObjectPoolManager.Instance.SpawnFromPool("Enemy");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
}
