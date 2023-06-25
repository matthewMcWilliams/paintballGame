using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO _weaponData;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;

    private WeaponRotator _weaponRotator;

    private void Awake()
    {
        _weaponRotator = GetComponentInParent<WeaponRotator>();
    }

    public void Fire()
    {
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity, transform.root.parent);
        bullet.GetComponent<Rigidbody2D>().velocity = _weaponRotator.GetDir() * _weaponData.speed;
        NetworkServer.Spawn(bullet);
    }
}
