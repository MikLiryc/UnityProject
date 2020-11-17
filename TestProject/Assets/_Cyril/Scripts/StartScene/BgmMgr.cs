using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmMgr : MonoBehaviour
{
    public static BgmMgr Instance;

    private Dictionary<string, AudioClip> bgmTable; //BGM 콜렉션
    private AudioSource audioMain;                  //메인 오디오
    private AudioSource audioSub;                   //서브 오디오 (BGM 교체시 사용)

    [Range(0f, 1.0f)]               //[]로 만들어져 있는 Range 어트리뷰트 (속성 or  부수물)
    public float masterVolume = 1.0f;

    private float volumeMain = 0.0f;        //메인 오디오 볼륨
    private float volumeSub = 0.0f;         //서브 오디오 볼륨
    private float crossFadeTime = 3.0f;     //크로스페이드 타임 3초

    private void Awake()
    {
        if (Instance) { DestroyImmediate(gameObject); return; }
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bgmTable = new Dictionary<string, AudioClip>();

        //오디오 소스 코드로 추가
        audioMain = gameObject.AddComponent<AudioSource>();
        audioSub = gameObject.AddComponent<AudioSource>();

        //오디오 볼륨 0으로 초기화
        audioMain.volume = 0.0f;
        audioSub.volume = 0.0f;
    }

    private void Update()
    {
        //BGM이 플레이 중일때 메인볼륨은 올리고 서브볼륨은 내린다
        if (audioMain.isPlaying)
        {
            //메인오디오 볼륨 올리기
            if (volumeMain < 1.0f)
            {
                volumeMain += Time.deltaTime / crossFadeTime;
                if (volumeMain >= 1.0f) volumeMain = 1.0f;
            }

            //서브오디오 볼륨 내리기
            if (volumeSub > .0f)
            {
                volumeSub -= Time.deltaTime / crossFadeTime;
                if (volumeSub <= .0f) { volumeSub = 0.0f; audioSub.Stop(); }
            }

            //볼륨조정
            audioMain.volume = volumeMain * masterVolume;
            audioSub.volume = volumeSub * masterVolume;
        }
    }

    //BGM 플레이
    public void PlayBGM(string bgmName)
    {
        //Dictionary 안에 BGM이 없으면 리소스 폴더에서 찾아서 새로 추가하기
        if (!bgmTable.ContainsKey(bgmName))
        {
            //유니티 엔진에서 특별한 기능의 Resources 폴더가 존재함
            //어디에서든 파일을 로드할 수 있다
            //스펠링 Resources 똑같아야함
            //리소스 폴더가 여러개 존재하면 모든 폴더를 검색한다
            //Resources/BGM/ 폴더안의 오디오 클립을 찾는다
            AudioClip bgm = (AudioClip)Resources.Load("BGM/" + bgmName);

            //리소스폴더에 BGM이 없다면 Dictionary에 추가하지 말고 그냥 리턴하고 나온다
            if (bgm == null) return;

            //있으면 Dictionary에 bgmName 키값으로 추가
            bgmTable.Add(bgmName, bgm);
        }

        //메인 오디오의 클립에 새로운 오디오 클립을 연결한다
        audioMain.clip = bgmTable[bgmName];
        //메인오디오로 플레이
        audioMain.Play();

        //볼륨값 세팅
        volumeMain = 1.0f;
        volumeSub = 0.0f;
    }

    //BGM 크로스페이드 플레이
    public void CrossFadeBGM(string bgmName, float cFTime = 3.0f)
    {
        if (!bgmTable.ContainsKey(bgmName))
        {
            AudioClip bgm = (AudioClip)Resources.Load("BGM/" + bgmName);

            if (bgm == null) return;

            bgmTable.Add(bgmName, bgm);
        }

        //크로스페이드 타임세팅
        crossFadeTime = cFTime;

        //메인 오디오에서 플레이 되고 있는걸 서브오디오로 연결
        AudioSource temp = audioMain;
        audioMain = audioSub;
        audioSub = temp;

        //볼륨 교체
        float tempVol = volumeMain;
        volumeMain = volumeSub;
        volumeSub = volumeMain;

        audioMain.clip = bgmTable[bgmName];
        audioMain.Play();
    }

    //일시정지
    public void PauseBGM()
    {
        audioMain.Pause();
    }

    //다시재생
    public void ResumeBGM()
    {
        audioMain.Play();
    }

}
