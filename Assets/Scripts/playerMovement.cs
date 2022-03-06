using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerMovement : MonoBehaviour
{
    //Player stats
    Vector2 movement;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    //Player bools
    public bool shootBullet;
    public bool shootMissile;

    //Player SFXs
    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip buffSound;
    public AudioClip deathSound;

    //Player particle effect for being hurt
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
        moveSpeed = 5.0f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, 0);
        //Enable movement of the player
        rb.simulated = true;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        //Different effcts from the buffs
        //Increase the maximum bullet amount with amountBuff
        if (collision.gameObject.tag == "amountBuff")
        {
            gameManager.Instance.bulletAmountEXP += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }

        //Increase the bullet damage with damageBuff
        if (collision.gameObject.tag == "damageBuff")
        {
            gameManager.Instance.bulletDamageEXP += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }

        //Increase the health with healthBuff
        if (collision.gameObject.tag == "healthBuff")
        {
            gameManager.Instance.playerHealth += 1;
            buffSFX();
            Destroy(collision.gameObject);
        }
    }
    
    //SFXs
    public void hurtSFX()
    {
        //Play the player hurt SFX
        audioSource.Stop();
        audioSource.clip = hurtSound;
        audioSource.Play();
    }
    public void buffSFX()
    {
        //Play the SFX when buffed
        audioSource.Stop();
        audioSource.clip = buffSound;
        audioSource.Play();
    }
    public void deathSFX()
    {
        //Play the player death SFX
        audioSource.Stop();
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Basic player movement
        if(gameManager.Instance.death == false)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //Help determines the player idle animation
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

            //Help determines the player walk animation
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        } else
        {
            //Disable movement of the player
            animator.SetBool("Death", true);
            rb.simulated = false;
        }
        
        //Player can't go through the borders
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

        //Use LMB and RMB to shoot arcane missiles and cast a magic circle (bullet and missile)
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
