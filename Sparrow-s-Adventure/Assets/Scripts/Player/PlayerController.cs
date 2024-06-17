using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public event Action OnAttack;
    public bool IsAttacking;
    public LayerMask EnemyLayerMask;

    private float moveSpeed;

    private void Awake()
    {
        IsAttacking = false;
        controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        OnAttack += Attack;
    }

    private void Update()
    {
        if(!IsAttacking) Move();
    }

    public void CallAttackEvent()
    {
        OnAttack?.Invoke();
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Move()
    {
        Vector3 moveDir = Vector3.forward * moveSpeed * Time.deltaTime;
        controller.Move(moveDir);
    }

    public void SetAttackState(bool b)
    {
        IsAttacking = b;
    }

    IEnumerator AttackRoutine()
    {
        while(CanDetectEnemy())
        {
            CallAttackEvent();
            // 최대 1초에 2번 공격. 공격속도 = 1초 당 공격 횟수.
            float attackSpeed = CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed;
            float waitSeconds = 1 / attackSpeed <= 2 ? 1 / attackSpeed : 2;
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    void Attack()
    {
        // TODO :: 몬스터 구현하고 공격 구현하기
        Debug.Log("공격!");

    }

    bool CanDetectEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.forward, Vector3.forward, out hit, 3f, EnemyLayerMask))
        {
            return true;
        }
        return false;
    }

    public void StartAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    public void StopAttack()
    {
        StopCoroutine(AttackRoutine());
    }
}
