using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //속력
    public float speed = 5.0f;
    private Rigidbody playerRigidBody;
    private float cameraWidth;
    private float cameraHeight;
    private float playerHalfWidth;
    private float playerHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        cameraHeight = Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        Vector3 colSize = GetComponent<Collider>().bounds.extents;
        playerHalfHeight = colSize.z;
        playerHalfWidth = colSize.x;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //transform.Translate(new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0), Space.World);

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        Vector3 movePosition = transform.position + dir.normalized * speed * Time.deltaTime;
        movePosition.x = Mathf.Clamp(movePosition.x, -cameraWidth + playerHalfWidth, cameraWidth - playerHalfWidth);
        movePosition.z = Mathf.Clamp(movePosition.z, -cameraHeight + playerHalfHeight, cameraHeight - playerHalfHeight);

        transform.position = movePosition;

        //P = P0 + vt;
        //Position = CurrentPosition + vector(Direction + speed) * Time;

        //transform.position += dir.normalized * speed * Time.deltaTime;

        //playerRigidBody.velocity = new Vector3(speed * h, 0f, speed * v);

    }
}
