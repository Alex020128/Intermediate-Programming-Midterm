using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class meleeEnemyMovement : MonoBehaviour
{
    private Transform player;

    public float moveSpeed = 2;

    public float health;

    private bool bomb;

    [SerializeField]
    private GameObject prefabAmountBuff = null;
    [SerializeField]
    private GameObject prefabDamageBuff = null;
    [SerializeField]
    private GameObject prefabHealthBuff = null;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        health = timeManager.Instance.meleeEnemyHealth;

        bomb = false;
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
            this.health -= gameManager.Instance.bulletDamage;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0));
            collision.gameObject.SetActive(false);
        }

        //Decrease health, emit particle, trigger sreenshake when gets hit by missiles
        if (collision.gameObject.tag == "Missile" && bomb == false)
        {
            this.health -= gameManager.Instance.missileDamage;
            //explode.Emit(7);
            bomb = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);

        bomb = false;

        if(health <= 0)
        {
            int chance = Random.Range(0, 100);
            if (chance <= 10)
            {
                Instantiate(prefabAmountBuff, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (chance > 10 && chance <= 20)
            {
                Instantiate(prefabDamageBuff, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (chance > 20 && chance <= 45)
            {
                Instantiate(prefabHealthBuff, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
