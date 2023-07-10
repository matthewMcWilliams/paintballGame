using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionSuccessOnClosestPlayerDie : Decision
{
    private AgentHitbox _closestPlayer;
    private Node.Status _status = Node.Status.RUNNING;

    public override Node.Status MakeDecision()
    {
        var oldClosestPlayer = _closestPlayer;
        var otherPlayerTransform = PlayerManager.Instance.FindClosestPlayer(transform);
        if (otherPlayerTransform == null)
            return Node.Status.SUCCESS;

        _closestPlayer = otherPlayerTransform.GetComponentInChildren<AgentHitbox>();

        if (_closestPlayer == null)
            return Node.Status.SUCCESS;

        if (oldClosestPlayer != _closestPlayer && _closestPlayer != null)
        {
            _closestPlayer.OnDie += ClosestPlayer_OnDie;
            if (oldClosestPlayer != null)
            {
                StartCoroutine(UnsubscribeFromOldPlayer(oldClosestPlayer));
            }
        }
        return _status;
    }

    private IEnumerator UnsubscribeFromOldPlayer(AgentHitbox oldClosestPlayer)
    {
        yield return new WaitForSeconds(1f);
        oldClosestPlayer.OnDie -= ClosestPlayer_OnDie;
    }

    private void ClosestPlayer_OnDie()
    {
        _status = Node.Status.SUCCESS;
    }
}
