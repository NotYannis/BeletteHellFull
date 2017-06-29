using UnityEngine;
using System.Collections;

public class ReadyFightScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("PlayerShmup").GetComponent<PlayerShmupController>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PlayFight(){
		//GetComponents<AudioSource>()[1].Play();
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("intro_fight");
		Debug.Log("PLAY FIGHT");
	}
	public void ReactivatePlayer(){
		GameObject.Find("PlayerShmup").GetComponent<PlayerShmupController>().enabled = true;
	}
}
