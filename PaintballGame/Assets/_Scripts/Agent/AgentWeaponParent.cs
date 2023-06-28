using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponParent : NetworkBehaviour
{
    [SerializeField] private AgentInput _agentInput;

    private Weapon _currentWeapon;
    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
    }

    private void Start()
    {
        _weaponSwitcher.OnWeaponEnable += WeaponSwitcher_OnWeaponEnable;
    }

    private void WeaponSwitcher_OnWeaponEnable(Weapon weapon)
    {
        _currentWeapon.Disable();
        _currentWeapon = weapon;
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (!isLocalPlayer)
            return;
        if (_agentInput.GetFireHold() && _currentWeapon.WeaponData.autoFire)
        {
            CmdFire();
        }
        else if (!_currentWeapon.WeaponData.autoFire && _agentInput.GetFirePress())
        {
            CmdFire();
        }
    }

    [Command]
    private void CmdFire()
    {
        RPCFire();
    }

    [ClientRpc]
    private void RPCFire()
    {
        Debug.Log(_currentWeapon);
        _currentWeapon.Fire();
    }
}
