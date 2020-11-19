using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //2D 에서 지렁이 만들듯이 꼬리가 머리를 따라다니게 만들기
    //꼬리가 타겟(플레이어)의 위치를 알고 있어야함

    public GameObject target;             //따라다닐 타겟 (플레이어)
    [SerializeField] float speed = 3.0f;            //속도
    
    // Update is called once per frame
    void Update()
    {
        //타겟 방향 구하기 (벡터의 뺄셈)
        //방향 = 타겟 - 자기자신

        Vector3 direction = target.transform.position - transform.position;
        direction.Normalize();                      //방향만 알기 위해서 normalize 단위벡터화 시킴
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
