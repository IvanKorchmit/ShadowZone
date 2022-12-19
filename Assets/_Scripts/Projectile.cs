using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AmmoBase ammo;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    public void Initialize(AmmoBase ammo)
    {
        this.ammo = ammo;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            transform.DetachChildren();
            Destroy(gameObject);
            return;
        }
        if (!collision.CompareTag("Player") && collision.TryGetComponent(out IDamagable damagable))
        {
            ammo.OnEnemyImpact(this, damagable as Enemy);
            transform.DetachChildren();
            Destroy(gameObject);
        }
        

    }
}
