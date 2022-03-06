using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Missile : MonoBehaviour
{
    //Stats
    [SerializeField]
    public float lifeTimer;
    
    private PolygonCollider2D pc;
    private Animator animator;
    
    //Missile SFX
    public AudioSource audioSource;
    public AudioClip missileSound;

    private void Awake()
    {
        lifeTimer = 2.0f;
        
        pc = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //The missile doesn't deal damage at first
        pc.enabled = false;
    }
    public void missileSFX()
    {
        //Play the missile SFX
        audioSource.Stop();
        audioSource.clip = missileSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //The missile doesn't deal damage before 1s
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
        {
            //Disable the missile
            this.gameObject.SetActive(false);
        }else if(lifeTimer <= 1f)
        {
            //Enable the collider, thus being able to hurt the enemies
            pc.enabled = true;
            animator.SetTrigger("Explode");
            Camera.main.transform.DOShakePosition(0.5f, new Vector3(0.5f, 0.5f, 0));
        }
        else
        {
            //The missile doesn't deal damage at first
            pc.enabled = false;
        }
    }
}
