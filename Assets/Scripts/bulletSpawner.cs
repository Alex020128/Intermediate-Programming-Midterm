using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private float spawnPerSecond = 0.1f;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private List<GameObject> bullets = new List<GameObject>();

    public AudioSource audioSource;

    public AudioClip bulletSound;
    public List<GameObject> Bullets
    {
        get
        {
            return bullets;
        }
    }

    public float bulletDamage;
    public float missileDamage;

    private void Start()
    {
        //Spawn a pool of bullets at top of the screen
        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, this.gameObject.transform);
            bullets.Add(newBullet);
            bullets[i].SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void bulletSFX()
    {
        audioSource.Stop();
        audioSource.clip = bulletSound;
        audioSource.Play();
    }


    public void shootBullet()
    {
        //let one of the waiting bullets to be active
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bulletSFX();
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

            GameObject.Find("Player").GetComponent<playerMovement>().shootBullet = false;
        }

        transform.position = GameObject.Find("Player").transform.position;
    }
}
