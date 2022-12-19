using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ammo, Medkit, Weapon, Key
    }
    [SerializeField] private ItemType type;
    [SerializeField] private AmmoItem ammo;
    [SerializeField] private Weapon weapon;
    [SerializeField] private int keyID;

    [SerializeField] private AudioEvent onPickup;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * Random.Range(4, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == ItemType.Ammo)
            {
                Player.Singleton.Backpack.Ammos.Add(ammo);
                onPickup.Play(Player.Singleton.Audio);
                Destroy(gameObject);
            }
            else if (type == ItemType.Weapon)
            {
                if (Player.Singleton.Backpack.AddWeapon(weapon))
                {
                    onPickup.Play(Player.Singleton.Audio);
                    Destroy(gameObject);
                }
            }
            else if (type == ItemType.Key)
            {
                Player.Singleton.Backpack.Keys.Add(keyID);
                onPickup.Play(Player.Singleton.Audio);
                Destroy(gameObject);
            }
        }
    }
}
