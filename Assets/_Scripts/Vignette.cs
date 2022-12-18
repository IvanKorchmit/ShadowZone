using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette : MonoBehaviour
{
    private UnityEngine.UI.Image m_Image;
    private AudioSource audioSource;
    private void Start()
    {
        m_Image = GetComponent<UnityEngine.UI.Image>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnGUI()
    {
        Player pl = Player.Singleton;
        
        float alpha = pl == null ? 1f : 1f - (pl.Stats.Health / 100f);
        m_Image.color = new (1,0,0,alpha);
        audioSource.volume = alpha;
    }
}
