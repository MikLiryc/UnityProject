using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlaneFire : MonoBehaviour
{
    public GameObject bulletSpawner;
    public GameObject firePoint;

    public Queue<GameObject> bulletPool;

    private int poolSize = 30;

    [SerializeField] float fireRate;

    private void Awake()
    {
        fireRate = 0.5f;

        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateBullet();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            TryFire();

            yield return new WaitForSeconds(fireRate);
        }
    }

    void TryFire()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.up = firePoint.transform.up;
        }
        else
        {
            CreateBullet();
            Fire();
        }
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletSpawner);
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private void Fire()
    {
        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.up = firePoint.transform.up;
    }
}
