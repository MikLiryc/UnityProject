using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private PlayerFire pf;
    private void Start()
    {
        //플레이어 게임 오브젝트의 PlayerFire 컴포넌트에 bulletPool속성을 지닌 오브젝트 풀을 찾는다
        pf = GameObject.Find("Player").GetComponent<PlayerFire>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //레이어로 충돌체 찾기
        //if (other.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        //{
        //    other.gameObject.SetActive(false);
        //    pf.bulletPool.Add(other.gameObject);
        //}
        //if (other.tag == "Bullet")
        //{
        //    other.gameObject.SetActive(false);
        //}
        //if (other.gameObject.name.Contains("PlayerBullet"))
        //{
        //    other.gameObject.SetActive(false);
        //    pf.bulletPool.Enqueue(other.gameObject);
        //}
        //else
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
