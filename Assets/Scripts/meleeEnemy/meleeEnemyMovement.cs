using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class meleeEnemyMovement : MonoBehaviour
{
    //Enemy stats
    public float moveSpeed = 2;
    public float health;
    
    private Transform player;
    private PolygonCollider2D pc;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    private Animator animator;

    //Enemy SFXs
    public AudioSource audioSource;
    public AudioClip hurtSound;
    
    //Enemy bools
    private bool bomb;
    private bool dead;

    //Enemy coroutine
    Coroutine deathCoroutine;

    //The three buffs player can get
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
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        pc.enabled = true;
        sr.enabled = true;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Decrease player health, emits particle, set player invincible time, trigger sreenshake when hits the player
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invinsible == false && gameManager.Instance.death == false)
        {
           gameManager.Instance.playerHealth -= 2;
           GameObject.Find("Player").GetComponent<playerMovement>().Particle.Emit(5);
           GameObject.Find("Player").GetComponent<playerMovement>().hurtSFX();
           Camera.main.transform.DOShakePosition(0.5f,new Vector3(0.5f, 0.5f, 0));
           gameManager.Instance.invinsibleTime = 0;
           scoreManager.Instance.Hit += 1;
           gameManager.Instance.invinsible = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.gameObject.tag == "Bullet")
        {
            this.health -= gameManager.Instance.bulletDamage;
            animator.SetTrigger("Hurt");
            particle.Emit(5);
            Camera.main.transform.DOShakePosition(0.25f, new Vector3(0.25f, 0.25f, 0));
            collision.gameObject.SetActive(false);
        }

        //Decrease health, emit particle, trigger sreenshake when gets hit by missile
        if (collision.gameObject.tag == "Missile" && bomb == false)
        {
            this.health -= gameManager.Instance.missileDamage;
            animator.SetTrigger("Hurt");
            particle.Emit(5);
            bomb = true;
        }

    }
    public void hurtSFX()
    {
        //Play the enemy hurt SFX
        audioSource.Stop();
        audioSource.clip = hurtSound;
        audioSource.Play();
    }

    private IEnumerator deathExplode(float wait)
    {
        //Make sure that the death particle will be shown
        yield return new WaitForSeconds(wait);
        scoreManager.Instance.meleeEnemyKills += 1;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Chases the player
        if (dead == false)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        bomb = false;

        //Determines whether a buff is dropped when dead
        if (health <= 0 && dead == false)
        {
            int chance = Random.Range(0, 100);
            if (chance <= 15)
            {
                //Instantiate an amount buff
                Instantiate(prefabAmountBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
            else if (chance > 15 && chance <= 30)
            {
                //Instantiate a damage buff
                Instantiate(prefabDamageBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
            else if (chance > 30 && chance <= 50)
            {
                //Instantiate a health buff
                Instantiate(prefabHealthBuff, transform.position, Quaternion.identity);
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            } else
            {
                //Instantiate nothing
                pc.enabled = false;
                sr.enabled = false;
                deathCoroutine = StartCoroutine(deathExplode(0.5f));
                particle.Emit(10);
                dead = true;
            }
        }
    }
}
