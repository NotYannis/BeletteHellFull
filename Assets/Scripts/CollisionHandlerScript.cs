using UnityEngine;
using System.Collections;

public class CollisionHandlerScript : MonoBehaviour {

	private ManagerRessourcesScript managerRessources;
    private RoomAlert insideShip;

	// Use this for initialization
	void Start () {
		managerRessources = GameObject.Find("PlayerGestionUI/ManagementScripts").GetComponent<ManagerRessourcesScript>();
        insideShip = GameObject.Find("PlayerGestionUI/ManagementScripts").GetComponent<RoomAlert>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherColl){
        if(GameObject.Find("Mudaship").GetComponent<HealthScript>().hp > 0)
        {
		    if (GameObject.Find ("ManagementScripts").GetComponent<WeakspotScript>().shootingWindow) {
			    if (otherColl.name == "Bullet_wwise(Clone)")
				    GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
			    if (otherColl.name == "BulletUp")//Triple shot
				    GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
			    if (otherColl.name == "BulletDown")//Triple shot
				    GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_gun");
			    if (otherColl.name == "Rocket_wwise(Clone)")
				    GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_ship_rocket");
		    }
        }
		managerRessources.UpdateShipHealthBar();
        insideShip.StartShaking();
	}
	public void LaserDamage(){
		managerRessources.UpdateShipHealthBar();
	}
}
