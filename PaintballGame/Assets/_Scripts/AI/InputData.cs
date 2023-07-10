using UnityEngine;

public class InputData : MonoBehaviour
{
    public Vector2 MovementInput { set; get; }
    public int SwitchWeaponInput { get; set; }
    public Vector2 MousePosWorld { get; set; }
    public bool FirePress { get; set; }
    public bool FireHold { get; set; }
}