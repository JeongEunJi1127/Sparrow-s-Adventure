using UnityEngine;
public class PlayerWalkState : PlayerGroundState
{
    public PlayerWalkState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
    }

    protected override void CheckPlayerZPos()
    {
        base.CheckPlayerZPos();
        if (posZ >= 37 && posZ <= 80)
        {
            stateMachine.ChangeState(stateMachine.BaseAttackState);
            stateMachine.Player.Controller.SetAttackState(true);
        }
    }
}
