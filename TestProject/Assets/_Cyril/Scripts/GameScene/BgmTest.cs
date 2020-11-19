using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            BgmMgr.Instance.PlayBGM("bgm1");
        }
        if (Input.GetKeyDown("2"))
        {
            BgmMgr.Instance.PlayBGM("bgm2");
        }
        if (Input.GetKeyDown("3"))
        {
            BgmMgr.Instance.CrossFadeBGM("bgm1");
        }
        if (Input.GetKeyDown("4"))
        {
            BgmMgr.Instance.CrossFadeBGM("bgm2");
        }
    }
}
