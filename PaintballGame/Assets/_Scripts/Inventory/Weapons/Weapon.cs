using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public WeaponDataSO WeaponData;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private AgentWeaponParent _weaponParent;
    [SerializeField] private WeaponRotator _weaponRotator;
    [SerializeField] private UnityEvent _onShoot;

    private bool _canShoot = true;

    private AgentInventoryManager _inventory;


    private void Awake()
    {
        _weaponRotator ??= GetComponentInParent<WeaponRotator>();
        _weaponParent ??= GetComponentInParent<AgentWeaponParent>();
        _inventory = transform.root.GetComponent<AgentInventoryManager>();
    }

    private void Start()
    {
        if (isActiveAndEnabled)
            _canShoot = true;
    }

    public void Fire(BulletDataSO bulletData)
    {
        Debug.Log(_weaponParent.Inventory.AmmoCount);
        if (!_canShoot || !isActiveAndEnabled || _weaponParent.Inventory.AmmoCount <= 0)
        {
            return;
        }
        var bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity, transform.root.parent);
        var spread = GetSpread();
        bullet.GetComponent<Rigidbody2D>().velocity = spread * (bulletData.Speed + WeaponData.Speed) / 2;
        if (bullet.TryGetComponent(out Bullet bulletScript))
        {
            bulletScript.BulletData = _inventory.BulletData;
            bulletScript.DistanceToTravel = WeaponData.distance;
        }
        NetworkServer.Spawn(bullet);
        _weaponParent.Inventory.Shoot();
        _onShoot?.Invoke();

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
