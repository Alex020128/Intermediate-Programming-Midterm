using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn = null;
    [SerializeField]
    private float spawnPerSecond = 10f;
    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private List<GameObject> missiles = new List<GameObject>();
    public List<GameObject> Missiles
    {
        get
        {
            return missiles;
        }
    }

    private void Start()
    {
        //Spawn one missile at top of the screen
        for (int i = 0; i < 1; i++)
        {
            GameObject newMissile = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, this.gameObject.transform);
            missiles.Add(newMissile);
            missiles[i].SetActive(false);
        }
    }

    public void shootMissile()
    {
        //let one of the waiting bullets to be active
        for (int i = 0; i < missiles.Count; i++)
        {
            if (!missiles[i].activeInHierarchy)
            {
                missiles[i].SetActive(true);
                missiles[i].GetComponent<Missile>().lifeTimer = 2.0f;
                
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                missiles[i].transform.position = mousePosition;

                missiles[i].transform.parent = GameObject.Find("rmbMissile").transform;
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

            GameObject.Find("Player").GetComponent<playerMovement>().shootMissile = false;
        }
    }
}
