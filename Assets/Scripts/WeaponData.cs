using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponData
{
    public int slotIndex;
    public WeaponConfig weaponConfig;
    
    public float weight => weaponConfig.weight;
    public Weapon prefab => weaponConfig.weaponPrefab;
}