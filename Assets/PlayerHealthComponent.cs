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
        currentHealth = playerConfig.healthValue;
        // StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1);
            ReduceHealth(10); 
        }

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
