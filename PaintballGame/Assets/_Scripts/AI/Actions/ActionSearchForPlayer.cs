using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionSearchForPlayer : Action
{
    [SerializeField] private float distanceToTurn = 3f;

    private Vector2 _targetPosition;
    private InputData _inputData;

    private void Awake()
    {
        _inputData = transform.root.GetComponent<InputData>();
    }

    private void Start()
    {
        _targetPosition = PickRandomPosition();
    }

    public override void TakeAction()
    {
        if (Vector2.Distance(_targetPosition, transform.position) < distanceToTurn)
        {
            _targetPosition = PickRandomPosition();
        }

        _inputData.MovementInput = (_targetPosition - (Vector2)transform.position).normalized;
    }

    private Vector2 PickRandomPosition()
    {
        BoundsInt bounds = Generator.Instance.Bounds;
        return new Vector2
        {
            x = Random.Range(bounds.xMin, bounds.xMax),
            y = Random.Range(bounds.yMin, bounds.yMax)
        };
    }
}
