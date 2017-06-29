using UnityEngine;
using System.Collections;

public class CreateMenu : MonoBehaviour {

    public GameObject menu;
    public Camera cameraJoueurGestion;

	// Use this for initialization
	void Awake () {
        Camera cam = Instantiate(cameraJoueurGestion);
        GameObject obj = Instantiate(menu) as GameObject;
        obj.GetComponent<Canvas>().worldCamera = cam;
	}
}
