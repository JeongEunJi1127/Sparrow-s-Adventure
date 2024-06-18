using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public event Action OnAttack;
    public bool IsAttacking;
    public LayerMask EnemyLayerMask;

    Transform bodyTransform;
    private float moveSpeed;
    private bool canAttack;

    private void Awake()
    {
        IsAttacking = false;
        canAttack = true;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        bodyTransform = transform.Find("Body");
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
        Vector3 moveDir = (Vector3.forward * moveSpeed+ (CharacterManager.Instance.Player.ForceReceiver.Movement)) * Time.deltaTime;
        controller.Move(moveDir);
    }

    public void SetAttackState(bool b)
    {
        IsAttacking = b;
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            CallAttackEvent();
            canAttack = true;
            GameObject obj = GetTarget();
            if(obj != null)  Attack(GetTarget());    

            // 최대 1초에 2번 공격. 공격속도 = 1초 당 공격 횟수.
            float attackSpeed = CharacterManager.Instance.Player.Data.PlayerAttackData.AttackSpeed;
            float waitSeconds = 1 / attackSpeed <= 2 ? 1 / attackSpeed : 2;
            yield return new WaitForSeconds(waitSeconds);
        }
    }

    //bool CanDetectEnemy()
    //{ 
    //    RaycastHit hit;
    //    Debug.DrawRay(bodyTransform.position + bodyTransform.forward, bodyTransform.forward * 3f, Color.cyan);
    //    if (Physics.Raycast(bodyTransform.position + bodyTransform.forward, bodyTransform.forward * 3f, out hit, 30f, EnemyLayerMask))
    //    {
    //        Debug.Log("Enemy detected: " + hit.collider.gameObject.name);
    //        return true;
    //    }
    //    return false;
    //}

    public void StartAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    public void StopAttack()
    {
        StopCoroutine(AttackRoutine());
        canAttack = true;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.layer == 7 && canAttack)
    //    {
    //        Attack(collision.gameObject);
    //    }
    //}

    GameObject GetTarget()
    {
        foreach (var enemy in ObjectPoolManager.Instance.PoolDictionary["Enemy"])
        {
            if (enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null;
    }

    void Attack(GameObject obj)
    {
        canAttack = false;
        float playerAttackDamage = CharacterManager.Instance.Player.Data.PlayerAttackData.PlayerAttackPower ;
        obj.GetComponent<EnemyCondition>().CallDamageEvent(playerAttackDamage);
    }
}
