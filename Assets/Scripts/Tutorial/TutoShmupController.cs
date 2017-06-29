using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class TutoShmupController : MonoBehaviour {
	public XboxController controller;
	public GameObject[] pageTutoList;
	public int currentTuto;
	public bool canChange;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canChange) {
			if (Input.GetKeyDown (KeyCode.Return) || XCI.GetButton (XboxButton.A, controller)) {
				canChange = false;
				ChangePageTuto ();
				Invoke ("ResetCanChange", 1.5f);
				//Destroy (this);
			}
		}

	}
	void ResetCanChange() {
		canChange = true;
	}
	void ChangePageTuto() {
		if (currentTuto < 2) {
			currentTuto++;
			for (int i = 0; i < pageTutoList.Length; i++) {
				if(i==currentTuto)
					pageTutoList [currentTuto].SetActive (true);
				else
					pageTutoList [i].SetActive (false);
			}
		}
	}
}
