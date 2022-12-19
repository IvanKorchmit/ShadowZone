using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private LayerMask m_ObstacleMask;
    private bool m_HasSpottedEarlier;
    public event System.Action OnPlayerSpot;
    public bool Sees => isCurrentlySeeing;
    [SerializeField]
    private float distance;
    [SerializeField] private bool isCurrentlySeeing;
    private void Update()
    {
        Player pl = Player.Singleton;
        if (pl == null || !pl.gameObject.activeSelf) return;
        Vector2 direction = pl.transform.position - transform.position;
        direction.Normalize();

        float distance = Vector2.Distance(pl.transform.position, transform.position);
        var result = Physics2D.Raycast(transform.position, direction, distance, m_ObstacleMask);
        isCurrentlySeeing = result.collider == null && distance <= this.distance;
        if (!m_HasSpottedEarlier && isCurrentlySeeing)
        {
            OnPlayerSpot?.Invoke();
            m_HasSpottedEarlier = true;
        }
    }
}
