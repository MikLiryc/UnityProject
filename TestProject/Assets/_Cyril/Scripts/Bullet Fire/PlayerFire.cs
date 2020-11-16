using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;            //총알 프리팹 찍어내는 곳
    public GameObject firePoint;                //총알 발사 위치

    //사운드 재생
    //오디오 소스 컴포넌트가 반드시 필요함
    //오디오 리스너는 게임 상에 단 한개만 존재해야 함
    //오디오 리스너는 기본적으로 카메라에 붙어있음
    private AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;    //오디오 파일이 여러 개일때
    [SerializeField] GameObject fireFx;

    private float lastFire = 0f;

    private void Start()
    {
        //오디오 소스 컴포넌트 가져옴
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //마우스 왼버튼 or 왼쪽 컨트롤 키
        if (Input.GetKey(KeyCode.Space) && lastFire + 0.2f < Time.time)
        {
            //발사하자마자 사운드 재생
            audioSource.PlayOneShot(audioClips[0]);

            //발사시 이펙트 생성
            showEffect();

            //총알 GameObject 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 위치 지정
            bullet.transform.position = firePoint.transform.position;

            lastFire = Time.time;
        }
    }

    private void showEffect()
    {
        GameObject fx = Instantiate(fireFx);
        fx.transform.position = transform.GetChild(0).transform.position;
    }
}
