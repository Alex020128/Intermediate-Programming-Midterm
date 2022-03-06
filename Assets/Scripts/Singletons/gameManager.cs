using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : Singleton<gameManager>
{
    //Player bullet
    [SerializeField]
    private GameObject prefabToSpawn = null;

    //Player stats
    public int playerHealth;
    public TMP_Text health; //UI text

    //Player bools
    public bool death = false;
    public bool invinsible = false;

    //Player invincible timer
    public float invinsibleTime;
    
    //Player stats that can grow as more buffs are collected
    public float missileCoolDownTime;
    
    public float bulletDamage;
    public float missileDamage;

    public float bulletDamageEXP;
    public float bulletAmountEXP;

    public float bulletDamageEXPBar;
    public float bulletAmountEXPBar;

    //Background music loop
    public AudioSource audioSource;
    public AudioClip musicLoop;

    void Awake()
    {
        name = "GameManager"; // Set name of object
        
        health = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        playerHealth = 100;
        invinsibleTime = 0;
        missileCoolDownTime = 10;

        bulletDamage = 1;

        bulletDamageEXP = 0;
        bulletAmountEXP = 0;

        bulletDamageEXPBar = 1;
        bulletAmountEXPBar = 1;
    }

    private void Start()
    {
        //Loops the music
        audioSource.clip = musicLoop;
        audioSource.Play();
    }

    void Update()
    {
        //The player has 1s of invincible time if gets hurt
        if (invinsible == true)
        {
            invinsibleTime += Time.deltaTime;
            if (invinsibleTime >= 1)
            {
                invinsible = false;
            }
        }

        //Display health
        if (death == false)
        {
            health.text = "Health: " + playerHealth;
        } else
        {
            health.enabled = false;
        }

        //Player death
        if (playerHealth <= 0)
        {
            death = true;
        }

        //Player's health can't be over 100
        if(playerHealth > 100)
        {
            playerHealth = 100;
        }

        //Missile's damage (RMB) is dependent on both the bullet damage and the bullet amount
        missileDamage = Mathf.Floor(0.5f * (bulletDamage * GameObject.Find("lmbBullet").GetComponent<bulletSpawner>().Bullets.Count));

        //Increases bullet damage by 1 if the exp bar is filled, increases the exp required for next levelup by 1 as well
        if (bulletDamageEXP == bulletDamageEXPBar)
        {
            bulletDamage += 1;
            bulletDamageEXP = 0;
            bulletDamageEXPBar += 1;
        }
        
        //Increases bullet amount by 1 if the exp bar is filled, increases the exp required for next levelup by 1 as well
        if (bulletAmountEXP == bulletAmountEXPBar)
        {
            GameObject newBullet = Instantiate(prefabToSpawn, GameObject.Find("lmbBullet").transform.position, Quaternion.identity, GameObject.Find("lmbBullet").transform);
            GameObject.Find("lmbBullet").GetComponent<bulletSpawner>().Bullets.Add(newBullet);
            newBullet.SetActive(false);
            bulletAmountEXP = 0;
            bulletAmountEXPBar += 1;
            missileCoolDownTime -= 1;
        }
    }
}
