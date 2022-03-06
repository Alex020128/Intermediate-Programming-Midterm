using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeManager : Singleton<timeManager>
{
    //Total play time
    public float playerTime;

    //UI text
    public TMP_Text time;

    //Enemy and spawn stats
    public float meleeEnemyHealth;
    public float rangeEnemyHealth;

    public float spawnSize;
    public float spawnFrequency;

    void Awake()
    {
        name = "TimeManager"; // Set name of object
        
        time = GetComponent<TMP_Text>();

        playerTime = 0;
        meleeEnemyHealth = 3;
        rangeEnemyHealth = 2;

        spawnSize = 30;
        spawnFrequency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.Instance.death == false)
        {
            //Record the total play time
            playerTime += Time.deltaTime;
            time.text = "Time: " + Mathf.Round(playerTime) + "s\nEnemy Level: " + Mathf.Ceil(playerTime / 20);
        } else
        {
           //Hide this when player is dead
            time.enabled = false;
        }

        //Increase the enemy stats
        if (Mathf.Floor(playerTime) % 20 == 0 && Mathf.Floor(playerTime) != 0)
        {
            //increases the enemy health stats
            meleeEnemyHealth = 3 * Mathf.Ceil(playerTime / 20);
            rangeEnemyHealth = 2 * Mathf.Ceil(playerTime / 20);

            //increases the maximum enemy size and the spawn frequency
            spawnSize = 30 + 10 * Mathf.Floor(playerTime / 20);
            spawnFrequency = 5 - 0.5f * Mathf.Floor(playerTime / 20);
            //The maximum spawn frequency
            if(spawnFrequency <= 0)
            {
                spawnFrequency = 0.5f;
            }
        }
    }
}
