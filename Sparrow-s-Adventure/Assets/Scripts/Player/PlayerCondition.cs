using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float health;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = health;
    }

    private void Start()
    {
        health = CharacterManager.Instance.Player.Data.PlayerInfoData.PlayerHealth;
    }

    public void Heal(float amount)
    {
        health = Mathf.Max(health + amount, maxHealth);
    }

    public void UpgradeHealth(float amount)
    {
        maxHealth += amount;
        Heal(amount);
    }

    public void TakeDamage(float amount)
    {
        health = Mathf.Min(0, health - amount);
        if(health < 0) { Die(); }
    }

    public void Die()
    {
        Debug.Log("Á×À½");
    }
}