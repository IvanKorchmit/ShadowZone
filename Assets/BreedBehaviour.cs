using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FoW;

public class BreedBehaviour : MonoBehaviour
{
    [SerializeField] private Enemy enemyToSpawn;
    [SerializeField] private float timer;
    private void Update()
    {
        bool isinfog = FogOfWarTeam.GetTeam(0).GetFogValue(transform.position) == 255;

        if (isinfog)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Instantiate(enemyToSpawn.gameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
