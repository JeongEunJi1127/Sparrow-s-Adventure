using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [Header("Ground")]
    [SerializeField] private string groundParameter = "@Ground";
    [SerializeField] private string walkParameter = "Walk";
    [SerializeField] private string runParameter = "Run";

    [Header("Attack")]
    [SerializeField] private string attackParameter = "@Attack";
    [SerializeField] private string baseAttackParameter = "BaseAttack";

    [HideInInspector]
    public int GroundParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int BaseAttackParameterHash { get; private set; }

    public void AnimaterHashInit()
    {
        GroundParameterHash = Animator.StringToHash(groundParameter);
        WalkParameterHash = Animator.StringToHash(walkParameter);
        RunParameterHash = Animator.StringToHash(runParameter);
        AttackParameterHash = Animator.StringToHash(attackParameter);
        BaseAttackParameterHash = Animator.StringToHash(baseAttackParameter);
    }
}