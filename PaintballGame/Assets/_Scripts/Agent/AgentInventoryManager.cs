using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInventoryManager : NetworkBehaviour
{
    // TODO: Refactor this and make it more flexible
    // It might be best to use a scriptable object to keep up with the stats.

    [field: SyncVar] public int AmmoCount { get; private set; }
    public BulletDataSO BulletData { get => _bulletData; }
    public ArmorDataSO ArmorData { get => _armorData; }
    public List<WeaponDataSO> Weapons { get => _weaponData; }
    public List<ToolDataSO> Tools { get => _tools; }

    public int CurrentWeaponIndex;

    [SerializeField] private List<WeaponDataSO> _weaponData;
    [SerializeField] private List<ToolDataSO> _tools;
    [SerializeField] private BulletDataSO _bulletData;
    [SerializeField] private ArmorDataSO _armorData;

    private void Start()
    {
        AmmoCount = _bulletData.Capacity;
    }

    public void Shoot()
    {
        AmmoCount--;
    }

    public void UseTool(int index)
    {
        Tools[index].UseTool(transform);
    }
}
