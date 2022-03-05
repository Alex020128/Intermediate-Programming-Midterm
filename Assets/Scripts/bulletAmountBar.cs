using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class bulletAmountBar : MonoBehaviour
{
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.DOValue(gameManager.Instance.playerHealth / 100f, 0.5f);
    }
}
