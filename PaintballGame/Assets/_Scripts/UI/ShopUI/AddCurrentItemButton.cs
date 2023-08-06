using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AddCurrentItemButton : NetworkBehaviour
{
    [SerializeField] private ItemType _type;

    private ItemData _item;

    enum ItemType
    {
        Weapon,
        Ammo,
        Armor,
        Tool
    }

    public void SetItem(ItemData item)
    {
        _item = item;
    }

    public void AddItem()
    {
        if (NetworkClient.localPlayer == null)
        {
            return;
        }

        AgentInventoryManager inventory = NetworkClient.localPlayer.GetComponent<AgentInventoryManager>();

        switch (_type)
        {
            case ItemType.Weapon:
                {
                    if (_item is WeaponDataSO)
                    {
                        inventory.Weapons.Add(_item as WeaponDataSO);
                    }
                }
                break;
            case ItemType.Ammo:
                {
                    if (_item is BulletDataSO)
                    {
                        inventory.BulletData = _item as BulletDataSO;
                    }
                }
                break;
            case ItemType.Armor:
                {
                    if (_item is ArmorDataSO)
                    {
                        inventory.ArmorData = _item as ArmorDataSO;
                    }
                }
                break;
            case ItemType.Tool:
                {
                    if (_item is ToolDataSO)
                    {
                        inventory.Tools.Add(_item as ToolDataSO);
                    }
                }
                break;
            default:
                break;
        }
    }
}
