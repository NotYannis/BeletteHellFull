using UnityEngine;
using System.Collections;

public class WeaponHudScript : MonoBehaviour {

	public GameObject[] list;
	// Use this for initialization
	void Start () {
		foreach (GameObject item in list) {
			if(item.name != "Single")
				item.SetActive (false);
		}
	}

	public void SetActiveBonus(int current) {
		for (int i = 0; i < list.Length; i++) {
			if (i != current) {
				list [i].SetActive (false);
			} else {
				list [i].SetActive (true);
			}
		}
		GetComponent<Animator> ().SetBool ("restart", true);
	}
	public void ChangeRestartAnim(){
		GetComponent<Animator> ().SetBool ("restart", false);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
