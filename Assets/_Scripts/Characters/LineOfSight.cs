using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private LayerMask m_ObstacleMask;
    private bool m_HasSpottedEarlier;
    public event System.Action OnPlayerSpot;
    public bool Sees => m_HasSpottedEarlier;
    private void Update()
    {
        Player pl = Player.Singleton;
        if (pl == null) return;
        Vector2 direction = pl.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector2.Distance(pl.transform.position, transform.position);
        var result = Physics2D.Raycast(transform.position, direction, distance, m_ObstacleMask);
        if (!m_HasSpottedEarlier && result.collider == null)
        {
            OnPlayerSpot?.Invoke();
            m_HasSpottedEarlier = true;
        }
    }
}
