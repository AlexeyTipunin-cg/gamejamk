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
        
        Debug.Log("WEAPON_ANGLE:-----> " + f.ToString() + " limits " + (_weaponConfig.angles.x + config.angleInGame).ToString() + " " + (_weaponConfig.angles.y + config.angleInGame).ToString());
        
        if ( (_weaponConfig.angles.x + config.angleInGame <= f && f <= _weaponConfig.angles.y + config.angleInGame) ||
            (_weaponConfig.angles.x + config.angleInGame <= f - 360 && f - 360 <= _weaponConfig.angles.y + config.angleInGame))
        {
            transform.rotation = Quaternion.Euler(0, 0, f);

        }
    }
    
    private bool isEmmiting = false;
    
    

    public void Attack()
    {
        if (!isCooldown && !isEmmiting)
        {
            attack.Play();
            isCooldown = true;
            isEmmiting = true;

            StartCoroutine(UpdateTime());
        }
        
        Debug.LogWarning("isCooldown " + isCooldown + "  " + "is Stopped " +attack.isStopped );
    }

    public void StopAttack()
    {
        attack.Stop();
        isEmmiting = false;
    }

    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(_weaponConfig.cooldownInSeconds);
        isCooldown = false;
    }
}