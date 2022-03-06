using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : Singleton<gameManager>
{
    [SerializeField]
    private GameObject prefabToSpawn = null;

    public int playerHealth;

    public TMP_Text health;

    public bool death = false;

    public bool invinsible = false;

    public float invinsibleTime;

    public float missileCoolDownTime;

    public float bulletDamage;
    public float missileDamage;

    public float bulletDamageEXP;
    public float bulletAmountEXP;

    public float bulletDamageEXPBar;
    public float bulletAmountEXPBar;

    public AudioSource audioSource;

    public AudioClip musicLoop;

    void Awake()
    {
        playerHealth = 100;
        name = "GameManager"; // Set name of object
        health = GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

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
        audioSource.clip = musicLoop;
        audioSource.Play();
    }


    void Update()
    {
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


        if (playerHealth <= 0)
        {
            //Display total time lasted and instruction for restart
            death = true;
        }

        if(playerHealth > 100)
        {
            playerHealth = 100;
        }

        missileDamage = Mathf.Floor(0.5f * (bulletDamage * GameObject.Find("lmbBullet").GetComponent<bulletSpawner>().Bullets.Count));

        if (bulletDamageEXP == bulletDamageEXPBar)
        {
            bulletDamage += 1;
            bulletDamageEXP = 0;
            bulletDamageEXPBar += 1;
        }

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
