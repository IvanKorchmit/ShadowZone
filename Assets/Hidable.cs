using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoW;
public class Hidable : MonoBehaviour, IInteractable
{
    [SerializeField] private FogOfWarUnit fogOfWarUnit;
    private Player currentPlayer;

    [SerializeField] private AudioEvent open;
    [SerializeField] private AudioEvent close;
    [SerializeField] private AudioSource audio;


    private void Start()
    {
        fogOfWarUnit.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPlayer != null)
            {
                currentPlayer.gameObject.SetActive(true);
                
                fogOfWarUnit.enabled = false;
                currentPlayer = null;
                close.Play(audio);
            }
        }
    }
    public void Interact()
    {
        if (currentPlayer == null && Player.Singleton != null)
        {
            currentPlayer = Player.Singleton;
            currentPlayer.gameObject.SetActive(false);
            fogOfWarUnit.enabled = true;
            open.Play(audio);

        }
    }
}
