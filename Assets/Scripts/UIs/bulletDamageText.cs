using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bulletDamageText : MonoBehaviour
{
    //UI text
    public TMP_Text damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet amount = Base Mana
        damage.text = "Base Damage: " + gameManager.Instance.bulletDamage;

        //Hide it when player is dead
        if (gameManager.Instance.death == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
