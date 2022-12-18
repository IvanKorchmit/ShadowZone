using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtTime : MonoBehaviour
{
    [SerializeField] private bool m_DeleteSelf;
    [SerializeField] private float m_Time;
    // Start is called before the first frame update
    private void Start()
    {
        if (m_DeleteSelf)
        {
            Destroy(gameObject, m_Time);
            return;
        }
        AudioSource source = GetComponent<AudioSource>();
        Destroy(source, 3f);
    }
}
