using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private ShipSlot[] slots;
    private WeaponConfig[] selectedWeapons;

    [HideInInspector]public List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        selectedWeapons = SceneConnection.weapons;

        for (int i = 0; i < selectedWeapons.Length; i++)
        {
            var w = selectedWeapons[i];
            if (w != null)
            {
                var slot = slots[i];
                var weapon = Instantiate(w.weaponPrefab, slot.transform);
                weapon.SetConfig(slot.slotConfig);
                weapon.transform.rotation = Quaternion.Euler(0, 0, slot.slotConfig.angleInGame);
                weapons.Add(weapon);
            }
        }
    }

    public Transform GetWeaponSlot(int index)
    {
        return slots[index].transform;
    }
    
    public Weapon GetWeapon(int index)
    {
        if (index > weapons.Count - 1)
        {
            return null;
        }
        
        return weapons[index];
    }

    public bool HasWeapon()
    {
        return weapons.Count > 0;
    }
}
