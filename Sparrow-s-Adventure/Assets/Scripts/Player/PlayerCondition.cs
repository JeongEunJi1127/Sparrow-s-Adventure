using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public float nowHealth;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = nowHealth;
        nowHealth = CharacterManager.Instance.Player.Data.PlayerInfoData.PlayerHealth;
    }

    public void Heal(float amount)
    {
        nowHealth = Mathf.Max(nowHealth + amount, maxHealth);
    }

    public void UpgradeHealth(float amount)
    {
        maxHealth += amount;
        Heal(amount);
    }

    public void TakeDamage(float amount)
    {
        nowHealth = Mathf.Min(0, nowHealth - amount);
        if(nowHealth < 0) { Die(); }
    }

    public void Die()
    {
        Debug.Log("Á×À½");
    }
}