using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour {
    [Tooltip("Score pour le JS lors de la desctruction")]
    public int scoreJS = 10;

    [Tooltip("Score pour le JG quand arrive au bout")]
    public int scoreJG = 20;

    public GameObject scoreTextFeedback;

    private GameObject canvas;
    private GlobalScoreScript globalScore;

	void Start () {
        //globalScore = GameObject.FindGameObjectWithTag("script").GetComponent<GlobalScoreScript>();
        canvas = GameObject.FindGameObjectWithTag("canvas");

        //Update the number of enemies and udpate the score of JG
        //globalScore.numberOfenemies++;
	}
	
	void Update () {
       SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        //Add score to JG when reach the end of the screen

        if(transform.position.x < 
            Camera.main.transform.position.x  - (Camera.main.orthographicSize * Screen.width / Screen.height) - sprite.bounds.size.x)
        {
            //globalScore.addScoreJG(scoreJG);
            Destroy(gameObject);
            //--globalScore.numberOfenemies;
        }
	}


    //Add score to JS when unit dies
    public void addScoreOnDeath()
    {
        Vector3 textPosition = new Vector3(transform.position.x, transform.position.y + 1, 0);
        scoreTextFeedback.GetComponent<Text>().text = "+ " + scoreJS.ToString();

        GameObject scoreTextFeed = Instantiate(scoreTextFeedback, textPosition, Quaternion.identity, canvas.transform) as GameObject;

        Destroy(scoreTextFeed, 1.0f);

        globalScore.addScoreJS(scoreJS);
        --globalScore.numberOfenemies;
    }
}
