using System.Collections;
using UnityEngine;

public class PlayerBaseAttackState : PlayerAttackState
{
    public PlayerBaseAttackState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Player.Controller.OnAttack += StartBaseAttackAnim;
        stateMachine.Player.Controller.StartAttack();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.Controller.StopAttack();
        StopAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //if (IsFinishBattle()) OnRun();
    }

    void OnRun()
    {
        stateMachine.ChangeState(stateMachine.RunState);
        stateMachine.Player.Controller.SetAttackState(false);
        SetSpeed(stateMachine.Player.Data.PlayerGroundData.PlayerRunSpeed);
    }

    bool IsFinishBattle()
    {
        if (ObjectPoolManager.Instance.CountActiveInHierarchy("Enemy") == 0)
        {
            GameManager.Instance.NextWave();
            return true;
        }
        return false;
    }

    void StartBaseAttackAnim()
    {
        StartAnimation(stateMachine.Player.AnimationData.BaseAttackParameterHash);
    }
}
