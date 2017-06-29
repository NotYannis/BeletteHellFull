using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOverMenuScript : MonoBehaviour {

    uint bankID;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
            Destroy(GameObject.Find("GamePlayScript"));
            SceneManager.LoadScene ("MainScene Copy", LoadSceneMode.Single);
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            Destroy(GameObject.Find("GamePlayScript"));
            SceneManager.LoadScene("JoanScene 1", LoadSceneMode.Single);
        }
    }

	public void Clikikiki(string option)
	{
		/*Do your stuff here*/
		if (option == "quit") {
		Debug.Log("OnMouseDown");
            Destroy(GameObject.Find("GamePlayScript"));
            SceneManager.LoadScene ("MainScene Copy", LoadSceneMode.Single);
        }
        else if(option == "replay"){
            Destroy(GameObject.Find("GamePlayScript"));
			SceneManager.LoadScene ("JoanScene 1", LoadSceneMode.Single);
        }
    }
}
