using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : NetworkBehaviour
{
    public Weapon CurrentWeapon => _weapons[_inventory.CurrentWeaponIndex].GetComponent<Weapon>();

    public event Action<Weapon> OnWeaponEnable;
    
    [SerializeField] private List<Transform> _weapons;

    private IInputtable _agentInput;
    private AgentInventoryManager _inventory;

    private bool _canSwitch = true;

    private void Awake()
    {
        _agentInput = transform.root.GetComponent<IInputtable>();
        _inventory = transform.root.GetComponent<AgentInventoryManager>();


        foreach (var weapon in _weapons)
        {
            NetworkServer.UnSpawn(weapon.gameObject);
            //Destroy(weapon);
        }
        _weapons.Clear();


        for (int i = 0; i < _inventory.Weapons.Count; i++)
        {
            if (_weapons.Count < i && _inventory.Weapons[i] != null && _weapons[i].GetComponent<Weapon>().WeaponData == _inventory.Weapons[i])
            {
                continue;
            }
            if (_weapons.Count < i)
            {
                Transform oldWeapon = _weapons[i];
                NetworkServer.UnSpawn(oldWeapon.gameObject);
                Destroy(oldWeapon);
            }

            WeaponDataSO weapon = _inventory.Weapons[i];
            var w = Instantiate(weapon.WeaponPrefab, transform);

            w.GetComponent<Weapon>().WeaponData = weapon;
            _weapons.Add(w.transform);
            NetworkServer.Spawn(w);
        }
    }

    private static int PositiveModulo(int a, int b)
    {
        a %= b;
        a += b;
        return a % b;
    }

    private void Update()
    {
        if (_inventory.Weapons.Count != _weapons.Count)
        {
            Awake();
        }

        CheckForInput();
        ChangeWeapon(_inventory.CurrentWeaponIndex);
    }

    private void CheckForInput()
    {
        if (_agentInput.GetSwitchWeapon() != 0 && isLocalPlayer && _canSwitch)
        {
            int weaponIndex = PositiveModulo(_inventory.CurrentWeaponIndex + _agentInput.GetSwitchWeapon(), _inventory.Weapons.Count);
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


    [ClientRpc]
    private void ChangeWeapon(int weaponIndex)
    {
        _inventory.CurrentWeaponIndex = weaponIndex;
        OnWeaponEnable?.Invoke(CurrentWeapon);
        for (int i = 0; i < _weapons.Count; i++)
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
