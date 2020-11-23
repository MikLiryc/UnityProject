using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float normalFireRate = 1.0f;
    [SerializeField] float rotateFireRate = 2.0f;

    public int HP { get; set; }

    [SerializeField]
    private GameObject dieEffect;

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

    private float appearSpeed = 2.0f;

    private float bossWidth;
    private float bossHeight;

    private bool isOnDieCoroutine = false;

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

        Vector3 colsize = GetComponent<Collider>().bounds.extents;
        bossWidth = colsize.x;
        bossHeight = colsize.z;
    }

    private void OnEnable()
    {
        StartCoroutine(AppearCoroutine());
    }

    void Update()
    {
        if (HP > 0)
        {
            if (rotateFireRate + rotateLastFire < Time.time)
            {
                rotateFire();
            }
            if (normalFireRate + normalLastFire < Time.time)
            {
                normalFire();
            }
        }
        else 
        {
            if (!isDead)
            {
                StartCoroutine(DieCoroutine());
            }
        }
    }

    IEnumerator AppearCoroutine()
    {
        transform.position -= Vector3.forward * appearSpeed * Time.deltaTime;
        yield return null;

        if (transform.position.z > 6)
        {
            StartCoroutine(AppearCoroutine());
        }
    }

    IEnumerator DieCoroutine()
    {
        isDead = true;

        while (true)
        {
            GameObject fx = Instantiate(dieEffect);
            fx.transform.position = new Vector3(Random.Range(transform.position.x - bossWidth, transform.position.x + bossWidth), 1.0f, Random.Range(transform.position.z - bossHeight, transform.position.z + bossHeight));
            yield return new WaitForSeconds(0.2f);
        }
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
