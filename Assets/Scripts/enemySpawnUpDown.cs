using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnUpDown : MonoBehaviour
{
    //Enemies to spawn
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private GameObject prefabMeleeEnemy = null;
    [SerializeField]
    private GameObject prefabRangeEnemy = null;
    
    //Spawn stats
    [SerializeField]
    public float spawnPerSecond;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    private float enemySize;

    private void Start()
    {
        //Spawn a pool of enemies outside the camera
        for (int i = 0; i < 10; i++)
        {
            int chance = Random.Range(0, 100);
            if(chance <= 20)
            {
                prefabToSpawn = prefabRangeEnemy;
            }
            else
            {
                prefabToSpawn = prefabMeleeEnemy;
            }

            float spawnY = Random.Range(0, 2) == 0 ? Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5: Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + 5;
            float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            GameObject newEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, this.gameObject.transform);
            enemies.Add(newEnemy);
        }

        spawnPerSecond = 5f;
}

    public void spawnEnemy()
    {
        //80% to spawn melee enemy, 20% to spawn range enmemy
        int chance = Random.Range(0, 100);
        if (chance <= 20)
        {
            prefabToSpawn = prefabRangeEnemy;
        }
        else
        {
            prefabToSpawn = prefabMeleeEnemy;
        }

        //Spawn the enemies outside the camera
        float spawnY = Random.Range(0, 2) == 0 ? Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5 : Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + 5;
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, this.gameObject.transform);
        enemies.Add(newEnemy);
    }

    void Update()
    {
        //Keeps spawning enemeis
        spawnTimer -= Time.deltaTime;
        while (spawnTimer < 0.0f && gameManager.Instance.death == false && enemies.Count < enemySize)
        {
            spawnTimer += spawnPerSecond;
            
            spawnEnemy();
        }

        //Delete the dead enemies from the list
        for(int i = 0; i< enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }

        //Spawn stats changes as time goes by
        spawnPerSecond = timeManager.Instance.spawnFrequency;
        enemySize = timeManager.Instance.spawnSize;
    }
}
