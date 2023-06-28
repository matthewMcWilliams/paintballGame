﻿using UnityEngine;

[CreateAssetMenu(fileName ="_WeaponData", menuName ="Weapons/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    // TODO: [Range(0, 100)] public float bounceResistance;
    // TODO: [Range(0, 5)] public float splashRadius;
    [Range(0, 50)] public float speed;
    [Range(0.01f, 1f)] public float rateOfFire;
    public bool autoFire;
    [Range(0.01f, 5f)] public float fallRate;
    [Range(0f, 10f)] public float spreadAngle;
    // TODO: [Range(0f, 10f)] public float weight;
    // TODO: [Range(0f, 100f)] public int ammoCapacity;
    [Range(0f, 1f)] public float switchTime = 0.4f;

}