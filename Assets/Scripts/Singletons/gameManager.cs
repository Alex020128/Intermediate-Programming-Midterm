using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : Singleton<gameManager>
{
    public int playerHealth;


    public TMP_Text health;

    public bool death = false;

    public bool invinsible = false;

    public float invinsibleTime;

    void Awake()
    {
        playerHealth = 100;
        name = "GameManager"; // Set name of object
        health = GetComponent<TMP_Text>();

        invinsibleTime = 0;
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
        }


        if (playerHealth <= 0)
        {
            //Display total time lasted and instruction for restart
            death = true;
        }
    }
}
