using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    private PlayerHealthComponent player { get; set; }
    [SerializeField] private ParticleSystem particles;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerHealthComponent>();
        _enemyHealthComponent = gameObject.GetComponent<EnemyHealthComponent>();

        particles.Play();
    }

    private void Update()
    {
        //CalculateRotation(player.transform);
    }
    
    private bool shoot = false;
    private EnemyHealthComponent _enemyHealthComponent;

    private void CalculateRotation(Transform TargetObjTransform)
    {
        // float angle = Mathf.Atan2(TargetObjTransform.position.y - gun.position.y,
        //     TargetObjTransform.position.x - gun.position.x) * Mathf.Rad2Deg;

        //float angle = Vector2.Angle(new Vector2(TargetObjTransform.position.x - gun.position.x, TargetObjTransform.position.y - gun.position.y), Vector3.left);
        
        
        if (!shoot)
        {
            particles.Play();

            shoot = true;
        }
        /*
        else
        {
            shoot = false;
            particles.Stop();
            StopAllCoroutines();
        }
        */
        
        //Debug.Log(angle);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out DamageComponent component))
        {
            _enemyHealthComponent.ReduceHealth(component.GetDamage());
        }
    }
}