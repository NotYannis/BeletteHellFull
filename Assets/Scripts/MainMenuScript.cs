using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using XboxCtrlrInput;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour {

	public XboxController controller;
	public enum States
	{
		Start = 0 ,
		Quit = 1,
		None = 2/*,
		Credit = 2*/
	}
	public GameObject[] buttons;
	public States currentState;
	public float delayChangeState;
	public float currentDelayChangeState;
	public float scaleButtonSelected = 1;
	// Use this for initialization
	void Start () {
		currentState = States.Start;
		currentDelayChangeState = 0 ;
		buttons[(int)currentState].transform.localScale = new Vector3(scaleButtonSelected,scaleButtonSelected,1);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().PlayEvent ("music_menu");
	}
	void ChangeState(bool direction) {
		buttons[(int)currentState].transform.localScale = new Vector3(1,1,1);
		if(direction) {
			if(currentState == States.Quit)
				currentState = States.Start;
			else
				currentState++;
		}else {
			if(currentState == States.Start)
				currentState = States.Quit;
			else 
				currentState--;
		}
		buttons[(int)currentState].transform.localScale = new Vector3(scaleButtonSelected,scaleButtonSelected,1);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster>().PlayEvent("menu_validate");
	}
	// Update is called once per frame
	void Update () {
		if (!startedTuto) {
			float verticalMove = XCI.GetAxis (XboxAxis.LeftStickY, controller);
			if (verticalMove != 0) {
				currentDelayChangeState += Time.deltaTime;
				if (currentDelayChangeState > delayChangeState) {
					ChangeState ((verticalMove < 0) ? true : false);
					currentDelayChangeState = 0;
				}
			} else {
				if (Input.GetKeyDown (KeyCode.UpArrow))
					verticalMove = 1;
				else if (Input.GetKeyDown (KeyCode.DownArrow))
					verticalMove = -1;
				if (verticalMove != 0)
					ChangeState ((verticalMove < 0) ? true : false);
			}
			if (Input.GetKeyDown (KeyCode.Return) || XCI.GetButton (XboxButton.A, controller)) {
				StartSelection (currentState);	
				//Destroy (this);
			}
		}
	}
	public void OnClikiki(){
		StartSelection(States.Start);
	}
	public void StartSelection(States newState){
		if (!startedTuto) {
			switch(newState) {
			case States.Start: 
				startedTuto = true;
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().PlayEvent ("menu_choice");

				Invoke ("StartTutorial", 3.311f);
                    Destroy(GameObject.FindGameObjectWithTag("MainCamera"), 3.0f);
                    break;
				case States.Quit:
					break;
				}
		}
	}

    public void Quit()
    {
        Application.Quit();
    }

	public void StartTutorial() {
		//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().StopEvent ("music_menu",1);
		SceneManager.LoadScene (2);
	}
	/*IEnumerator StartGame(){
		yield return new WaitForSeconds(GetComponents<AudioSource>()[1].clip.length-0.25f);
		//GetComponents<AudioSource>()[1].Play();
		Debug.Log("StartGame");
		SceneManager.LoadScene ("TutoScene", LoadSceneMode.Single);
	}*/
	public bool startedTuto = false;

}
