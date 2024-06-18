using UnityEngine;
using UnityEngine.UI;

public class PlayerCondition : MonoBehaviour
{
    public float nowHealth;
    private float maxHealth;

    public Image HPBar;

    private void Start()
    {
        nowHealth = CharacterManager.Instance.Player.Data.PlayerInfoData.PlayerHealth;
        maxHealth = nowHealth;
    }

    private void Update()
    {
        HPBar.fillAmount = GetPercentage();
    }

    public void Heal(float amount)
    {
        nowHealth = Mathf.Max(nowHealth + amount, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        nowHealth -= amount;
        UIManager.Instance.UpdateStatText();
        if(nowHealth <= 0) { Die(); }
    }

    public void Die()
    {
        Debug.Log("Á×À½");
    }
    public float GetPercentage()
    {
        return nowHealth / maxHealth;
    }
}