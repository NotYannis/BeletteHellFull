using UnityEngine;
using System.Collections;
//using UnityEditor;
public class LaserScript : MonoBehaviour {

	public GameObject head;
	public GameObject body;

	[Header("Laser pieces")]
	public GameObject laserStart;
	public GameObject laserMiddle;
	public GameObject laserEnd;

	private bool isInfinite = false;
	public float maxLaserSize = 40f;
	public float cadenceOfDamage = 1f;
	public float currentTimeOfDamage = 0;
	// Use this for initialization
	void Start () {
		//currentTimeOfDamage = -1;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector2 laserDirection = this.transform.right;
		RaycastHit2D hit = Physics2D.Raycast(
			this.transform.position, 
			laserDirection, 
			maxLaserSize
		);
		if (hit != null) {
			float currentLaserSize = maxLaserSize;
			if (hit.collider != null) {
				if(!laserEnd.activeSelf) laserEnd.SetActive (true);
				isInfinite = false;
				currentLaserSize = Vector2.Distance (hit.collider.transform.position, this.transform.position);
				currentLaserSize -= hit.collider.bounds.extents.x/2;

			} else {
				isInfinite = true;
				laserEnd.SetActive (false);
			}

			float startSpriteWidth = laserStart.GetComponent<SpriteRenderer> ().bounds.size.x;
			float endSpriteWidth = 0f;
			if (!isInfinite)
				endSpriteWidth = laserEnd.GetComponent<SpriteRenderer> ().bounds.size.x;

			// -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
			laserMiddle.transform.localScale = new Vector3 (currentLaserSize - startSpriteWidth, laserMiddle.transform.localScale.y, laserMiddle.transform.localScale.z);
			laserMiddle.transform.localPosition = new Vector3 ((currentLaserSize / 2f), 0.011f, 0);

			if (!isInfinite) {
				laserEnd.transform.localPosition = new Vector3 (currentLaserSize- 0.037f, 0.011f, -0.21f);
			}
			if (hit.collider) {
				if (hit.collider.gameObject.GetComponent<HealthScript> () != null) {
						currentTimeOfDamage += Time.deltaTime;
						if(currentTimeOfDamage>cadenceOfDamage){
							//CheckWeakPointSound(hit.collider.gameObject);
							hit.collider.gameObject.GetComponent<HealthScript> ().Damage (laserMiddle.GetComponent<ShotScript> ().damage);	
							if(hit.collider.gameObject.GetComponent<CollisionHandlerScript> ()!=null)
								hit.collider.gameObject.GetComponent<CollisionHandlerScript> ().LaserDamage();
							currentTimeOfDamage=0;
						}	
				}	
			}
		}else {
			currentTimeOfDamage = -1;
		}
	}
	void CheckWeakPointSound(GameObject hitGameObject){
		if (hitGameObject.GetComponent<WeakPointSFX> () != null) {
			GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().PlayEvent ("jg_impact_shield_laser");
			//weakPointLaserEffect = hitGameObject.GetComponents<AudioSource> ()[2];
			//weakPointLaserEffect.Play();
		} else if (hitGameObject.GetComponent<BossMovementScript> () != null) {
			
		}

	}
	void OnDestroy(){
		if(weakPointLaserEffect!=null) {
			weakPointLaserEffect.Stop();
			weakPointLaserEffect = null;
		}
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().StopEvent ("jg_impact_ship_laser",0);
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().StopEvent ("jg_impact_shield_laser",0);
		//GameObject.Find ("GamePlayScript").GetComponent<AudioMaster> ().StopEvent ("jg_impact_shield_laser", 0);
	}
	public AudioSource weakPointLaserEffect;

}
