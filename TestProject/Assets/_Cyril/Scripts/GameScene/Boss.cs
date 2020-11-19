using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float normalFireRate = 1.0f;
    [SerializeField] float rotateFireRate = 2.0f;

    public int HP { get; set; }

    public Queue<GameObject> normalBulletPool;
    private int normalBulletPoolSize = 5;

    public Queue<GameObject> rotateBulletPool;
    private int rotateBulletPoolSize = 40;
    
    [SerializeField] GameObject target;
    [SerializeField] int bulletMax = 36;
    public GameObject bossBullet;
    public GameObject bossRotateBullet;
    private float normalLastFire = 3f;
    private float rotateLastFire = 5f;

    public bool isDead { get; set; }

    private void Start()
    {
        isDead = false;
        HP = 100;
        normalBulletPool = new Queue<GameObject>();
        rotateBulletPool = new Queue<GameObject>();
        for (int i = 0; i < normalBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bossBullet);
            bullet.SetActive(false);
            normalBulletPool.Enqueue(bullet);
        }
        for (int i = 0; i < rotateBulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bossRotateBullet);
            bullet.SetActive(false);
            rotateBulletPool.Enqueue(bullet);
        }
    }

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
            StartCoroutine(DieCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        isDead = true;

        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    private void normalFire()
    {
        if (target != null)
        {
            GameObject bullet = normalBulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = transform.GetChild(0).position;
            bullet.transform.LookAt(target.transform);
            
            normalLastFire = Time.time;
        }
    }

    private void rotateFire()
    {
        for (int i = 0; i < bulletMax; i++)
        {
            GameObject bBullet = rotateBulletPool.Dequeue();
            bBullet.SetActive(true);
            bBullet.transform.position = transform.GetChild(0).position;
            bBullet.transform.Rotate(new Vector3(0f, (360.0f / bulletMax) * i, 0f));
        }
      
        rotateLastFire = Time.time;
    }
}
