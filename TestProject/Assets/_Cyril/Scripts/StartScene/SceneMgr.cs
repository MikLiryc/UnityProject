using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    //씬매니저 싱글톤 만들기
    //씬매니저는 시작, 게임, 종료씬 등 모든 씬들을 관리해야 한다
    //또한 씬매니저는 씬이 변경되도 삭제되면 안된다
    public static SceneMgr Instance;

    private void Awake()
    {
        if (Instance) { DestroyImmediate(gameObject); return; }
        else Instance = this;

        //오브젝트를 삭제하지 않고 그대로 남겨둔다
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
