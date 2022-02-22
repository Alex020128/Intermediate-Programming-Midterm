using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemyMovement : MonoBehaviour
{
    private Transform player;

    public float moveSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
