using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolPanelScript : NetworkBehaviour
{
    [SerializeField] private int _index;
    private AgentInventoryManager _inventory;

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
        
        // Functionality goes here


    }

    public void OnClicked()
    {
        if (_inventory == null)
        {
            OnStartClient();
            return;
        }

        _inventory.UseTool(_index);
    }
}
