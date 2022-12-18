using UnityEngine;
using System.Collections;
public abstract class WeaponBase : ScriptableObject
{
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
    public bool IsChamber => isChamber;
    public float Cooldown => m_Cooldown;
    [Min(0)]
    [SerializeField] protected float m_Cooldown;
    [SerializeField] private Sprite sprite;
    [SerializeField] private new string name;
    [SerializeField] private int capacity;
    [SerializeField] private bool isChamber;
    [SerializeField] private AudioEvent insertSound;
    [SerializeField] private AudioEvent cockSound;
    [SerializeField] private AudioEvent ammoOut;
    
    public string Name => name;
    public int Capacity => capacity;
    public abstract void Use(Player owner, float angle, AmmoBase ammoType);

    [SerializeField] protected float m_Damage;

    public abstract void OnCockLoaded(Player owner);
    [SerializeField] private AudioEvent openSound;
    [SerializeField] private AudioEvent closeSound;
    public AudioEvent InsertSound => insertSound;
    public AudioEvent OpenSound => openSound;
    public AudioEvent CloseSound => closeSound;
    public AudioEvent CockSound => cockSound;
    public AudioEvent AmmoOutSound => ammoOut;
    public Sprite @Sprite => sprite;
}
