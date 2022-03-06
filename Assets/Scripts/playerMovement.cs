using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerMovement : MonoBehaviour
{
    Vector2 movement;

    public float moveSpeed;

    public Rigidbody2D rb;

    public bool shootBullet;
    public bool shootMissile;

    public Animator animator;

    public AudioSource audioSource;

    public AudioClip hurtSound;
    public AudioClip buffSound;
    public AudioClip deathSound;

    private ParticleSystem particle;
    public ParticleSystem Particle
    {
        get
        {
            return particle;
        }
    }

private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = 5.0f;

        animator = GetComponent<Animator>();

        particle = GetComponent<ParticleSystem>();

        audioSource = GetComponent<AudioSource>();
    }

    public float health;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);
        rb.simulated = true;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        //Different effcts from the buffs
        //Increase the maximum bullet amount with amountBuff
        if (collision.gameObject.tag == "amountBuff")
        {
            //explode.Emit(7);
            gameManager.Instance.bulletAmountEXP += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }

        //Increase the maximum bullet amount with damageBuff
        if (collision.gameObject.tag == "damageBuff")
        {
            //explode.Emit(7);

            gameManager.Instance.bulletDamageEXP += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }

        //Increase the maximum bullet amount with healthBuff
        if (collision.gameObject.tag == "healthBuff")
        {
            //explode.Emit(7);
            gameManager.Instance.playerHealth += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }
    }
    public void hurtSFX()
    {
        audioSource.Stop();
        audioSource.clip = hurtSound;
        audioSource.Play();
    }
    public void buffSFX()
    {
        audioSource.Stop();
        audioSource.clip = buffSound;
        audioSource.Play();
    }
    public void deathSFX()
    {
        audioSource.Stop();
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.death == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        } else
        {
            animator.SetBool("Death", true);
            rb.simulated = false;
        }
        
        if (transform.position.x <= -40f)
        {
            transform.position = new Vector2(-40f, transform.position.y);
        }else if (transform.position.x >= 40f)
        {
            transform.position = new Vector2(40f, transform.position.y);
        }

        if (transform.position.y <= -40f)
        {
            transform.position = new Vector2(transform.position.x, -40f);
        }else if (transform.position.y >= 40f)
        {
            transform.position = new Vector2(transform.position.x, 40f);
        }


        if (Input.GetMouseButton(0) && shootBullet == false && gameManager.Instance.death == false)
        {
            GameObject.Find("lmbBullet").GetComponent<bulletSpawner>().shootBullet();
            shootBullet = true;
        }

        if (Input.GetMouseButton(1) && shootMissile == false && gameManager.Instance.death == false)
        {
            GameObject.Find("rmbMissile").GetComponent<missileSpawner>().shootMissile();
            shootMissile = true;
        }
    }
}
