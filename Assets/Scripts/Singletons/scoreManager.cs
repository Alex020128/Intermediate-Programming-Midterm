using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreManager : Singleton<scoreManager>
{
    public float Score;

    public float meleeEnemyKills;
    public float rangeEnemyKills;
    public float Hit;

    public TMP_Text score;

    // Start is called before the first frame update
    void Awake()
    {
        Score = 0;
        meleeEnemyKills = 0;
        rangeEnemyKills = 0;
        Hit = 0;

        name = "ScoreManager"; // Set name of object
        score = GetComponent<TMP_Text>();
    }

    void Start()
    {
        transform.localPosition = new Vector2(0, 206.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.death == false)
        {
            Score = Mathf.Floor(meleeEnemyKills * 100 + rangeEnemyKills * 150 + timeManager.Instance.playerTime * 10 - Hit * 100);
        }

        if(Score < 0)
        {
            Score = 0;
        }
        if (gameManager.Instance.death == true)
        {
            transform.localPosition = new Vector2(0, 0);

            //Display total time lasted and instruction for restart
            score.text = "You lasted: " + Mathf.Round(timeManager.Instance.playerTime) + "s\nTotal score: " + Score + "\n\nPress Space to Restart";

            //Reload scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Map");
            }
        } else
        {
            score.text = "Score: " + Score;
        }
    }
}
