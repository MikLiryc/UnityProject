using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float normalFireRate = 1.0f;
    [SerializeField] float rotateFireRate = 2.0f;
    [SerializeField] int HP = 100;
    [SerializeField] GameObject target;
    [SerializeField] int bulletMax = 36;
    public GameObject bossBullet;
    public GameObject bossRotateBullet;
    private float normalLastFire = 3f;
    private float rotateLastFire = 5f;


    // Update is called once per frame
    void Update()
    {
        if (rotateFireRate + rotateLastFire < Time.time)
        {
            rotateFire();
        }
        if (normalFireRate + normalLastFire < Time.time)
        {
            normalFire();
        }
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void normalFire()
    {
        if (target != null)
        {
            GameObject bullet = Instantiate(bossBullet);
            bullet.transform.position = transform.GetChild(0).position;
            bullet.transform.LookAt(target.transform);

            normalLastFire = Time.time;
        }
    }

    private void rotateFire()
    {
        for (int i = 0; i < bulletMax; i++)
        {
            GameObject bBullet = Instantiate(bossRotateBullet);
            bBullet.transform.position = transform.GetChild(0).position;
            bBullet.transform.Rotate(new Vector3(0f, (360.0f / bulletMax) * i, 0f));
        }
        rotateLastFire = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;
        Destroy(other.gameObject);
    }
}
