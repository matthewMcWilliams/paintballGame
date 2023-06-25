using UnityEngine;

[CreateAssetMenu(fileName ="_WeaponData", menuName ="Weapons/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    [Range(0, 100)] public float bounceResistance;
    [Range(0, 5)] public float splashRadius;
    [Range(0, 50)] public float speed;
    [Range(0.01f, 1f)] public float rateOfFire;
    [Range(0.01f, 1f)] public float fallRate;
    [Range(0f, 10f)] public float spreadAngle;
    [Range(0f, 10f)] public float weight;
    [Range(0f, 100f)] public int ammoCapacity;

}