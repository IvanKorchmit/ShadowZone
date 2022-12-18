using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponInfoAndTutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI canLoad;
    [SerializeField] private TextMeshProUGUI canPull;
    [SerializeField] private TextMeshProUGUI canShoot;
    [SerializeField] private Color grey;
    private void OnGUI()
    {
        if (Player.Singleton == null) return;

        Weapon weapon = Player.Singleton.WeaponInfo;
        title.text = weapon.WeaponBase.Name;
        canPull.color = !weapon.IsReady && weapon.AmmoCount > 0 ? Color.white : grey;
        canLoad.color = Player.Singleton.Backpack.HasAmmo(weapon.WeaponBase, out _) && weapon.AmmoCount < weapon.WeaponBase.Capacity ? Color.white : grey;
        canShoot.color = weapon.IsReady ? Color.white : grey;
    }
}
