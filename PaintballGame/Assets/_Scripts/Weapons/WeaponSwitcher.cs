using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : NetworkBehaviour
{
    public Weapon CurrentWeapon => _weapons[_currentWeaponIndex].GetComponent<Weapon>();

    public event Action<Weapon> OnWeaponEnable;
    
    [SerializeField] private int _currentWeaponIndex;
    [SerializeField] private Transform[] _weapons;
    [SerializeField] private AgentInput _agentInput;

    

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
        if (_agentInput.GetSwitchWeapon() != 0 && isLocalPlayer)
        {
            int weaponIndex = PositiveModulo(_currentWeaponIndex + _agentInput.GetSwitchWeapon(), _weapons.Length);
            _currentWeaponIndex = weaponIndex;
            ChangeWeapon(weaponIndex);
        }
    }

    [Command]
    private void ChangeWeapon(int weaponIndex)
    {
        ChangeWeaponClient(weaponIndex);
    }

    [ClientRpc]
    private void ChangeWeaponClient(int weaponIndex)
    {
        _currentWeaponIndex = weaponIndex;
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
