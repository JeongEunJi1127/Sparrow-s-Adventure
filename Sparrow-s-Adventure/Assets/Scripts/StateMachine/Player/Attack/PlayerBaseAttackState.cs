using UnityEngine;

public class PlayerBaseAttackState : PlayerAttackState
{
    public PlayerBaseAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (FinishBattle()) OnRun();
    }

    void OnRun()
    {
        stateMachine.ChangeState(stateMachine.RunState);
        stateMachine.Player.Controller.SetAttackState(false);
        SetSpeed(stateMachine.Player.Data.PlayerGroundData.PlayerRunSpeed);
    }

    bool FinishBattle()
    {
        // TODO :: 전투 구현 & 몬스터 다 잡으면 return true;
        GameManager.Instance.Wave++;
        return true;
    }
}
