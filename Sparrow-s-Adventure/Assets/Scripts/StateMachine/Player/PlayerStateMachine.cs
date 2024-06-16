using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player;

    [Header("Ground")]
    public float WalkSpeed;
    public float RunSpeed;

    [Header("Attack")]
    public float BaseAttackPower;
    public float AttackSpeed;

    [Header("States")]
    public PlayerWalkState WalkState;
    public PlayerRunState RunState;
    public PlayerBaseAttackState BaseAttackState;

    public PlayerStateMachine(Player player)
    {
        Player = player;
        WalkSpeed = Player.Data.PlayerGroundData.PlayerWalkSpeed;
        RunSpeed = Player.Data.PlayerGroundData.PlayerRunSpeed;
        BaseAttackPower = Player.Data.PlayerAttackData.PlayerAttackPower;
        AttackSpeed = Player.Data.PlayerAttackData.AttackSpeed;

        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        BaseAttackState = new PlayerBaseAttackState(this);
    }
}
