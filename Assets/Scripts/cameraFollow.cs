using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float followSpeed = 4.5f;
    public float yPos;
    public Transform player;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPos = player.position;
        Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        smoothPos.x = Mathf.Clamp(smoothPos.x,-31f, 31f);
        smoothPos.y = Mathf.Clamp(smoothPos.y, -35, 35);
        transform.position = new Vector3(smoothPos.x, smoothPos.y + yPos, -15.0f);
    }
}
