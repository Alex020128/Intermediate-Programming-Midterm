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

    private bool dead;

    private PolygonCollider2D pc;
    private SpriteRenderer sr;

    private ParticleSystem particle;

    Coroutine deathCoroutine;

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
        dead = false;

        particle = GetComponent<ParticleSystem>();
        pc = GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        pc.enabled = true;
        sr.enabled = true;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invinsible == false)
        {
            gameManager.Instance.playerHealth -= 2;
            GameObject.Find("Player").GetComponent<playerMovement>().Particle.Emit(5);
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
            particle.Emit(5);
            Camera.main.transform.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0));
            collision.gameObject.SetActive(false);
        }

        //Decrease health, emit particle, trigger sreenshake when gets hit by missiles
        if (collision.gameObject.tag == "Missile" && bomb == false)
        {
            this.health -= gameManager.Instance.missileDamage;
            particle.Emit(5);
            bomb = true;
        }

    }

    private IEnumerator deathExplode(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        bomb = false;

        if(health <= 0 && dead == false)
        {
            int chance = Random.Range(0, 100);
            if (chance <= 15)
            {
                Instantiate(prefabAmountBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
            else if (chance > 15 && chance <= 30)
            {
                Instantiate(prefabDamageBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
            else if (chance > 30 && chance <= 50)
            {
                Instantiate(prefabHealthBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            } else
            {
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
        }
    }
}
