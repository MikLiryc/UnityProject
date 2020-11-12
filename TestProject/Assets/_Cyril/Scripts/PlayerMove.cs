using System;
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
    public Vector2 margin;

    // Start is called before the first frame update
    void Start()
    {
        //카메라 높이의 절반
        cameraHeight = Camera.main.orthographicSize;
        //카메라 넓이의 절반
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        //플레이어 collider 의 길이 (bounds)의 절반 (extents)
        Vector3 colSize = GetComponent<Collider>().bounds.extents;
        playerHalfHeight = colSize.z;
        playerHalfWidth = colSize.x;

        playerRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //normalMove();

        //LimitByPosition();

        LimitByScreenSize();

        //LimitByViewPort();
    }

    private void LimitByViewPort()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        transform.Translate(dir * Time.deltaTime * speed);

        Vector3 position = position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x, 1.0f - margin.x);
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y, 1.0f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void normalMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        //P = P0 + vt;
        //Position = CurrentPosition + vector(Direction + speed) * Time;

        transform.position += dir.normalized * speed * Time.deltaTime;

        playerRigidBody.velocity = new Vector3(speed * h, 0f, speed * v);
    }

    private void LimitByPosition()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //동일함
        //Vector3 dir = new Vector3(h, 0f, v);
        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        Vector3 position = transform.position + dir.normalized * speed * Time.deltaTime;

        //if (position.x > 2.5f) position.x = 2.5f;
        //if (position.x < -2.5f) position.x = -2.5f;
        //if (position.z > 2.5f) position.z = 2.5f;
        //if (position.z < -2.5f) position.z = -2.5f;
        //clamp 함수가 성능이 더 뛰어나다 => 최적화를 위해선 if문 반복보다는 clamp 사용이 권장됨
        position.x = Mathf.Clamp(position.x, -2.5f, 2.5f);
        position.z = Mathf.Clamp(position.z, -2.5f, 2.5f);

        transform.position = position;
    }

    private void LimitByScreenSize()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0f, v);

        Vector3 movePosition = transform.position + dir.normalized * speed * Time.deltaTime;
        movePosition.x = Mathf.Clamp(movePosition.x, -cameraWidth + playerHalfWidth, cameraWidth - playerHalfWidth);
        movePosition.z = Mathf.Clamp(movePosition.z, -cameraHeight + playerHalfHeight, cameraHeight - playerHalfHeight);

        transform.position = movePosition;
    }
}
