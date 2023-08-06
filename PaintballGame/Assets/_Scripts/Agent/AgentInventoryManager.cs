using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInventoryManager : NetworkBehaviour
{
    [field: SyncVar] public int AmmoCount { get; private set; }
    public BulletDataSO BulletData { get => _bulletData; set => _bulletData = value; }
    public ArmorDataSO ArmorData { get => _armorData; set => _armorData = value; }
    public List<WeaponDataSO> Weapons { get => _weaponData; set => _weaponData = value; }
    public List<ToolDataSO> Tools { get => _tools; set => _tools = value; }

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
