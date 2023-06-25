using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeaponParent : MonoBehaviour
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
        if (_agentInput.GetFiring())
        {
            _weapon.Fire();
        }
    }
}
