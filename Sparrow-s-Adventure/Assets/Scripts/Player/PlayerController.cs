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
