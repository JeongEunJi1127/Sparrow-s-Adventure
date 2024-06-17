using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] public EnemySO Data {  get; private set; }

    public EnemyController Controller;
}
