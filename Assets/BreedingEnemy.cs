using UnityEngine;
public class BreedingEnemy : Enemy
{
    [SerializeField] private BreedBehaviour breeder;
    protected override void OnDeath()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(breeder.gameObject, (Vector2)transform.position + (Random.insideUnitCircle * 1.25f), Quaternion.identity);
        }
        base.OnDeath();
    }
}