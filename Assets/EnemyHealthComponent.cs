using System;
using System.Collections;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour
{
    [SerializeField]private EnemyConfig enemyConfig;
    
    public Action<float> OnHealthChanged;
    public Action OnDeath;
    
    private float currentHealth;
    
    public bool isAlive => currentHealth > 0;

    private void Awake()
    {
        RefillHealth();
    }

    public void RefillHealth()
    {
        currentHealth = enemyConfig.healthValue;
    }
    
    public void ReduceHealth(float amount, bool needDestroy = true)
    {
        if (currentHealth - amount <= 0)
        {
            OnDeath?.Invoke();
            if (needDestroy)
            {
                Destroy(this.gameObject);
            }
        }
        
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
}
