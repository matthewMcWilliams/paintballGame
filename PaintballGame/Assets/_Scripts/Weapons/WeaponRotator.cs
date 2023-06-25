using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private Transform _rotateThis;
    [SerializeField] private AgentInput _agentInput;

    private Vector2 dir = Vector2.right;

    private void Update()
    {
        dir = RotateGun();
        FlipGun(dir);
    }

    public Vector2 GetDir()
    {
        return dir.normalized;
    }

    private void FlipGun(Vector2 dir)
    {
        bool flipThis = Vector2.Dot(dir, Vector2.right) < 0;
        transform.localScale = new Vector3(transform.localScale.x, flipThis ? -1 : 1, transform.localScale.z);
    }

    private Vector2 RotateGun()
    {
        Vector2 dir = _agentInput.GetMousePositionWorld() - (Vector2)transform.position;
        Quaternion rotation = new Quaternion { eulerAngles = new(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) };
        transform.rotation = rotation;
        return dir;
    }
}
