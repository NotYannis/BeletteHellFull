using UnityEngine;
using System.Collections;

public class WeakPointSFX : MonoBehaviour {
	// Use this for initialization
	public bool isCurrentWeakPoint;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D(Collider2D otherCollider){
		if (otherCollider.name == "LaserEnd") {
			GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().StopEvent ("jg_impact_ship_laser",0);
			GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().StopEvent ("jg_impact_shield_laser",0);
		}
	}
	void OnTriggerEnter2D(Collider2D otherCollider){
		if(otherCollider.GetComponent<ShotScript>()!=null){
			if (GameObject.Find ("ManagementScripts").GetComponent<WeakspotScript> ().shootingWindow) {
				if (otherCollider.name == "Bullet_wwise(Clone)")
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
				if (otherCollider.name == "BulletUp")//Triple shot
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
				if (otherCollider.name == "BulletDown")//Triple shot
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
				if (otherCollider.name == "Rocket_wwise(Clone)")
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_rocket");
				/*if(otherCollider.name == "LaserEnd")
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_laser");*/
			} else{
				if (isCurrentWeakPoint) {
					if (otherCollider.name == "Bullet_wwise(Clone)")
						GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_gun");
					if (otherCollider.name == "BulletUp")//Triple shot
						GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_gun");
					if (otherCollider.name == "BulletDown")//Triple shot
						GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_gun");
					if (otherCollider.name == "Rocket_wwise(Clone)")
						GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_rocket");
					if(otherCollider.name == "LaserEnd")
						GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_laser");
				} else {
					GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_null");
				}	
			}
		}
	}
}
