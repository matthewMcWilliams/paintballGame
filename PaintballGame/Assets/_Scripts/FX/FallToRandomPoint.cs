using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallToRandomPoint : MonoBehaviour
{
    private const float _checkRadius = 0.1f;

    [SerializeField] private float _minAngle, _maxAngle;
    [SerializeField] private float _minDistance, _maxDistance;
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _onSplat;

    private Vector2 _targetPosition;

    private void Start()
    {
        float angle = Random.Range(_minAngle, _maxAngle);
        float distance = Random.Range(_minDistance, _maxDistance);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        _targetPosition = (Vector2)transform.position + direction;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        if (Vector3.Distance(_targetPosition, transform.position) < _checkRadius)
        {
            _onSplat?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, new Vector3(Mathf.Cos(_minAngle), Mathf.Sin(_minAngle)) * _maxDistance);
        Gizmos.DrawRay(transform.position, new Vector3(Mathf.Cos(_maxAngle), Mathf.Sin(_maxAngle)) * _maxDistance);
    }
}
