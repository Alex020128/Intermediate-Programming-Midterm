using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : Singleton<gameManager>
{
    public int playerHealth;

    public float playerTime;

    public TMP_Text health;

    public bool death = false;

    void Awake()
    {
        playerHealth = 10;
        playerTime = 0;
        name = "GameManager"; // Set name of object
        health = GetComponent<TMP_Text>();
    }

    /*void Update()
    {
        //Display health
        if (death == false)
        {
            playerTime += Time.deltaTime;
            health.text = "Health: " + playerHealth;
        }

        if(playerHealth <= 0)
        {
            //Display total time lasted and instruction for restart
            death = true;
            health.text = "You lasted: " + Mathf.Round(playerTime) + "s\n\nPress Space to Restart";

            //Reload scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Spawner");
            }
        }
    }*/
}
