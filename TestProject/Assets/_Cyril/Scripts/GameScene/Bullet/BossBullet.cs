using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead Zone")
        {
            ReturnBullet();
        }
        else if (other.tag == "Player" )
        {
            ReturnBullet();
            if (GameObject.Find("Boss").GetComponent<Boss>().HP > 0)
            {
                other.GetComponent<Player>().Die();
            }
        }
    }

    private void ReturnBullet()
    {
        gameObject.SetActive(false);
        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        if (gameObject.name.Contains("Targeting Bullet"))
        {
            GameObject.Find("Boss").GetComponent<Boss>().normalBulletPool.Enqueue(gameObject);
        }
        else if (gameObject.name.Contains("Rotate Bullet"))
        {
            GameObject.Find("Boss").GetComponent<Boss>().rotateBulletPool.Enqueue(gameObject);
        }
    }
}
