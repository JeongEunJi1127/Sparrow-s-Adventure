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
            // �ִ� 1�ʿ� 2�� ����. ���ݼӵ� = 1�� �� ���� Ƚ��.
            float attackSpeed = CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed;
            float waitSeconds = 1 / attackSpeed <= 2 ? 1 / attackSpeed : 2;
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    void Attack()
    {
        // TODO :: ���� �����ϰ� ���� �����ϱ�
        Debug.Log("����!");
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
