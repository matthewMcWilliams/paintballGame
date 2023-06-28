using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponDataSO WeaponData;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;

    private bool _canShoot = true;

    private WeaponRotator _weaponRotator;


    private void Awake()
    {
        _weaponRotator = GetComponentInParent<WeaponRotator>();
    }

    public void Fire()
    {
        if (!_canShoot || !isActiveAndEnabled)
        {
            return;
        }
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity, transform.root.parent);
        var spread = GetSpread();
        bullet.GetComponent<Rigidbody2D>().velocity = spread * WeaponData.speed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1f / WeaponData.fallRate);
        StartCoroutine(ShootCoroutine());
    }

    private Vector2 GetSpread()
    {
        var currentAngle = Vector2.SignedAngle(Vector2.right, _weaponRotator.GetDir());
        var angleDif = UnityEngine.Random.Range(currentAngle - WeaponData.spreadAngle, currentAngle + WeaponData.spreadAngle) * Mathf.Deg2Rad;
        Vector2 newDir = new(Mathf.Cos(angleDif), Mathf.Sin(angleDif));
        return newDir;
    }

    private IEnumerator ShootCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(WeaponData.rateOfFire);
        _canShoot = true;
    }

    public void Disable()
    {
        _canShoot = true;
    }
}
