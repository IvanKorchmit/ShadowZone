using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAcrossScenes : MonoBehaviour
{
    private static MusicAcrossScenes currentInstance;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (currentInstance == null || audioSource.clip != currentInstance.audioSource.clip)
        {
            if (currentInstance != null)
            {
                Destroy(currentInstance.gameObject);
            }
            currentInstance = this;
            DontDestroyOnLoad(currentInstance.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
