using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float enemySpawnRate = 0.5f;
    public GameObject boss;
    private float lastSpawnTime = 0.0f;

    [SerializeField]
    private Text gameoverText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Text gameClearText;

    public GameObject enemySpawner;
    
    public Queue<GameObject> enemyPool;
    public int enemySize = 10;

    private GameManager gameManager;
    private bool isShowBoss = false;
    public int spawnCount = 0;

    [SerializeField]
    private int clearCount = 30;

    public float cameraWidth { get; set; }
    public float cameraHeight { get; set; }
    

    private void Awake()
    {
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < enemySize; i++)
        {
            CreateEnemy();
        }

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }

    private void OnEnable()
    {
        StartCoroutine(EnemyCoroutine());
    }

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().lifeCount <= 0 && GameObject.Find("Player") != null)
        {
            gameoverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        else if (boss.GetComponent<Boss>().HP <= 0 && boss.GetComponent<Boss>().gameObject.activeSelf)
        {
            gameClearText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }

    private void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemySpawner);
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }

    IEnumerator EnemyCoroutine()
    {
        while (spawnCount <= clearCount)
        {
            TrySpawn();
            Debug.Log("Enemy Coroutine works");
            yield return new WaitForSeconds(enemySpawnRate);
        }

        boss.SetActive(true);
        isShowBoss = true;
    }

    private void TrySpawn()
    {
        if (enemyPool.Count < 0)
        {
            SpawnEnemy();
        }
        else
        {
            CreateEnemy();
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.Dequeue();
        Vector3 pos = enemy.transform.position;
        pos.x = Random.Range(-cameraWidth, cameraWidth);
        pos.z = cameraHeight + 1.0f;
        enemy.transform.position = pos;
        enemy.transform.Rotate(new Vector3(0f, 180f, 0f));
        spawnCount++;
        enemy.SetActive(true);
    }
}
