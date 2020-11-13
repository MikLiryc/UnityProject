using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material mat;
    public float scrollSpeed = 0.1f;

    void Start()
    {
        //머테리얼은 렌더러 컴포넌트 안에 속성으로 붙어있음
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundScroll();
    }

    private void BackgroundScroll()
    {
        //머테리얼의 메인 텍스쳐 오프셋은 Vector2 로 만들어져있음
        Vector2 offset = mat.mainTextureOffset;

        //offset의 y값만 보정해줌
        offset.Set(offset.x - scrollSpeed * Time.deltaTime, 0);

        //다시 머테리얼 오프셋에 담음
        mat.mainTextureOffset = offset;
    }
}
