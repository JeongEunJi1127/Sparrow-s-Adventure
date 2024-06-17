using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;

    public event Action OnAttack;
    public bool IsAttacking;

    private float moveSpeed;

    private void Awake()
    {
        IsAttacking = false;
        Controller = GetComponent<CharacterController>();
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
        Controller.Move(moveDir);
    }

    public void SetAttackState(bool b)
    {
        IsAttacking = b;
    }

    IEnumerator AttackRoutine()
    {
        while(true)
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

    public void StartAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    public void StopAttack()
    {
        StopCoroutine(AttackRoutine());
    }
}
