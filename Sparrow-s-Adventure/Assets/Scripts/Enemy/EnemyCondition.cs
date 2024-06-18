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
    public bool IsDie;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        nowHealth = enemy.Data.EnemyHealth;
        maxHealth = nowHealth;
        IsDie = false;
    }

    private void Start()
    {
        OnDamage += TakeDamage;
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
        if (nowHealth <= 0) 
        {
            nowHealth = 0;
            Die(); 
        }
    }

    public void Revival()
    {
        nowHealth = maxHealth;
        IsDie = false;
    }

    public void Die()
    {
        enemy.Animator.SetTrigger("Die");
        StartCoroutine(DisableAfterAnimation());
        IsDie = true;
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