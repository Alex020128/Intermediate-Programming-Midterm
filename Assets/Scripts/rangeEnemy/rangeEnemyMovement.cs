using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rangeEnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float shootingRange;
    public float fireRate = 3.0f;
    private float nextFireTime;
    //public Animator animator;

    public GameObject bulletParent;
    private Transform player;
    //public Rigidbody2D rb;

    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private GameObject[] bullets = new GameObject[5];

    public float health;

    private bool bomb;

    [SerializeField]
    private GameObject prefabAmountBuff = null;
    [SerializeField]
    private GameObject prefabDamageBuff = null;
    [SerializeField]
    private GameObject prefabHealthBuff = null;

    //public AudioSource audioSource;

    //public AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        //audioSource = GetComponent<AudioSource>();

        //Spawn a pool of bullets at top of the screen
        for (int i = 0; i < bullets.Length; i++)
        {
            GameObject newBullet = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, bulletParent.transform);
            bullets[i] = newBullet;
            bullets[i].SetActive(false);
        }

        health = timeManager.Instance.rangeEnemyHealth;

        bomb = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void hurtSFX()
    {
        //audioSource.Stop();
        //audioSource.clip = hurtSound;
        //audioSource.Play();
    }


    public void shootBullet()
    {
        //let one of the waiting bullets to be active
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
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.collider.gameObject.tag == "Player" && gameManager.Instance.invinsible == false)
        {
            gameManager.Instance.playerHealth -= 1;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.5f, new Vector3(0.5f, 0.5f, 0));
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

        bomb = false;
        //string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

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

        if (health <= 0)
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
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
