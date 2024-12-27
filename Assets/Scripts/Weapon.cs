using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig config;
    [SerializeField] private ParticleSystem attack;

    private float currentCooldownTime;
    bool isCooldown;
    public void Rotate(Vector3 mouseDelta)
    {
        Vector3 diff = mouseDelta - transform.position;
        float f = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        if (config.angles.x <= f && f <= config.angles.y)
        {
            transform.rotation = Quaternion.Euler(0, 0, f);

        }
    }
    
    

    public void Attack()
    {
        if (!isCooldown)
        {
            attack.Play();
            isCooldown = true;

            StartCoroutine(UpdateTime());
        }
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(config.cooldownInSeconds);
        isCooldown = false;
    }
}