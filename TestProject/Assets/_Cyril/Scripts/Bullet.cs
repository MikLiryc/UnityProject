using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알 클래스 하는 일
    /* 플레이어가 발사 버튼을 누르면
     * 총알이 생성된 후 발사하고 싶은 방향(위)으로 발사됨
     * 총알은 지정된 방향으로 계속해서 날아감
     */
    private float cameraWidth;
    private float cameraHeight;
    private float bulletHalfWidth;
    private float bulletHalfHeight;

    [SerializeField] float bulletSpeed = 5.0f;
    [SerializeField] float sidePlaneBulletSpeed = 8.0f;

    private void Start()
    {
        //카메라 높이의 절반
        cameraHeight = Camera.main.orthographicSize;
        //카메라 넓이의 절반
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        //플레이어 collider 의 길이 (bounds)의 절반 (extents)
        Vector3 colSize = GetComponent<Collider>().bounds.extents;
        bulletHalfHeight = colSize.z;
        bulletHalfWidth = colSize.x;
    }

    void Update()
    {
        if (gameObject.name == "PlayerBullet(Clone)")
        {
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);    
        }
        else if (gameObject.name == "SidePlaneBullet(Clone)")
        {
            transform.Translate(Vector3.forward * sidePlaneBulletSpeed * Time.deltaTime);
        }

        //eraseBullet();
    }

    //카메라 화면밖으로 나가서 보이지 않게 되면 호출되는 이벤트 함수
    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //    //gameObject => 이 스크립트가 할당된 게임오브젝트
    //}

    //void eraseBullet()
    //{
    //    Vector3 position = transform.position;
    //    if (position.x > cameraWidth + bulletHalfWidth
    //        || position.x < -cameraWidth - bulletHalfWidth
    //        || position.z > cameraHeight + bulletHalfHeight
    //        || position.z < -cameraHeight - bulletHalfHeight)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
