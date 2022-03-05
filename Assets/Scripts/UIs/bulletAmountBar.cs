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
        slider.DOValue(gameManager.Instance.bulletAmountEXP / gameManager.Instance.bulletAmountEXPBar, 0.5f);

        if (gameManager.Instance.death == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
