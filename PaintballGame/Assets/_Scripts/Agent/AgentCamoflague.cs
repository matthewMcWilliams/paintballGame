using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCamoflague : MonoBehaviour
{
    [SerializeField, Min(0)] private float _waitMultiplier = 1f;

    private float _timer = 0f, _hidingTimer = 0f;

    private IInputtable _input;
    private AgentInventoryManager _inventory;
    private AgentRenderer _renderer;

    private void Awake()
    {
        _input = transform.root.GetComponent<IInputtable>();
        _inventory = transform.root.GetComponent<AgentInventoryManager>();
        _renderer = GetComponent<AgentRenderer>();
    }

    private void Update()
    {
        _hidingTimer -= Time.deltaTime;

        if (_input.GetMovementInput().sqrMagnitude < 0.1f && !_input.GetFireHold() && !_input.GetFirePress())
        {
            // The agent is not moving or shooting.
            _timer += Time.deltaTime;
        }
        else
        {
            // The agent is moving and/or shooting.
            _timer = 0f;
            _renderer.Visible = true;
        }

        if (_timer > CalculateWaitTime(_inventory.ArmorData.CamoFactor) || _hidingTimer > 0)
        {
            _renderer.Visible = false;
        }
    }

    public void HideForSeconds(float seconds)
    {
        _hidingTimer = seconds;
    }

    private float CalculateWaitTime(float camoFactor) => (1 / camoFactor) * _waitMultiplier;
}
