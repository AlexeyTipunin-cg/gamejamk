using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private ShipSlot[] slots;
    private WeaponData[] selectedWeapons;

    [HideInInspector] public List<(int index, Weapon weapon)> weapons = new List<(int index, Weapon)>();

    private void Awake()
    {
        selectedWeapons = SceneConnection.weapons;

        for (int i = 0; i < selectedWeapons.Length; i++)
        {
            var w = selectedWeapons[i];
            if (w != null)
            {
                var slot = slots.First(s => s.index == w.slotIndex);
                var weapon = Instantiate(w.prefab, slot.transform);
                weapon.SetConfig(slot.slotConfig);
                weapon.transform.rotation = Quaternion.Euler(0, 0, slot.slotConfig.angleInGame);
                weapons.Add((slot.index, weapon));
            }
        }
    }

    public Transform GetWeaponSlot(int index)
    {
        return slots[index].transform;
    }

    public (int index, Weapon weapon) GetFirst()
    {
        var index = weapons.Min(s => s.index);
        return weapons.First(s => s.index == index);
    }
    
    public Weapon GetWeapon(int index)
    {
        if (index > weapons.Count - 1)
        {
            return null;
        }
        
        return weapons.FirstOrDefault( s => s.index == index).weapon;
    }

    public bool HasWeapon()
    {
        return weapons.Count > 0;
    }
}
