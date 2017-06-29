using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathScreenScript : MonoBehaviour {

    
	//private GlobalScoreScript globalScore; //TODO : show score on the deathscreen
	public GameObject counter;
	public GameObject score;
	public int scoreCost = 1000;


	private int counterTime = 9;
	private float counterRate = 1.0f;
	private float counterCooldown = 1.0f;
	private Button[] yesNoButtons = new Button[2];


	// Use this for initialization
	void Start () {
		//globalScore = GameObject.FindGameObjectWithTag("script").GetComponent<GlobalScoreScript>();
		yesNoButtons = GetComponentsInChildren<Button>();
		Text scoreText = score.GetComponent<Text>();
		scoreText.text = "Buy a life for " + scoreCost.ToString() + " score !";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") == 1){
			yesNoButtons[0].Select();
		}
		if(Input.GetAxisRaw("Vertical") == -1){
			yesNoButtons[1].Select();
		}

		Text counterText = counter.GetComponent<Text>();

		if(counterCooldown <= 0.0f){
			counterTime--;
			counterText.text = counterTime.ToString();
			counter.transform.localScale = new Vector2(1.0f, 1.0f);
			counterCooldown = counterRate;
		}
		else{
			counterCooldown -= Time.deltaTime;
			counter.transform.localScale = new Vector2(counter.transform.localScale.x - 0.01f, counter.transform.localScale.y - 0.01f);
		}
	}
}
