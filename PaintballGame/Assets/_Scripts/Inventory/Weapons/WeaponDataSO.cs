using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="_WeaponData", menuName ="Weapons/Weapon")]
public class WeaponDataSO : ItemData
{
    public Sprite WeaponSprite;
    public GameObject WeaponPrefab;
    // TODO: [Range(0, 5)] public float splashRadius;
    [Range(0, 50)] public float Speed;
    [Range(0.01f, 1f)] public float RateOfFire;
    public bool autoFire;
    [Range(0.01f, 15f)] public float distance;
    [Range(0f, 10f)] public float spreadAngle;
    // TODO: [Range(0f, 10f)] public float weight;
    [Range(0f, 1f)] public float switchTime = 0.4f;
}