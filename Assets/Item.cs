using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AmmoItem ammo;

    [SerializeField] private AudioEvent onPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Singleton.Backpack.Ammos.Add(ammo);
            onPickup.Play(Player.Singleton.Audio);
            Destroy(gameObject);
        }
    }
}
