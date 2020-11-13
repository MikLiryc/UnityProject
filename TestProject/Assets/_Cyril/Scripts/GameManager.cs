using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float enemySpawnRate = 1.5f;
    public GameObject boss;
    private float lastSpawnTime = 0.0f;

    public GameObject enemySpawner;

    private GameManager gameManager;
    private bool isShowBoss = false;
    public int spawnCount = 0;

    public float cameraWidth { get; set; }
    public float cameraHeight { get; set; }
    

    private void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }

    void Update()
    {
        if (GameObject.Find("Player") != null)
        {
            if (lastSpawnTime + enemySpawnRate < Time.time)
            {
                GameObject enemy = Instantiate(enemySpawner);
                Vector3 pos = enemy.transform.position;
                pos.x = Random.Range(-cameraWidth, cameraWidth);
                pos.z = cameraHeight + 1.0f;
                enemy.transform.position = pos;
                enemy.transform.Rotate(new Vector3(0f, 180f, 0f));

                lastSpawnTime = Time.time;
                spawnCount++;
            }

            if (!boss.activeSelf && !isShowBoss && spawnCount > 20)
            {
                boss.SetActive(true);
                isShowBoss = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
