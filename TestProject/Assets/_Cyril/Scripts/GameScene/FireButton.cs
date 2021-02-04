using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject player;
    
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    private bool isTouch = false;

    void Update()
    {
        if (GameObject.Find("Player").gameObject.GetComponent<Player>().lifeCount > 0)
        {
            if (isTouch)
            {
                player.GetComponent<PlayerFire>().OnFireButtonClick();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
    }

}
