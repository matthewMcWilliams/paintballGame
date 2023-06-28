using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInput : MonoBehaviour
{
    [SerializeField] private string _horizontalInputName = "Horizontal";
    [SerializeField] private string _verticalInputName = "Vertical";
    [SerializeField] private string _fireName = "Fire1";
    [SerializeField] private string _switchWeaponAxisName = "ScrollUp";

    private Vector2 _input;
    private Vector2 _worldMousePos;
    private bool _fireHold;
    private bool _firePress;
    private int _switchWeapon;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
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
        _worldMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CalculateMovement()
    {
        _input = new Vector2(
            Input.GetAxisRaw(_horizontalInputName),
            Input.GetAxisRaw(_verticalInputName)
            );
    }
}
