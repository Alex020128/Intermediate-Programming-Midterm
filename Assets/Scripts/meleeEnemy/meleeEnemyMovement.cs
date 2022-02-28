using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class meleeEnemyMovement : MonoBehaviour
{
    private Transform player;

    public float moveSpeed = 2;

    public float health;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        health = 3;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invisible == false)
        {
            gameManager.Instance.playerHealth -= 2;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.5f,new Vector3(0.5f, 0.5f, 0));
            gameManager.Instance.invisibleTime = 0;
            gameManager.Instance.invisible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
