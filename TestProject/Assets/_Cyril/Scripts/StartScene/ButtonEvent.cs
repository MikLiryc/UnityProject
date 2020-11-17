using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    //시작버튼 클릭했을때 
    public void OnStartButtonClick()
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }

    //메뉴버튼 클릭
    public void OnMenuButtonClick()
    {

    }

    //옵션 버튼 클릭
    public void OnOptionButtonClick()
    {

    }
}
