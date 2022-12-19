using UnityEngine;

[CreateAssetMenu]
public class FlammableAmmo : AmmoBase
{
    public override void OnEnemyImpact(Projectile projectile, Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}