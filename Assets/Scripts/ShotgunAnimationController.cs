using UnityEngine;
using System.Collections;

public class ShotgunAnimationController : MonoBehaviour {

	public Animator shotgunAnim;
	// Use this for initialization
	void Start () {
		shotgunAnim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void startAttack() {
		shotgunAnim.SetBool ("attack", true);
	}
	public void finishAttack(){
		shotgunAnim.SetBool ("attack", false);
	}
}
