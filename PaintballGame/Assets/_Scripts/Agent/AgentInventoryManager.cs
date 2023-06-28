using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInventoryManager : MonoBehaviour
{
    // TODO: Refactor this and make it more flexible
    // It might be best to use a scriptable object to keep up with the stats.

    public int AmmoCount { get; private set; }

    private WeaponDataSO _weaponData;

    private void Start()
    {
        _weaponData = GetComponentInChildren<AgentWeaponParent>().CurrentWeapon.WeaponData;
        AmmoCount = _weaponData.ammoCapacity;
    }

    public void Shoot()
    {
        AmmoCount--;
    }
}
