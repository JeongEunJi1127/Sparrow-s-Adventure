using System;
using UnityEngine;

[Serializable]
public class PlayerInfoData
{
    public string PlayerName;
    public float PlayerHealth;
}

[Serializable]
public class PlayerGroundData
{
    [Header("Walk")]
    [Range(0f,10f)] public float PlayerWalkSpeed;

    [Header("Run")]
    [Range(0f, 25f)] public float PlayerRunSpeed;
}

[Serializable]
public class PlayerAttackData
{
    [Header("BaseAttack")]
    public float PlayerAttackPower;
    public float AttackSpeed;
}

[CreateAssetMenu(fileName ="PlayerSO",menuName = "New Data/Player")]
public class PlayerSO : ScriptableObject
{
    public PlayerInfoData PlayerInfoData;
    public PlayerGroundData PlayerGroundData;
    public PlayerAttackData PlayerAttackData;
}
