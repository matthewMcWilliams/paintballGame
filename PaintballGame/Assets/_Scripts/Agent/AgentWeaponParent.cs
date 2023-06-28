using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponParent : NetworkBehaviour
{
    public Weapon CurrentWeapon { get; private set; }
    [field: SerializeField] public AgentInventoryManager Inventory { get; private set; }

    [SerializeField] private AgentInput _agentInput;

    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        CurrentWeapon = GetComponentInChildren<Weapon>();
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
    }

    private void Start()
    {
        _weaponSwitcher.OnWeaponEnable += WeaponSwitcher_OnWeaponEnable;
    }

    private void WeaponSwitcher_OnWeaponEnable(Weapon weapon)
    {
        CurrentWeapon.Disable();
        CurrentWeapon = weapon;
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (!isLocalPlayer)
            return;
        if (_agentInput.GetFireHold() && CurrentWeapon.WeaponData.autoFire)
        {
            CmdFire();
        }
        else if (!CurrentWeapon.WeaponData.autoFire && _agentInput.GetFirePress())
        {
            CmdFire();
        }
    }

    [Command]
    private void CmdFire()
    {
        Debug.Log(Inventory.AmmoCount);
        CurrentWeapon.Fire();
    }
}
