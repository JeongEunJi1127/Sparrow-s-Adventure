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
    private bool canAttack;

    private void Awake()
    {
        IsAttacking = false;
        canAttack = true;
        controller = GetComponent<CharacterController>();
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
        while (true)
        {
            if (CanDetectEnemy())
            {
                Debug.Log("참새공격감지");
                CallAttackEvent();
                canAttack = true;

                // 최대 1초에 2번 공격. 공격속도 = 1초 당 공격 횟수.
                float attackSpeed = CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed;
                float waitSeconds = 1 / attackSpeed <= 2 ? 1 / attackSpeed : 2;
                yield return new WaitForSeconds(waitSeconds);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    bool CanDetectEnemy()
    { 
        // 참새도 몸통 돌려주기
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && canAttack)
        {
            Attack(collision.gameObject);
            // 질문. 여기서 getcomponent밖에 방법이 없는지
        }
    }

    void Attack(GameObject obj)
    {
        Debug.Log("참새공격");
        canAttack = false;
        float playerAttackDamage = CharacterManager.Instance.Player.Data.PlayerAttackData.PlayerAttackPower ;
        obj.GetComponent<EnemyCondition>().CallDamageEvent(playerAttackDamage);
    }
}
