using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] items;
    private bool isOpen;

    public void Interact()
    {
        if (isOpen) return;
        Open();
    }

    private void Open()
    {
        foreach (var item in items)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        GetComponent<Animator>().Play("Open");
        isOpen = true;
    }
}
public interface IInteractable
{
    void Interact();
}