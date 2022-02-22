using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnUpDown : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private GameObject prefabMeleeEnemy = null;
    [SerializeField]
    private GameObject prefabRangeEnemy = null;
    [SerializeField]
    private float spawnPerSecond = 2f;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        //Spawn a pool of enemies at top of the screen
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
    }

    public void spawnEnemy()
    {
        int chance = Random.Range(0, 100);
        if (chance <= 20)
        {
            prefabToSpawn = prefabRangeEnemy;
        }
        else
        {
            prefabToSpawn = prefabMeleeEnemy;
        }

        float spawnY = Random.Range(0, 2) == 0 ? Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5 : Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + 5;
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, this.gameObject.transform);
        enemies.Add(newEnemy);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        while (spawnTimer < 0.0f && gameManager.Instance.death == false)
        {
            spawnTimer += spawnPerSecond;
            
            spawnEnemy();
        }
    }
}
