using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bulletAmountText : MonoBehaviour
{
    //UI text
    public TMP_Text amount;

    // Start is called before the first frame update
    void Start()
    {
        amount = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet amount = Base Mana
        amount.text = "Base Mana: " + GameObject.Find("lmbBullet").GetComponent<bulletSpawner>().Bullets.Count;

        //Hide it when player is dead
        if (gameManager.Instance.death == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
