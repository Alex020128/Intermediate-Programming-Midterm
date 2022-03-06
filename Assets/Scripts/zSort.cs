using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSort : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Z-sort the objects
        sr.sortingOrder = -(int)(transform.position.y * 10);
    }
}
