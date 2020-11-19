using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject sidePlaneLeft;
    public GameObject sidePlaneRight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            switch (sidePlaneLeft.activeSelf && sidePlaneRight.activeSelf)
            {
                case true:
                    sidePlaneLeft.SetActive(false);
                    sidePlaneRight.SetActive(false);
                    break;
                case false:
                    sidePlaneLeft.SetActive(true);
                    sidePlaneRight.SetActive(true);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (gameObject.GetComponent<Player>().bombCount > 0)
            {
                gameObject.GetComponent<Player>().bombCount -= 1;

                TryUseBomb();
            }
        }
    }

    private void TryUseBomb()
    {
        throw new NotImplementedException();
    }
}
