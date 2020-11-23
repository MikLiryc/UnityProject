using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifeCount { get; set; }
    public int bombCount { get; set; }
    
    public bool isDead = false;
    public bool isOnPos = true;

    [SerializeField]
    private GameObject dieEffect;

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
        else
        {
            //transform.position = new Vector3(100f, 100f, 100f);
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            GameObject dieFX = Instantiate(dieEffect);
            dieFX.transform.position = gameObject.transform.position;
            lifeCount--;
            isDead = true;
            isOnPos = false;
            transform.position = GameObject.Find("Respawn Point").transform.position;
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
