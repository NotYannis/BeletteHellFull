using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerShumpHUDController : MonoBehaviour {

	public int playerLife = 3;
	public GameObject lifeHUD;
	public GameObject lifeIcon;
	public List<GameObject> heartList;
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("PlayerShmup") != null) {
			playerLife = GameObject.Find ("PlayerShmup").GetComponent<HealthScript> ().hp;
		}
		heartList = new List<GameObject>();
		StartHUDHearth ();
	}

	void StartHUDHearth(){
		GameObject heart = lifeHUD.transform.GetChild (0).gameObject;//It's necessary to have a heart as reference
		heartList.Add(heart);
		RectTransform heartTrasnform = heart.GetComponent<RectTransform>();
		float heartOffset = 7.5f;
		for (int i = 1; i < playerLife; i++) {
			GameObject newHeart = Instantiate (lifeIcon) as GameObject;
			heartList.Add(newHeart);
			newHeart.transform.SetParent (lifeHUD.transform, false);// lifeHUD.GetComponent<RectTransform>().transform;
			Vector3 heartRectPosition = newHeart.GetComponent<RectTransform>().anchoredPosition;
			heartRectPosition.x = heartRectPosition.x + (newHeart.GetComponent<RectTransform> ().rect.width * i) + (heartOffset*i);
			newHeart.GetComponent<RectTransform> ().anchoredPosition = heartRectPosition;
//			Debug.Log (newHeart.GetComponent<RectTransform> ().rect.width);
		}

	}
	public void UpdateLife(int subtractedLife){
		for (int i = 0; i < subtractedLife; i++) {
			playerLife--;
			if(playerLife>=0) Destroy (lifeHUD.transform.GetChild (playerLife).gameObject);
		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
