using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentInput : MonoBehaviour
{
    [SerializeField] private string _horizontalInputName = "Horizontal";
    [SerializeField] private string _verticalInputName = "Vertical";
    [SerializeField] private string _fireName = "Fire1";

    private Vector2 _input;
    private Vector2 _worldMousePos;
    private bool _fire;

    private Camera _camera;


    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CalculateMovement();
        CalculateMousePos();
        CalculateFire();
    }

    private void CalculateFire()
    {
        _fire = Input.GetButtonDown(_fireName);
    }

    public Vector2 GetMovementInput()
    {
        return _input;
    }

    public Vector2 GetMousePositionWorld()
    {
        return _worldMousePos;
    }

    public bool GetFiring()
    {
        return _fire;
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
