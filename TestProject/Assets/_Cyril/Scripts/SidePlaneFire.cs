using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlaneFire : MonoBehaviour
{
    public GameObject bulletSpawner;
    public GameObject firePoint;

    [SerializeField] float fireRate;
    float lastFire;

    private void Start()
    {
        fireRate = 0.5f;
        lastFire = 0f;
    }

    void Update()
    {
        if (lastFire + fireRate < Time.time)
        {
            GameObject bullet = Instantiate(bulletSpawner);
            bullet.transform.position = firePoint.transform.position;
            lastFire = Time.time;
        }

        //Invoke("fire", fireRate);
    }

    private void fire()
    {
        GameObject bullet = Instantiate(bulletSpawner);
        bullet.transform.position = firePoint.transform.position;
    }
}
