using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;

    private GameObject target;
    private GameManager gameManager;
    private Vector3 dir;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        target = GameObject.FindWithTag("Player");
        dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.LookAt(target.transform);
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        
        if (transform.position.z < -1 * gameManager.cameraHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Ground" && other.transform.tag != "Enemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
