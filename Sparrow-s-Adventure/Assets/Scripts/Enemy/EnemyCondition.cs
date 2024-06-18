using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour
{
    public event Action<float> OnDamage;

    private float nowHealth;
    private float maxHealth;

    [SerializeField] private Image hPBar;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        OnDamage += TakeDamage;

        nowHealth = enemy.Data.EnemyHealth;
        maxHealth = nowHealth;
    }
    private void Update()
    {
        hPBar.fillAmount = GetPercentage();
    }

    public void CallDamageEvent(float damage)
    {
        OnDamage?.Invoke(damage);
    }

    public void TakeDamage(float amount)
    {
        nowHealth -= amount;
        if (nowHealth <= 0) { Die(); }
    }

    public void Die()
    {
        enemy.Animator.SetBool("Die", true);
        StartCoroutine(DisableAfterAnimation());
    }

    IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public float GetPercentage()
    {
        return nowHealth / maxHealth;
    }
}