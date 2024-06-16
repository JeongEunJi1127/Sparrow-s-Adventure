using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;

    public bool IsAttacking;

    private float moveSpeed;

    private void Awake()
    {
        IsAttacking = false;
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(!IsAttacking) Move();
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetAttackState(bool b)
    {
        IsAttacking = b;
    }

    void Move()
    {
        Vector3 moveDir = Vector3.forward * moveSpeed * Time.deltaTime;
        Controller.Move(moveDir);
    }
}
