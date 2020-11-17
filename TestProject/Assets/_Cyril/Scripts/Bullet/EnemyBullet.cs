using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;

    private GameObject target;
    private GameManager gameManager;
    //private Vector3 dir;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        target = GameObject.Find("Player");
        //dir = target.transform.position - transform.position;
        //dir.Normalize();
        transform.LookAt(target.transform);
    }

    void Update()
    {
        if (target != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.z < -1 * gameManager.cameraHeight)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Dead Zone")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            if (other.gameObject.name == "Player")
            {
                SceneMgr.Instance.LoadScene("StartScene");
            }
        }
    }
}
