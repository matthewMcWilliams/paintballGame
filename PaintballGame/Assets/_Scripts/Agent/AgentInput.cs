using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AgentInput : NetworkBehaviour, IInputtable
{
    [SerializeField] private string _horizontalInputName = "Horizontal";
    [SerializeField] private string _verticalInputName = "Vertical";
    [SerializeField] private string _fireName = "Fire1";
    [SerializeField] private string _switchWeaponAxisName = "ScrollUp";

    [SyncVar] private Vector2 _input;
    [SyncVar] private Vector2 _worldMousePos;
    [SyncVar] private bool _fireHold;
    [SyncVar] private bool _firePress;
    [SyncVar] private int _switchWeapon;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        CalculateMovement();
        CalculateMousePos();
        CalculateFireHold();
        CalculateFirePress();
        CalculateSwitchWeapon();
    }

    public Vector2 GetMovementInput()
    {
        return _input;
    }

    public Vector2 GetMousePositionWorld()
    {
        return _worldMousePos;
    }

    public bool GetFirePress()
    {
        return _firePress;
    }

    public bool GetFireHold()
    {
        return _fireHold;
    }

    public int GetSwitchWeapon()
    {
        return _switchWeapon;
    }

    private void CalculateFirePress()
    {
        _firePress = Input.GetButtonDown(_fireName);
    }

    private void CalculateSwitchWeapon()
    {
        int positive = 0, negative = 0;
        if (!string.IsNullOrEmpty(_switchWeaponAxisName))
        {
            positive = Input.GetAxisRaw(_switchWeaponAxisName) > 0.01f ? 1 : 0;
            negative = Input.GetAxisRaw(_switchWeaponAxisName) < -0.01f ? 1 : 0;
        }
        _switchWeapon = positive - negative;
    }

    private void CalculateFireHold()
    {
        _fireHold = Input.GetButton(_fireName);
    }

    private void CalculateMousePos()
    {
        Vector2 pos = Input.mousePosition;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        _worldMousePos = _camera.ScreenToWorldPoint(pos);
    }

    private void CalculateMovement()
    {
        _input = new Vector2(
            Input.GetAxisRaw(_horizontalInputName),
            Input.GetAxisRaw(_verticalInputName)
            );
        if (_input.sqrMagnitude > 1)
        {
            _input.Normalize();
        }
    }
}
