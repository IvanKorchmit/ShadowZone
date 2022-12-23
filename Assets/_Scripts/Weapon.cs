using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
    private bool open;
    [SerializeField] private List<AmmoItem> ammo;
    [SerializeField] private AmmoItem currentAmmo;
    public AmmoBase AmmoBase => currentAmmo?.Ammo ?? null;
    public bool IsReady => currentAmmo?.Ammo ?? null != null && !currentAmmo.isUsed;
    public int AmmoCount => ammo.Count;

    [field: SerializeField] public WeaponBase WeaponBase { get; private set; }
    public void Pull(Player owner)
    {
        WeaponBase.CockSound.Play(owner.Audio);
        if (WeaponBase is Shotgun)
        {
            if (currentAmmo != null)
            {
                WeaponBase.OnCockLoaded(owner);
                currentAmmo = null;
            }
            if (ammo.Count > 0)
            {
                currentAmmo = ammo[0];
                ammo.RemoveAt(0);
            }
        }
        else
        {
            for (int i = 0; i < AmmoCount; i++)
            {
                WeaponBase.OnCockLoaded(owner);
            }
            ammo.Clear();
            currentAmmo = null;
        }
    }
    private bool AddAmmo(AmmoBase ammo)
    {
        if (AmmoCount + 1 > WeaponBase.Capacity)
        {
            return false;
        }
        foreach (AmmoItem a in this.ammo)
        {
            if (a.Ammo == ammo && a.Quantity < ammo.Capacity)
            {
                a.Quantity++;
                return true;
            }
        }
        this.ammo.Add(new AmmoItem(ammo, 1));
        return true;
    }

    public bool AreAllAmmosUsable()
    {
        int count = 0;
        foreach (var item in ammo)
        {
            if (!item.isUsed)
            {
                count++;
            }
        }
        return count != 0 || !(currentAmmo?.isUsed ?? false);
    }

    public void Insert(Inventory backpack, Player owner)
    {
        
        if (AmmoCount < WeaponBase.Capacity && WeaponBase.IsChamber && !open)
        {
            open = true;
            WeaponBase.OpenSound.Play(owner.Audio);
            return;
        }
        if (backpack.HasAmmo(WeaponBase, out AmmoItem _ammo))
        {
            if (AddAmmo(_ammo.Ammo))
            {
                WeaponBase.InsertSound.Play(owner.Audio);
                _ammo.Quantity--;
                if (_ammo.Quantity <= 0)
                {
                    backpack.Ammos.Remove(_ammo);
                }

            }
            else if (WeaponBase.IsChamber)
            {
                open = false;
                WeaponBase.CloseSound.Play(owner.Audio);
                currentAmmo = ammo[0];
            }
        }
        else if (WeaponBase.IsChamber)
        {
            if (AmmoCount > 0)
            {
                open = false;
                WeaponBase.CloseSound.Play(owner.Audio);
                currentAmmo = ammo[0];
            }
        }
    }
    public void Shoot(Player owner, float angle)
    {
        if (!IsReady || (WeaponBase is Shotgun && currentAmmo.isUsed))
        {
            WeaponBase.AmmoOutSound.Play(owner.Audio);
            return;
        }
        if (currentAmmo.isUsed)
        {
            ammo.Add(currentAmmo);
            currentAmmo = ammo[0];
            ammo.RemoveAt(0);
            WeaponBase.AmmoOutSound.Play(owner.Audio);
            return;
        }
        WeaponBase.Use(owner, angle, currentAmmo.Ammo);
        currentAmmo.isUsed = true;
    }

}
[System.Serializable]
public class AmmoItem
{
    public AmmoItem(AmmoBase ammo, int quantity)
    {
        Ammo = ammo;
        Quantity = quantity;
    }
    public bool isUsed;
    [field: SerializeField] public AmmoBase Ammo { get; private set; }
    [field: SerializeField] public int Quantity { get; set; }
}

public abstract class AmmoBase : ScriptableObject
{
    [SerializeField] private WeaponBase[] validWeapons;
    [field: SerializeField] public int Capacity { get; private set; }
    [SerializeField] private float damage;
    [field: SerializeField] public string Name { get; private set; }
    public float Damage => damage;
    public abstract void OnEnemyImpact(Projectile projectile, Enemy enemy);
    public bool DoesWeaponFit(WeaponBase weapon)
    {
        foreach (WeaponBase w in validWeapons)
        {
            if (w == weapon)
            {
                return true;
            }
        }
        return false;
    }
}
