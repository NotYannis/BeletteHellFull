using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public GameObject gameOver;
	// Use this for initialization
	void Start () {
	
	}
	public void ActivateGameOver(string winner){
		gameOver.SetActive(true);
		gameOver.transform.Find(winner).gameObject.SetActive(false);
		if(GameObject.Find("PlayerShmup")!=null)
			GameObject.Find("PlayerShmup").GetComponent<PlayerShmupController>().enabled = false;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
