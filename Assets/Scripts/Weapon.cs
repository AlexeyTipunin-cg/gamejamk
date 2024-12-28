using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponConfig _weaponConfig;
    [SerializeField] private ParticleSystem attack;
    private WeaponSlotConfig config;


    private float currentCooldownTime;
    bool isCooldown;

    public void SetConfig(WeaponSlotConfig injectedConfig)
    {
        config = injectedConfig;
    }
    public void Rotate(Vector3 mouseDelta)
    {
        Vector3 diff = mouseDelta - transform.position;
        float f = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        
        Debug.Log("WEAPON_ANGLE:-----> " + f + " limits " + config.angles.ToString());
        
        if (_weaponConfig.angles.x <= f && f <= _weaponConfig.angles.y)
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
        yield return new WaitForSeconds(_weaponConfig.cooldownInSeconds);
        isCooldown = false;
    }
}