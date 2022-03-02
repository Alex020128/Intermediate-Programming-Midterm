using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Missile : MonoBehaviour
{
    [SerializeField]
    public float lifeTimer;

    private PolygonCollider2D pc;

    private void Awake()
    {
        lifeTimer = 2.0f;
        pc = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        pc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0)
        {
            this.gameObject.SetActive(false);
        }else if(lifeTimer <= 0.5f)
        {
            pc.enabled = true;
            Camera.main.transform.DOShakePosition(0.5f, new Vector3(1, 1, 0));
        }
        else
        {
            pc.enabled = false;
        }
    }
}
