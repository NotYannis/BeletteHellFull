using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TutoMenuScript : MonoBehaviour {

	public bool canStartGame = false;
	public bool startedGame = false;
	public GameObject[] pageTutoList;
	public int currentTuto;
	public bool canChange;
	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().PlayEvent ("music_menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ActivateOnMouseDonw(){
		canStartGame = true;
	}
	void OnMouseDown()
	{
		/*Do your stuff here*/
		if (canStartGame && !startedGame) {
            /*startedGame = true;
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().PlayEvent ("menu_choice");
			Invoke ("StartGame", 3.311f);*/
            if (canChange)
            {
                canChange = false;
                ChangePageTuto();
                Invoke("ResetCanChange", 1.5f);
            }
            
        }
	}
    void ChangePageTuto()
    {
        if (currentTuto < 3)
        {
            currentTuto++;
            for (int i = 0; i < pageTutoList.Length; i++)
            {
                if (i == currentTuto)
                    pageTutoList[currentTuto].SetActive(true);
                else
                    pageTutoList[i].SetActive(false);
            }
        }else {
            startedGame = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioMaster>().PlayEvent("menu_choice");
            Destroy(GameObject.FindGameObjectWithTag("MainCamera"), 3.0f);

            Invoke("StartGame", 3.311f);
        }
    }
    void ResetCanChange()
    {
        canChange = true;
    }
    public void StartGame() {
		//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioMaster> ().StopEvent ("music_menu", 1);
		SceneManager.LoadScene ("JoanScene 1", LoadSceneMode.Single );
		Debug.Log ("StartGame!!!!");	
	}
}
