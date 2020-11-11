using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float enemySpawnRate = 1.5f;
    private float lastSpawnTime = 0.0f;

    [SerializeField] GameObject enemySpawner;

    private GameManager gameManager;
    
    public float cameraWidth { get; set; }
    public float cameraHeight { get; set; }

    List<int> abs;
    List<string> astring;

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = new GameManager();
        }
        else
        {
            Destroy(gameObject);
        }
        
        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;
    }

    void Update()
    {
        if (lastSpawnTime + enemySpawnRate < Time.time)
        {
            GameObject enemy = Instantiate(enemySpawner);
            Vector3 pos = enemy.transform.position;
            pos.x = Random.Range(-cameraWidth, cameraWidth);
            pos.z = cameraHeight + 1.0f;
            enemy.transform.position = pos;

            lastSpawnTime = Time.time;
        }
    }
}
