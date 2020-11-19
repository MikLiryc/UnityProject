using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletSpawner;
    [SerializeField] float fireRate = 1.0f;

    BulletManager bP;

    private Transform firePoint;
    private float lastFire = 0.0f;

    private void OnEnable()
    {
        bP = GameObject.Find("BulletManager").GetComponent<BulletManager>();
        firePoint = transform.GetChild(0);
        
        StartCoroutine(FireCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            TryFire();

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void TryFire()
    {
        if (bP.bulletPool.Count > 0)
        {
            Fire();
        }
        else
        {
            bP.CreateBullet();
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = bP.bulletPool.Dequeue();
        bullet.transform.position = firePoint.position;
        bullet.transform.up = firePoint.up;
        bullet.SetActive(true);
    }
}
