public class PlayerRunState : PlayerGroundState
{
    private bool hasRespawned;

    public PlayerRunState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        hasRespawned = false;
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    protected override void CheckPlayerZPos()
    {
        base.CheckPlayerZPos();
        if (posZ <= 37)
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            SetSpeed(stateMachine.Player.Data.PlayerGroundData.PlayerWalkSpeed);
        }
        else if (posZ >= 80 && !hasRespawned)
        {
            MapManager.Instance.RespawnMap();
            hasRespawned = true;
        }
    }
}
