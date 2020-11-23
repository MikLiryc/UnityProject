using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rectBg;
    [SerializeField] private RectTransform rectJoystick;

    private float radius;

    private float cameraWidth;
    private float cameraHeight;
    private float playerHalfWidth;
    private float playerHalfHeight;

    [SerializeField] private GameObject Player;

    private bool isTouch = false;
    private Vector3 movePosition;

    void Start()
    {
        radius = rectBg.rect.width * 0.5f;

        //카메라 높이의 절반
        cameraHeight = Camera.main.orthographicSize;
        //카메라 넓이의 절반
        cameraWidth = cameraHeight * Screen.width / Screen.height;

        //플레이어 collider 의 길이 (bounds)의 절반 (extents)
        Vector3 colSize = Player.gameObject.GetComponent<Collider>().bounds.extents;
        playerHalfHeight = colSize.z;
        playerHalfWidth = colSize.x;
    }

    void Update()
    {
        if (GameObject.Find("Player").gameObject.GetComponent<Player>().lifeCount > 0)
        {
            if (isTouch)
            {
                Vector3 nextPos = Player.transform.position + movePosition;
                nextPos.x = Mathf.Clamp(nextPos.x, -cameraWidth + playerHalfWidth, cameraWidth - playerHalfWidth);
                nextPos.z = Mathf.Clamp(nextPos.z, -cameraHeight + playerHalfHeight, cameraHeight - playerHalfHeight);
                Player.transform.position = nextPos;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rectBg.position;
        value = Vector2.ClampMagnitude(value, radius);
        rectJoystick.localPosition = value;

        float distance = Vector2.Distance(Vector3.zero, rectJoystick.localPosition) / radius;
        
        value = value.normalized;
        movePosition = 
            new Vector3(value.x * Player.GetComponent<PlayerMove>().playerSpeed * distance * Time.deltaTime, 0f, 
            value.y * Player.GetComponent<PlayerMove>().playerSpeed * distance * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rectJoystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }
}
