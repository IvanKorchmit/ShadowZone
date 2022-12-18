using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Inventory
{
    [field: SerializeField] public List<AmmoItem> Ammos { get; private set; } = new List<AmmoItem>();
    [field: SerializeField] public Weapon[] Weapons { get; private set; }
    public bool HasAmmo(WeaponBase weapon, out AmmoItem result)
    {
        foreach (AmmoItem ammo in Ammos)
        {
            if (ammo.Ammo.DoesWeaponFit(weapon))
            {
                result = ammo;
                return true;
            }
        }

        result = null;
        return false;
    }
}
