using UnityEngine;

[CreateAssetMenu]
public class RegularAmmo : AmmoBase
{
    public override void OnEnemyImpact(Enemy enemy)
    {
        enemy.Damage(Damage);
    }
}
