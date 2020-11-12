using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float fireRate = 2.0f;
    [SerializeField] int HP = 100;
    public GameObject bossBullet;
    private float lastFire = 5f;


    // Update is called once per frame
    void Update()
    {
        if (fireRate + lastFire < Time.time)
        {
            fire();

            lastFire = Time.time;
        }
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void fire()
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject bBullet = Instantiate(bossBullet);
            bBullet.transform.position = transform.GetChild(0).position;
            bBullet.transform.Rotate(new Vector3(0f, 0f + 10 * i, 0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;
        Debug.Log(HP);
        Destroy(other.gameObject);
    }
}
