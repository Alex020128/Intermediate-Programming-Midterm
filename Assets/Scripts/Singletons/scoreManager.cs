using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreManager : Singleton<scoreManager>
{
    //Components of the total score
    public float Score;
    public float meleeEnemyKills;
    public float rangeEnemyKills;
    public float Hit;

    //UI text
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
        //The initial position of the UI text
        transform.localPosition = new Vector2(0, 206.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Record your score, which is related to total play time, total kills of the enemies, and the number of times when you get hit
        if (gameManager.Instance.death == false)
        {
            Score = Mathf.Floor(meleeEnemyKills * 100 + rangeEnemyKills * 150 + timeManager.Instance.playerTime * 10 - Hit * 100);
        }

        //Score cannot be negative
        if(Score < 0)
        {
            Score = 0;
        }
        
        if (gameManager.Instance.death == true)
        {
            //Place the UI text in the middle of the screen
            transform.localPosition = new Vector2(0, 0);

            //Display total time lasted, total score, and instruction for restart
            score.text = "You lasted: " + Mathf.Round(timeManager.Instance.playerTime) + "s\nTotal score: " + Score + "\n\nPress Space to Restart";

            //Reload scene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Map");
            }
        } else
        {
            //Display total score
            score.text = "Score: " + Score;
        }
    }
}
