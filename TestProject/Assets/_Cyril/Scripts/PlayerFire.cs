using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;            //총알 프리팹 찍어내는 곳
    public GameObject firePoint;                //총알 발사 위치

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //마우스 왼버튼 or 왼쪽 컨트롤 키
        if (Input.GetButtonDown("Fire1"))
        {
            //총알 GameObject 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 위치 지정
            bullet.transform.position = firePoint.transform.position;
        }
    }
}
