using UnityEngine;

[CreateAssetMenu]
public class RegularAmmo : AmmoBase
{
    [SerializeField] private float knockback;
    public override void OnEnemyImpact(Projectile projectile, Enemy enemy)
    {
        enemy.Damage(Damage);
        Vector2 knockbackVector = -(projectile.transform.position - enemy.transform.position) * knockback;
        enemy.transform.Translate(knockbackVector);
    }
}
