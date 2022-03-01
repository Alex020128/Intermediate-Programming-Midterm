using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timeManager : Singleton<timeManager>
{
    public float playerTime;

    public TMP_Text time;

    public float meleeEnemyHealth;
    public float rangeEnemyHealth;

    public float bulletDamage;

    public float spawnSize;

    void Awake()
    {
        playerTime = 0;
        name = "TimeManager"; // Set name of object
        time = GetComponent<TMP_Text>();

        meleeEnemyHealth = 3;
        rangeEnemyHealth = 2;

        bulletDamage = 1;

        spawnSize = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.death == true)
        {
            //Display total time lasted and instruction for restart
            time.text = "You lasted: " + Mathf.Round(playerTime) + "s\n\nPress Space to Restart";

            //Reload scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Map");
            }
        }
        else
        {
            playerTime += Time.deltaTime;
            time.text = "Time: " + Mathf.Round(playerTime) + "s";
        }

        //Increase the enemy stats
        if (Mathf.Floor(playerTime) % 20 == 0 && Mathf.Floor(playerTime) != 0)
        {
            meleeEnemyHealth = 3 * Mathf.Ceil(playerTime / 20);
            rangeEnemyHealth = 2 * Mathf.Ceil(playerTime / 20);


            spawnSize = 30 + 10 * Mathf.Ceil(playerTime / 20);
        }

        //Increase the player stats
        if (Mathf.Floor(playerTime) % 30 == 0 && Mathf.Floor(playerTime) != 0)
        {
            bulletDamage = 1 * Mathf.Ceil(playerTime / 30);
        }
    }
}
