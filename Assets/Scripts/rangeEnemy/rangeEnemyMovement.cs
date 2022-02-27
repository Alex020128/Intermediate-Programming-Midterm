using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Update is called once per frame
    void Update()
    {

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

    }
}
