using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//()안의 컴포넌트를 반드시 가지고 있어야만함
//없다면 자동으로 원하는 컴포넌트를 추가한다
//반드시 필요한 컴포넌트를 실수로 삭제할 수도 있기 때문에 강제로 붙어있게 만들어줌
[RequireComponent(typeof(Rigidbody))]

public class Enemy : MonoBehaviour
{
    //에너미의 역할
    //위에서 아래로 떨어진다
    //에너미가 플레이어를 향해서 총알 발사

    [SerializeField] float speed = 5.0f;
    [SerializeField] GameObject bloodFx;

    //충돌처리 - Rigidbody 사용

    private float cameraHeight;

    private void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (transform.position.z < -cameraHeight)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Ground")
        {
            GameObject fx = Instantiate(bloodFx);
            fx.transform.position = transform.position;

            if (collision.gameObject.name =="Player")
            {
                SceneMgr.Instance.LoadScene("StartScene");
            }

            ScoreManager.Instance.addScore();

            //자기자신도 없애고 충돌된 오브젝트도 없앰
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Ground" 
            && other.transform.tag != "Enemy Bullet"
            && other.transform.tag != "Dead Zone")
        {
            GameObject fx = Instantiate(bloodFx);
            fx.transform.position = transform.position;

            ScoreManager.Instance.addScore();

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
