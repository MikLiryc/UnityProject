using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject sidePlaneLeft;
    public GameObject sidePlaneRight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
    }
}
