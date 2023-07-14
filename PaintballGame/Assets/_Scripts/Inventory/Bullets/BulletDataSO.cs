using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_BulletData", menuName = "Weapons/Bullet")]
public class BulletDataSO : ScriptableObject
{
    [Range(0f, 1f)] public float BounceResistance;
    [Range(0, 100)] public int Capacity; 
    [Range(0, 50)] public float Speed;

}
