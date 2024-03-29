using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponParent : NetworkBehaviour
{
    public Weapon CurrentWeapon => _weaponSwitcher.CurrentWeapon;
    [field: SerializeField] public AgentInventoryManager Inventory { get; private set; }

    private IInputtable _agentInput;

    private WeaponSwitcher _weaponSwitcher;

    private void Awake()
    {
        _weaponSwitcher = GetComponent<WeaponSwitcher>();
        _agentInput = transform.root.GetComponent<IInputtable>();
    }

    private void Start()
    {
        _weaponSwitcher.OnWeaponEnable += WeaponSwitcher_OnWeaponEnable;
    }

    private void WeaponSwitcher_OnWeaponEnable(Weapon weapon)
    {
        CurrentWeapon.Disable();
    }

    private void Update()
    {
        if (!GameManager.Instance.CountdownFinished && GameManager.Instance.PlayersInPosition)
        {
            return;
        }
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (!isServer)
            return;
        if (_agentInput.GetFireHold() && CurrentWeapon.WeaponData.autoFire)
        {
            Fire();
        }
        else if (!CurrentWeapon.WeaponData.autoFire && _agentInput.GetFirePress())
        {
            Fire();
        }
    }

    private void Fire()
    {
        //Debug.Log(Inventory.AmmoCount);
        if (isServer)
            CurrentWeapon.Fire(Inventory.BulletData);

    }
}
