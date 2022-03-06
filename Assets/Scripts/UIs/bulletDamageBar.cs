using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class bulletDamageBar : MonoBehaviour
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
        //Lerps the bullet damage exp bar
        slider.DOValue(gameManager.Instance.bulletDamageEXP / gameManager.Instance.bulletDamageEXPBar, 0.5f);
        
        if (gameManager.Instance.death == true)
        {
            //Hide this when player is dead
            this.gameObject.SetActive(false);
        }
    }
}
