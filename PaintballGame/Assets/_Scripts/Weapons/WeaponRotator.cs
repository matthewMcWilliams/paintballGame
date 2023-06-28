using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : NetworkBehaviour
{
    [SerializeField] private Transform _rotateThis;
    [SerializeField] private AgentInput _agentInput;
    [SerializeField] private SpriteRenderer _weaponRenderer;

    [SyncVar] private Vector2 _dir = Vector2.right;

    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        _weaponSwitcher = GetComponentInChildren<WeaponSwitcher>();
    }

    private void Start()
    {
        _weaponSwitcher.OnWeaponEnable += (Weapon w) => _weaponRenderer = w.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isLocalPlayer || !Application.isFocused)
        {
            return;
        }
        _dir = RotateGun();
        FlipGun(_dir);
        HideGun(_dir);
        UpdateDir(_dir);
    }

    private void HideGun(Vector2 dir)
    {
        bool hide = Vector2.Dot(Vector2.up, dir) > 0;
        _weaponRenderer.sortingOrder = hide ? -1 : 1;
    }

    public Vector2 GetDir()
    {
        return _dir.normalized;
    }

    [Command]
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
        Vector2 dir = _agentInput.GetMousePositionWorld() - (Vector2)transform.position;
        Quaternion rotation = new Quaternion { eulerAngles = new(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) };
        transform.rotation = rotation;
        return dir;
    }
}
