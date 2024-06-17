using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName ="New Data/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int EnemyID;
    public EnemyType EnemyType;
    public float EnemyHealth;
    public float EnemyAttackPower;
}
