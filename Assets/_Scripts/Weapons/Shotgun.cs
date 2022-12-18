using UnityEngine;
using System.Collections;
[CreateAssetMenu]
public class Shotgun : Firearm
{
    [SerializeField] private int m_Pellets;
    public override void Use(Player owner, float angle, AmmoBase ammoType)
    {
        m_ShootSound.Play(owner.Audio);
        for (int i = 0; i < m_Pellets; i++)
        {
            base.Use(owner, angle, ammoType);
        }
    }
}
