using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private int keyIDRequired;
    public void Interact()
    {
        if (Player.Singleton.Backpack.Keys.Contains(keyIDRequired))
        {
            Destroy(gameObject);
        }
    }
}
