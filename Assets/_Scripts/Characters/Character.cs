using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable
{
    public AudioSource Audio { get; set; }
    [field: SerializeField] public Stats Stats { get; set; }
    private Rigidbody2D rb;
    protected Animator Animator { get; set; }
    public SpriteRenderer Visuals { get; set; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Visuals = GetComponentInChildren<SpriteRenderer>();
        Audio = GetComponentInChildren<AudioSource>();
        Stats.InitSpeed = Stats.Speed;
    }

    public abstract void Damage(float damage);
    public virtual void Move(Vector2 move)
    {
        rb.MovePosition(rb.position + (Stats.Speed * Time.deltaTime * move));
        Animator.SetFloat("Speed", move.magnitude);
    }
}

public interface IDamagable
{
    void Damage(float damage);
}

[System.Serializable]
public class Stats
{
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    public float InitSpeed { get; set; }
}