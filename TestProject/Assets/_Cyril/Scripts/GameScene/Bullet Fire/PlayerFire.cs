using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;            //총알 프리팹 찍어내는 곳
    public GameObject firePoint;                //총알 발사 위치

    //오브젝트 풀링
    //오브젝트 풀링에 사용할 최대 총알 갯수
    private int poolSize = 20;
    private int fireIndex = 0;

    // 1. Array
    //private GameObject[] bulletPool;
    // 2. List
    //public List<GameObject> bulletPool;
    // 3. Queue
    public Queue<GameObject> bulletPool;

    //레이저를 발사하기 위한 라인 렌더러
    //레이저와 충돌은 Raycast 를 사용
    private LineRenderer lR;
    [SerializeField]
    LayerMask layerMask;

    //파티클
    private GameObject fireParticle;

    //사운드 재생
    //오디오 소스 컴포넌트가 반드시 필요함
    //오디오 리스너는 게임 상에 단 한개만 존재해야 함
    //오디오 리스너는 기본적으로 카메라에 붙어있음
    private AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;    //오디오 파일이 여러 개일때

    private float lastFire = 0f;

    private void Start()
    {
        //파티클 시스템
        fireParticle = GameObject.Find("PlayerFireEffect");

        //오디오 소스 컴포넌트 가져옴
        audioSource = GetComponent<AudioSource>();

        //오브젝트 풀링 초기화
        InitObjectPooling();

        lR = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Fire();
    }

    public void OnFireButtonClick()
    {
        StartCoroutine(FireLaserCoroutine());
    }

    IEnumerator FireLaserCoroutine()
    {
        //라인렌더러 활성화
        lR.enabled = true;
        //라인 시작점, 끝점 세팅
        lR.SetPosition(0, firePoint.transform.position);
        lR.SetPosition(1, firePoint.transform.position + Vector3.forward * 50);

        //Ray로 충돌처리
        Ray ray = new Ray(firePoint.transform.position, Vector3.forward);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag != "Enemy Bullet")
            {
                //레이저의 끝점 지점
                lR.SetPosition(1, hitInfo.point);
                //충돌된 오브젝트 모두 지우기
                if (hitInfo.collider.tag != "Dead Zone")
                {
                    hitInfo.collider.gameObject.SetActive(false);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        lR.enabled = false;
    }

    private void InitObjectPooling()
    {
        // 1. Array
        //bulletPool = new GameObject[poolSize];
        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //
        //    bulletPool[i] = bullet;
        //}

        // 2. List
        //bulletPool = new List<GameObject>();
        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool.Add(bullet);
        //}

        // 3. Queue
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private void Fire()
    {
        //마우스 왼버튼 or 왼쪽 컨트롤 키
        if (Input.GetKey(KeyCode.Space) && lastFire + 0.2f < Time.time)
        {
            //발사하자마자 사운드 재생
            audioSource.PlayOneShot(audioClips[Random.Range(0, 2)]);

            //발사시 이펙트 생성
            showEffect();

            // 1. 배열 오브젝트 풀링으로 총알 발사
            //bulletPool[fireIndex].SetActive(true);
            //bulletPool[fireIndex].transform.position = firePoint.transform.position;
            //bulletPool[fireIndex].transform.up = firePoint.transform.up;
            // 2. List
            //bulletPool[fireIndex].SetActive(true);
            //bulletPool[fireIndex].transform.position = firePoint.transform.position;
            //bulletPool[fireIndex].transform.up = firePoint.transform.up;
            // 2-1. List 오브젝트 풀링으로 총알 발사
            //if (bulletPool.Count > 0)
            //{
            //    GameObject bullet = bulletPool[0];
            //    bullet.SetActive(true);
            //    bullet.transform.position = firePoint.transform.position;
            //    bullet.transform.up = firePoint.transform.up;
            //    //오브젝트 풀에서 빼준다
            //    bulletPool.Remove(bullet);
            //}
            //else  //오브젝트 풀이 비어서 풀 크기를 늘려준다
            //{
            //    GameObject bullet = Instantiate(bulletFactory);
            //    bullet.SetActive(false);
            //    bulletPool.Add(bullet);
            //}
            // 3. Queue (큐가 리스트보다 성능이 조금 더 좋음)
            if (bulletPool.Count > 0)
            {
                FireBullet();
            }
            else
            {
                CreateBullet();
                FireBullet();
            }
            
            fireIndex++;
            if (fireIndex >= poolSize) { fireIndex = 0; }

            ////총알 GameObject 생성
            //GameObject bullet = Instantiate(bulletFactory);
            ////총알 위치 지정
            //bullet.transform.position = firePoint.transform.position;

            lastFire = Time.time;
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private void FireBullet()
    {
        GameObject bullet = bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.up = firePoint.transform.up;
    }

    private void showEffect()
    {
        fireParticle.GetComponent<ParticleSystem>().Play();
        //GameObject fx = Instantiate(fireFx);
        //fx.transform.position = transform.GetChild(0).transform.position;
    }
}
