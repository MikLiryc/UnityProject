using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifeCount { get; set; }
    public int bombCount { get; set; }
    
    public bool isDead = false;
    public bool isOnPos = true;

    private void Awake()
    {
        lifeCount = 5;
        bombCount = 3;
    }

    private void Update()
    {
        if (lifeCount > 0)
        {
            if (isDead)
            {
                if (!isOnPos)
                {
                    transform.position += Vector3.forward * 8.0f * Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Respawn Limit")
        {
            isOnPos = true;
            isDead = false;
        }
    }
}
