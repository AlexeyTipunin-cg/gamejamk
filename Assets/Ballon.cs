using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    private PlayerHealthComponent player { get; set; }

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerHealthComponent>();
        _enemyHealthComponent = gameObject.GetComponent<EnemyHealthComponent>();

    }

    private void Update()
    {
        
    }
    
    private bool shoot = false;
    private EnemyHealthComponent _enemyHealthComponent;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out DamageComponent component))
        {
            _enemyHealthComponent.ReduceHealth(component.GetDamage());
        }
    }
}