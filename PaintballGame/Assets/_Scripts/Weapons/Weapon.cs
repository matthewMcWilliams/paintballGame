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
    [SerializeField] private AgentWeaponParent _weaponParent;
    [SerializeField] private WeaponRotator _weaponRotator;

    private bool _canShoot = true;



    private void Awake()
    {
        _weaponRotator ??= GetComponentInParent<WeaponRotator>();
        _weaponParent ??= GetComponentInParent<AgentWeaponParent>();
    }

    private void Start()
    {
        if (isActiveAndEnabled)
            _canShoot = true;
    }

    public void Fire()
    {
        Debug.Log(_weaponParent.Inventory.AmmoCount);
        if (!_canShoot || !isActiveAndEnabled || _weaponParent.Inventory.AmmoCount <= 0)
        {
            return;
        }
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity, transform.root.parent);
        var spread = GetSpread();
        bullet.GetComponent<Rigidbody2D>().velocity = spread * WeaponData.speed;
        NetworkServer.Spawn(bullet);
        _weaponParent.Inventory.Shoot();
        StartCoroutine(ShootCoroutine());
        if (bullet.TryGetComponent(out Bullet bulletScript))
        {
            bulletScript.DistanceToTravel = WeaponData.distance;
        }
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
        if (_weaponParent.Inventory.AmmoCount > 0)
        {
            _canShoot = true; 
        }
    }

    private void OnEnable()
    {
        _canShoot = _weaponParent.Inventory.AmmoCount > 0;
        
    }

    public void Disable()
    {
        _canShoot = _weaponParent.Inventory.AmmoCount > 0;
    }
}
