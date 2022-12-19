using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Inventory
{
    [field: SerializeField] public List<AmmoItem> Ammos { get; private set; } = new List<AmmoItem>();
    [field: SerializeField] public List<Weapon> Weapons { get; private set; } = new();
    [field: SerializeField] public List<int> Keys { get; private set; } = new List<int>();
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
    public Weapon GetWeapon(int index, Player player)
    {
        foreach (var item in Weapons)
        {
            if (item.WeaponBase.WeaponSlot == index)
            {
                return item;
            }
        }
        return player.WeaponInfo;
    }
    public bool AddWeapon(Weapon weapon)
    {
        foreach (var item in Weapons)
        {
            if (item.WeaponBase == weapon.WeaponBase)
            {
                return false;
            }
        }
        Weapons.Add(weapon);
        return true;
    }
}
