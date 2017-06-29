using UnityEngine;
using System.Collections;

public class RadarScript : MonoBehaviour {

    GameObject playerShmup;

	// Use this for initialization
	void Start () {
        playerShmup = GameObject.Find("PlayerShmup");
	}
	
	// Update is called once per frame
	void Update () {
		if(playerShmup!=null) {
			float y = Mathf.Lerp(-500, 500, Mathf.InverseLerp(-9, 9, playerShmup.transform.localPosition.y));
			transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);	
		}
	}
}
