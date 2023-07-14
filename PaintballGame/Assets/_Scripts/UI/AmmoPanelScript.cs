using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanelScript : NetworkBehaviour
{
    private AgentInventoryManager _inventory;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _defaultColor = Color.gray, _noAmmoColor = Color.red;
    [SerializeField] private Image _ammoPanel;

    public override void OnStartClient()
    {
        var localPlayer = NetworkClient.localPlayer;
        if (localPlayer == null)
        {
            return;
        }
        _inventory = localPlayer.GetComponent<AgentInventoryManager>();
    }

    private void Update()
    {
        if (_inventory == null)
        {
            OnStartClient();
            return;
        }
        SetBulletText();
        SetColor();
    }

    private void SetColor()
    {
        if (_inventory.AmmoCount > 0)
        {
            _ammoPanel.color = _defaultColor; 
        } else
        {
            _ammoPanel.color = _noAmmoColor;
        }
    }

    private void SetBulletText()
    {
        _text.text = _inventory.AmmoCount.ToString();
    }
}
