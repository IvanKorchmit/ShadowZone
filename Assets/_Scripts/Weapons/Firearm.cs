using UnityEngine;
using System.Collections;
[CreateAssetMenu]
public class Firearm : WeaponBase
{

    
    [SerializeField] private bool m_PlaySound;
    [SerializeField] protected AudioEvent m_ShootSound;
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private float m_Cone;
    [SerializeField] private bool m_SpawnShell;
    [SerializeField] protected GameObject m_Shell;
    public override void Use(Player owner, float angle, AmmoBase ammoType)
    {
        Projectile projectile = Instantiate(m_ProjectilePrefab, owner.ShootPoint.transform.position, Quaternion.Euler(0, 0, angle + Random.Range(-m_Cone, m_Cone))).GetComponent<Projectile>();
        projectile.Initialize(ammoType);
        if (m_PlaySound)
        {
            m_ShootSound?.Play(owner.Audio);
        }
        if (m_SpawnShell)
        {
            Instantiate(m_Shell, owner.transform.position, Quaternion.identity);
        }
    }
    public override void OnCockLoaded(Player owner)
    {
        Instantiate(m_Shell, owner.transform.position, Quaternion.identity);
    }
}
