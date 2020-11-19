using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;

    private GameObject target;
    //private Vector3 dir;
    
    private void Start()
    {
        target = GameObject.Find("Player");
        //dir = target.transform.position - transform.position;
        //dir.Normalize();
        transform.LookAt(target.transform);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //if (transform.position.z < -1 * gameManager.cameraHeight)
        //{
        //    Destroy(gameObject); 
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Dead Zone")
        {
            gameObject.SetActive(false);
            GameObject.Find("BulletManager").GetComponent<BulletManager>().bulletPool.Enqueue(gameObject);
        }
        else if (other.gameObject.name == "Player" && !other.gameObject.GetComponent<Player>().isDead)
        {
            gameObject.SetActive(false);
            GameObject.Find("BulletManager").GetComponent<BulletManager>().bulletPool.Enqueue(gameObject);

            other.GetComponent<Player>().lifeCount -= 1;
            other.GetComponent<Player>().isDead = true;
            other.GetComponent<Player>().isOnPos = false;
            other.gameObject.transform.position = GameObject.Find("Respawn Point").transform.position;
            //other.gameObject.GetComponent<Player>().lifeCount -= 1;
            //
            //SceneMgr.Instance.LoadScene("EndScene");
        }
    }
}
