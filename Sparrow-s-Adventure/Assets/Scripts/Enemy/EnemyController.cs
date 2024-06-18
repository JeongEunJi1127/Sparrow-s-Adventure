using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackCooltime;

    private bool isAttacking;
    private Enemy enemy;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();

        isAttacking = false;
    }

    private void Update()
    {
        if (!CanDetectedPlayer()) Move();
        else
        {
            animator.SetBool("Walk", false);
            if (!isAttacking) StartCoroutine(Attack());
        }
    }

    void Move()
    {
        Vector3 moveDir = (CharacterManager.Instance.Player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        moveDir.y = 0;

        animator.SetBool("Walk", true);
        Rotate();
        controller.Move(moveDir);
    }

    void Rotate()
    {
        Vector3 dir = (CharacterManager.Instance.Player.transform.position - transform.position).normalized;
        if(dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    bool CanDetectedPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);
        return distanceToPlayer <= attackDistance;
    }

    IEnumerator Attack()
    {
        if (!enemy.Condition.IsDie)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Rotate();
            CharacterManager.Instance.Player.Condition.TakeDamage(enemy.Data.EnemyAttackPower);
            yield return new WaitForSeconds(attackCooltime);
            isAttacking = false;
        }
    }
}