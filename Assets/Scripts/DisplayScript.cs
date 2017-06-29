using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DisplayScript : MonoBehaviour {
    AsyncOperation loadingMainscene;

    // Use this for initialization
    void Start () {
        if(Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
        loadingMainscene = SceneManager.LoadSceneAsync(2);
        loadingMainscene.allowSceneActivation = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(loadingMainscene.progress == 0.9f)
        {
            SceneManager.LoadScene(1);
        }
	}
}
