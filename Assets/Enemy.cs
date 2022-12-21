using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Character
{
    private float oldHealth;
    [SerializeField] private AudioEvent hitSounds;
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private Seeker seeker;
    [SerializeField] private LineOfSight los;
    [SerializeField] private AudioEvent spotSounds;
    private AIPath aiPath;
    public override void Damage(float damage)
    {
        Stats.Health -= damage;
    }
    protected override void Start()
    {
        base.Start();
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = Stats.Speed;
        los = GetComponent<LineOfSight>();
        seeker = GetComponent<Seeker>();
        los.OnPlayerSpot += Los_OnPlayerSpot;
        if (Player.Singleton != null)
        {
            seeker.StartPath(transform.position, Player.Singleton.transform.position);
        }
        InvokeRepeating(nameof(FindPlayer), 0, 15);
    }
    private void FindPlayer()
    {
        if (Player.Singleton == null) return;
        seeker.StartPath(transform.position, Player.Singleton.transform.position);
    }
    private void Los_OnPlayerSpot()
    {
        spotSounds.Play(Audio);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out IDamagable dmg))
        {
            dmg.Damage(33);
        }
    }
    
    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (los.Sees && Player.Singleton != null)
        {
            seeker.StartPath(transform.position, Player.Singleton.transform.position);
        }
        if (Player.Singleton == null || (!los.Sees && Player.Singleton != null && !Player.Singleton.gameObject.activeSelf && aiPath.reachedEndOfPath))
        {
            RandomPath rand = RandomPath.Construct(transform.position, 100000);
            seeker.StartPath(rand);
        }
        Animator.SetFloat("Speed", aiPath.velocity.normalized.magnitude);
        Visuals.flipX = aiPath.velocity.x < 0;
        if (Stats.Health < oldHealth)
        {
            hitSounds.Play(Audio);
            Instantiate(bloodParticle.gameObject, transform.position, Quaternion.identity);
        }
        if (Stats.Health <= 0)
        {
            OnDeath();
            return;
        }
        oldHealth = Stats.Health;
    }
}
