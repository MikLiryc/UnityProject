using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] GameObject bulletSpawner;
    [SerializeField] float fireRate = 1.0f;
    
    private Transform firePoint;
    private float lastFire = 0.0f;

    private void Start()
    {
        firePoint = transform.GetChild(0);
    }
    void Update()
    {
        if (lastFire + fireRate < Time.time)
        {
            GameObject bullet = Instantiate(bulletSpawner);
            bullet.transform.position = firePoint.position;

            lastFire = Time.time;
        }
    }
}
