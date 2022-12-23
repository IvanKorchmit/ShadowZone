using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private int keyIDRequired;
    [SerializeField] private UnityEngine.Events.UnityEvent OnOpen;
    public void Interact()
    {
        if (Player.Singleton.Backpack.Keys.Contains(keyIDRequired))
        {
            OnOpen?.Invoke();
        }
    }
}
