using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField]private PlayerConfig playerConfig;
    
    public Action<float> OnHealthChanged;
    public Action OnDeath;
    
    private float currentHealth;
    
    public bool isAlive => currentHealth > 0;

    private void Awake()
    {
        currentHealth = SceneConnection.bodyConfig.health;
    }
    
    public void ReduceHealth(float amount)
    {
        if (currentHealth - amount <= 0)
        {
            OnDeath?.Invoke();
        }
        
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        OnHealthChanged?.Invoke(currentHealth);
    }
    
}
