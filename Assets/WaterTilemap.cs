using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTilemap : MonoBehaviour
{
    [Range(0f, 1f), SerializeField]
    private float slowDownFactor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Stats.Speed = player.Stats.InitSpeed * slowDownFactor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.Stats.Speed = player.Stats.InitSpeed;
        }
    }
}
