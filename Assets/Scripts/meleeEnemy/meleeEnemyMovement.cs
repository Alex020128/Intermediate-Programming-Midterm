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

        health = timeManager.Instance.meleeEnemyHealth; ;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invinsible == false)
        {
            gameManager.Instance.playerHealth -= 2;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.5f,new Vector3(0.5f, 0.5f, 0));
            gameManager.Instance.invinsibleTime = 0;
            gameManager.Instance.invinsible = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.gameObject.tag == "Bullet")
        {
            this.health -= timeManager.Instance.bulletDamage;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0));
            collision.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
