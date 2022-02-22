using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private float spawnPerSecond = 0.5f;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private GameObject[] bullets = new GameObject[10];

    private void Start()
    {
        //Spawn a pool of bullets at top of the screen
        for (int i = 0; i < bullets.Length; i++)
        {
            GameObject newBullet = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, this.gameObject.transform);
            bullets[i] = newBullet;
            bullets[i].SetActive(false);
        }
    }

    public void shootBullet()
    {
        //let one of the waiting bullets to be active
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                bullets[i].GetComponent<Bullet>().lifeTimer = 3.0f;
                bullets[i].transform.position = transform.position;
                bullets[i].transform.parent = GameObject.Find("lmbBullet").transform;
                break;
            }
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        while (spawnTimer < 0.0f && gameManager.Instance.death == false)
        {
            spawnTimer += spawnPerSecond;
        }

        transform.position = GameObject.Find("Player").transform.position;
    }
}
