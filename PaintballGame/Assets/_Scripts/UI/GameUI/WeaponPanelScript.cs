using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanelScript : NetworkBehaviour
{
    [SerializeField] private Image _weaponImage, _weaponPanel;
    [SerializeField] private int _index;
    [SerializeField] private Color _selected = Color.white, _notSelected = Color.grey;

    private AgentInventoryManager _agentInventoryManager;

    public override void OnStartClient()
    {
        var localPlayer = NetworkClient.localPlayer;
        if (localPlayer == null)
        {
            return;
        }
        _agentInventoryManager = localPlayer.GetComponent<AgentInventoryManager>();
        UpdateWeapon();
        UpdateCurrent();
    }

    private void UpdateCurrent()
    {
        if (_agentInventoryManager.CurrentWeaponIndex == _index - 1)
        {
            _weaponPanel.color = _selected;
        } else
        {
            _weaponPanel.color = _notSelected;
        }
    }

    private void UpdateWeapon()
    {
        if (_agentInventoryManager.Weapons.Count > _index - 1)
        {
            _weaponImage.color = Color.white;
            _weaponImage.sprite = _agentInventoryManager.Weapons[_index - 1].WeaponSprite;
        }
        else
        {
            _weaponImage.color = Color.clear;
        }
    }

    private void Update()
    {
        if (_agentInventoryManager == null)
        {
            OnStartClient();
            return;
        }

        UpdateWeapon();
        UpdateCurrent();


    }
}
