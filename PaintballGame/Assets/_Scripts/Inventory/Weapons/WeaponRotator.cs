using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : NetworkBehaviour
{
    [SerializeField] private Transform _rotateThis;
    [SerializeField] private SpriteRenderer _weaponRenderer;

    [SyncVar] private Vector2 _dir = Vector2.right;

    private IInputtable _agentInput;
    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        _weaponSwitcher = GetComponentInChildren<WeaponSwitcher>();
        _agentInput = transform.root.GetComponent<IInputtable>();
    }

    private void Start()
    {
        _weaponSwitcher.OnWeaponEnable += (Weapon w) => _weaponRenderer = w.GetComponent<SpriteRenderer>();
        _weaponRenderer = _weaponSwitcher.CurrentWeapon.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _dir = RotateGun();
        
        FlipGun(_dir);
        HideGun(_dir);
        UpdateDir(_dir);
    }

    private void HideGun(Vector2 dir)
    {
        _weaponRenderer = _weaponSwitcher.CurrentWeapon.GetComponentInChildren<SpriteRenderer>();

        bool hide = Vector2.Dot(Vector2.up, dir) > 0;
        _weaponRenderer.sortingOrder = hide ? -1 : 1;
    }

    public Vector2 GetDir()
    {
        return _dir.normalized;
    }


    private void UpdateDir(Vector2 dir)
    {
        _dir = dir;
    }

    private void FlipGun(Vector2 dir)
    {
        bool flipThis = Vector2.Dot(dir, Vector2.right) < 0;
        transform.localScale = new Vector3(transform.localScale.x, flipThis ? -1 : 1, transform.localScale.z);
    }

    private Vector2 RotateGun()
    {
        Vector2 dir = NewMethod();
        Quaternion rotation = new Quaternion { eulerAngles = new(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) };
        transform.rotation = rotation;
        return dir;
    }

    private Vector2 NewMethod()
    {
        return _agentInput.GetMousePositionWorld() - (Vector2)transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if (_agentInput != null)
            Gizmos.DrawSphere(_agentInput.GetMousePositionWorld(), 1f);
    }
}
