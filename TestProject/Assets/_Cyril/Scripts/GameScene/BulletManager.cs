using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletSpawner;
    public Queue<GameObject> bulletPool;
    [SerializeField]
    int poolSize = 50;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateBullet();
        }
    }

    public void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletSpawner, gameObject.transform);
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
