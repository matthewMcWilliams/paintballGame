using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour, IInputtable
{
    public InputData inputData;

    bool IInputtable.GetFireHold() => inputData.FireHold;

    bool IInputtable.GetFirePress() => inputData.FirePress;

    Vector2 IInputtable.GetMousePositionWorld() => inputData.MousePosWorld;

    Vector2 IInputtable.GetMovementInput() => inputData.MovementInput;

    int IInputtable.GetSwitchWeapon() => inputData.SwitchWeaponInput;
}
