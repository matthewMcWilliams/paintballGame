using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DecisionPlayerInRange : Decision
{
    [SerializeField] private float _checkDistance;
    [SerializeField] private Node.Status _statusOnEnter = Node.Status.SUCCESS;
    public override Node.Status MakeDecision()
    {
        foreach (var player in PlayerManager.Instance.Players)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance < _checkDistance && distance != 0)
            {
                return _statusOnEnter;
            }
        }
        return Node.Status.RUNNING;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _checkDistance);
    }
}
