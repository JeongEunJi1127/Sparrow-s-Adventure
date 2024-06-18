using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] public EnemySO Data {  get; private set; }

    public EnemyController Controller;
    public EnemyCondition Condition;
    public Animator Animator;

    private void Awake()
    {
        CharacterManager.Instance.Enemy.Add(this);
        Controller = GetComponent<EnemyController>();
        Condition = GetComponent<EnemyCondition>();
        Animator = GetComponent<Animator>();
    }
}
