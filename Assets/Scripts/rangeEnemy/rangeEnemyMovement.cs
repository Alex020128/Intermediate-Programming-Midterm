using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rangeEnemyMovement : MonoBehaviour
{
    //Enemy stats
    public float moveSpeed;
    public float shootingRange;
    public float fireRate = 3.0f;
    private float nextFireTime;
    public float health;

    public GameObject bulletParent;
    private Transform player;
    private PolygonCollider2D pc;
    private SpriteRenderer sr;
    private ParticleSystem particle;
    private Animator animator;

    //Enemy bullets
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private GameObject[] bullets = new GameObject[5];

    //Enemy bools
    private bool bomb;
    private bool dead;

    //Enemy SFXs
    public AudioSource audioSource;
    public AudioClip hurtSound;

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

        //Spawn a pool of enemy bullets
        for (int i = 0; i < bullets.Length; i++)
        {
            GameObject newBullet = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, bulletParent.transform);
            bullets[i] = newBullet;
            bullets[i].SetActive(false);
        }

        //The health of the enemy increases over time
        health = timeManager.Instance.rangeEnemyHealth;

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

    //Show gizmos of the shoot bullet range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void hurtSFX()
    {
        //Play the enemy hurt SFX
        audioSource.Stop();
        audioSource.clip = hurtSound;
        audioSource.Play();
    }

    public void shootBullet()
    {
        //let one of the waiting enemy bullets to be active
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                bullets[i].GetComponent<enemyBullet>().lifeTimer = 3.0f;
                bullets[i].transform.position = transform.position;
                bullets[i].transform.parent = bulletParent.transform;
                break;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Decrease player health, emits particle, set player invincible time, trigger sreenshake when hits the player
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invinsible == false && gameManager.Instance.death == false)
        {
            gameManager.Instance.playerHealth -= 1;
            GameObject.Find("Player").GetComponent<playerMovement>().Particle.Emit(5);
            GameObject.Find("Player").GetComponent<playerMovement>().hurtSFX();
            Camera.main.transform.DOShakePosition(0.5f, new Vector3(0.5f, 0.5f, 0));
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
    private IEnumerator deathExplode(float wait)
    {
        //Make sure that the death particle will be shown
        yield return new WaitForSeconds(wait);
        scoreManager.Instance.rangeEnemyKills += 1;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        bomb = false;

        if (dead == false)
        {
            //Shoot the player when player is inside the shoot range, otherwise chases the player
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
            {
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }

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
            }
            else
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
