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
    }

    private void Update()
    {
        
    }
    
    private bool shoot = false;

    private void OnParticleCollision(GameObject other)
    {
        var t = other.GetComponent<ParticleSystem>();
        if (!t.customData.enabled)
        {
            Destroy(gameObject);
        }
    }
}