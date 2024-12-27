using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirGun : MonoBehaviour
{
    private PlayerHealthComponent player { get; set; }
    [SerializeField] private Transform gun;
    [SerializeField] private ParticleSystem particles;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerHealthComponent>();
    }

    private void Update()
    {
        CalculateRotation(player.transform);
    }
    
    private bool shoot = false;

    private void CalculateRotation(Transform TargetObjTransform)
    {
        // float angle = Mathf.Atan2(TargetObjTransform.position.y - gun.position.y,
        //     TargetObjTransform.position.x - gun.position.x) * Mathf.Rad2Deg;

        float angle = Vector2.Angle(new Vector2(TargetObjTransform.position.x - gun.position.x, TargetObjTransform.position.y - gun.position.y), Vector3.left);

        if (angle >= 0 && angle <= 43)
        {
            gun.rotation = Quaternion.Euler(0, 0, -angle);
            
            particles.Play();
            if (!shoot)
            {
                shoot = true;
                StartCoroutine(Shoot());

            }
        }
        else
        {
            shoot = false;
            particles.Stop();
            StopAllCoroutines();
        }
        
        Debug.Log(angle);
    }

    public IEnumerator Shoot()
    {
        particles.Play();
        yield return new WaitForSeconds(0.1f);
    }
}