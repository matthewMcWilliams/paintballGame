using UnityEngine;

public interface IInputtable
{
    bool GetFireHold();
    bool GetFirePress();
    Vector2 GetMousePositionWorld();
    Vector2 GetMovementInput();
    int GetSwitchWeapon();
}