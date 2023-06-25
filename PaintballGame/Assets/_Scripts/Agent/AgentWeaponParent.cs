using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponParent : NetworkBehaviour
{
    [SerializeField] private AgentInput _agentInput;

    private Weapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        CheckForInput();
    }

    private void CheckForInput()
    {
        if (isLocalPlayer && _agentInput.GetFiring())
        {
            CmdFire();
        }
    }

    [Command]
    private void CmdFire()
    {
        _weapon.Fire();
    }
}
