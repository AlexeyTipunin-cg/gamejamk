using System;
using UnityEngine;

    public class DamageComponent : MonoBehaviour
    {
        private float damage = 10;
        private void Awake()
        {
           var w = GetComponentInParent<Weapon>();
           if (w != null)
           {
               damage = w.weaponConfig.damage;
           }
        }
        
        public float GetDamage()
        {
            return damage;
        }
    }