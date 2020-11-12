using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float enemySpawnRate = 1.5f;
    public GameObject boss;
    private float lastSpawnTime = 0.0f;

    public GameObject enemySpawner;

    private GameManager gameManager;
    private bool isShowBoss = false;

    public float cameraWidth { get; set; }
    public float cameraHeight { get; set; }
    
    private void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }

    void Update()
    {
        //if (lastSpawnTime + enemySpawnRate < Time.time)
        //{
        //    GameObject enemy = Instantiate(enemySpawner);
        //    Vector3 pos = enemy.transform.position;
        //    pos.x = Random.Range(-cameraWidth, cameraWidth);
        //    pos.z = cameraHeight + 1.0f;
        //    enemy.transform.position = pos;
        //
        //    lastSpawnTime = Time.time;
        //}
        
        if (!boss.activeSelf && !isShowBoss)
        {
            if (Time.time > 5.0f)
            {
                boss.SetActive(true);
                isShowBoss = true;
            }
        }
    }
}
