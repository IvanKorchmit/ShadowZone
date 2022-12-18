using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecal : MonoBehaviour
{
    [SerializeField] GameObject[] m_Decals;
    private ParticleSystem m_ParticleSystem;
    private ParticleSystem.Particle[] m_Particles;

    // Start is called before the first frame update
    void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }
    // Update is called once per frame
    void LateUpdate()
    {
        int count = m_ParticleSystem.GetParticles(m_Particles);

        bool worldSpace = (m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World);
        for (int i = 0; i < count; i++)
        {
            if (i < count)
            {
                if (worldSpace)
                {

                    if (m_Particles[i].remainingLifetime < 0.05f)
                    {
                        if (Random.Range(0, 100) < 10)
                        {
                            Destroy(Instantiate(m_Decals[Random.Range(0, m_Decals.Length)], m_Particles[i].position, Quaternion.identity), 5);
                        }
                    }
                }
            }
        }
    }
}
