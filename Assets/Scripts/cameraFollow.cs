using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraFollow : MonoBehaviour
{
    public float followSpeed = 4.5f;
    public float yPos;
    public Transform player;

    public GameObject deathBackground;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        deathBackground = GameObject.Find("deathBackground");
    }


    // Start is called before the first frame update
    void Start()
    {
        deathBackground.SetActive(false);
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

    private void Update()
    {
        if(gameManager.Instance.death == true)
        {
            deathBackground.SetActive(true);
        }
    }
}
