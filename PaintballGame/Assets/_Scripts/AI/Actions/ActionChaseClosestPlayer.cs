using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChaseClosestPlayer : Action
{
    private InputData _inputData;
    [SerializeField, Range(0f, 1f)] private float _chaseSpeed = 1f;

    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
        
    }

    public override void TakeAction()
    {
        Transform player = PlayerManager.Instance.FindClosestOpponent(transform);
        if (player == null)
            return;
        _inputData.MovementInput = (player.position - transform.position).normalized * _chaseSpeed;
    }
}
