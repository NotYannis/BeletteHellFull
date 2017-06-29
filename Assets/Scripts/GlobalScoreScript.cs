using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalScoreScript : MonoBehaviour {
    private int scoreJG = 0;
    private int scoreJS = 0;

    private Text scoreJGText;
    private Text scoreJSText;

    [Tooltip("Le score qu'a le JG au début de la partie")]
    public int scoreJGBeginning = 100;

    [Tooltip("Vitesse à laquelle l'income du JG augmente")]
    public float scoreJGIncomeRate = 1.0f;
    private float scoreJGIncomeCooldown = 0.0f;

    [Tooltip("Quantité de score que le JG gagne à chaque tic d'income")]
    public int scoreJGIncome = 10;

    [System.NonSerialized]
    public int numberOfenemies = 0; //TODO : Move this to an other script


    void Awake () {
        scoreJGText = GameObject.FindGameObjectWithTag("JGscore").GetComponent<Text>();
        scoreJSText = GameObject.FindGameObjectWithTag("JSscore").GetComponent<Text>();

        scoreJG = scoreJGBeginning;

        scoreJGText.text = scoreJG.ToString();
        scoreJSText.text = scoreJS.ToString();

    }
	
	void Update () {
        Income();
    }

    //Manage income of JG
    private void Income()
    {
        if (GetEnemiesOnScreen() > 1)
        {
            if(scoreJGIncomeCooldown <= 0.0f)
            {
                addScoreJG(scoreJGIncome);
                scoreJGIncomeCooldown = scoreJGIncomeRate;
            }
            else
            {
                scoreJGIncomeCooldown -= Time.deltaTime;
            }
        }
        else //When no enemies is left, reset the coooldown
        {
            scoreJGIncomeCooldown = 0.0f;
        }
    }

    public void addScoreJG(int score)
    {
        scoreJG += score;
        scoreJGText.text = scoreJG.ToString();
    }

    public void addScoreJS(int score)
    {
        scoreJS += score;
        scoreJSText.text = scoreJS.ToString();
    }

    //TODO : Move this to an other script
    private int GetEnemiesOnScreen()
    {
        return numberOfenemies;
    }

    public int getScoreJG()
    {
        return scoreJG;
    }

    public int getScoreJS()
    {
        return scoreJS;
    }
}
