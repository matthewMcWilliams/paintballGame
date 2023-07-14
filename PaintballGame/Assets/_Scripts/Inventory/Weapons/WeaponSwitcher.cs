using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : NetworkBehaviour
{
    public Weapon CurrentWeapon => _weapons[_inventory.CurrentWeaponIndex].GetComponent<Weapon>();

    public event Action<Weapon> OnWeaponEnable;
    
    [SerializeField] private Transform[] _weapons;

    private IInputtable _agentInput;
    private AgentInventoryManager _inventory;

    private bool _canSwitch = true;

    private void Awake()
    {
        _agentInput = transform.root.GetComponent<IInputtable>();
        _inventory = transform.root.GetComponent<AgentInventoryManager>();
    }

    private static int PositiveModulo(int a, int b)
    {
        a %= b;
        a += b;
        return a % b;
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (_agentInput.GetSwitchWeapon() != 0 && isLocalPlayer && _canSwitch)
        {
            int weaponIndex = PositiveModulo(_inventory.CurrentWeaponIndex + _agentInput.GetSwitchWeapon(), _weapons.Length);
            _inventory.CurrentWeaponIndex = weaponIndex;
            ChangeWeapon(weaponIndex);
            StartCoroutine(SwitchCoroutine());
        }
    }

    private IEnumerator SwitchCoroutine()
    {
        _canSwitch = false;
        yield return new WaitForSeconds(CurrentWeapon.WeaponData.switchTime);
        _canSwitch = true;
    }

    [Command]
    private void ChangeWeapon(int weaponIndex)
    {
        ChangeWeaponClient(weaponIndex);
    }

    [ClientRpc]
    private void ChangeWeaponClient(int weaponIndex)
    {
        _inventory.CurrentWeaponIndex = weaponIndex;
        OnWeaponEnable?.Invoke(CurrentWeapon);
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i == weaponIndex)
            {
                _weapons[i].gameObject.SetActive(true);
            }
            else
            {
                _weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
