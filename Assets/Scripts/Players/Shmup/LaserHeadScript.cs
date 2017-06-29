using UnityEngine;
using System.Collections;


public class LaserHeadScript : MonoBehaviour {

	public bool headCollided;
	public GameObject otherCollided;
	// Use this for initialization
	void Start () {
		headCollided = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter2D(Collision2D other){
		

	}
	void OnTriggerEnter2D (Collider2D other) {
		if (!headCollided) {
			Debug.Log ("Ha colisionado");
			headCollided = true;
			otherCollided = other.gameObject;
			Debug.Log (other.gameObject.name);	
		}

		//Destroy (gameObject);
	}
}
	