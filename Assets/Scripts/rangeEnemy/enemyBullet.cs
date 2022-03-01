using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class enemyBullet : MonoBehaviour
{
    [SerializeField]
    public float turnSpeed = 360; // degrees per second
    [SerializeField]
    public float lifeTimer;
    public float moveSpeed = 10;

    public Vector3 targetRotation;

    public Transform player;


    private void Awake()
    {
        lifeTimer = 3.0f;
    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Decrease health, emit particle, trigger sreenshake when gets hit by bullets
        if (collision.gameObject.tag == "playerArea" && gameManager.Instance.invinsible == false)
        {
            gameManager.Instance.playerHealth -= 1;
            //explode.Emit(7);
            Camera.main.transform.DOShakePosition(0.5f, new Vector3(0.5f, 0.5f, 0));
            gameManager.Instance.invinsibleTime = 0;
            gameManager.Instance.invinsible = true;
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {


        lifeTimer -= Time.deltaTime;


        if (lifeTimer <= 2.5f)
        {
            //Move along the latest rotation(towards the player)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            transform.parent = null;
            if (lifeTimer <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {

            //Sets the rotation of the bullet towards the player
            Vector3 playerPosition = player.position;
            Vector2 direction = playerPosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            targetRotation = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.Euler(targetRotation);

            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
