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
        currentHealth = enemyConfig.healthValue;
    }
    
    public void ReduceHealth(float amount)
    {
        if (currentHealth - amount <= 0)
        {
            OnDeath?.Invoke();
            Destroy(this.gameObject);
        }
        
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
}
