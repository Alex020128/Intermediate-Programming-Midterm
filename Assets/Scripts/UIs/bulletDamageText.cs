using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bulletDamageText : MonoBehaviour
{
    public TMP_Text damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damage.text = "Base Damage: " + gameManager.Instance.bulletDamage;

        if (gameManager.Instance.death == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
