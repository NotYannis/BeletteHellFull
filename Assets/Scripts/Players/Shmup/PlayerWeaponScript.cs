using UnityEngine;
using System.Collections;

public class PlayerWeaponScript : MonoBehaviour {

	[Tooltip("Prefab du projectile")]
	public Transform currentShot;
	public Transform defaultShot;
	[Tooltip("Cadence de tir (en millisecondes)")]
	public float fireRate = 0.25f;

	private float fireCooldown;
	public GameObject originPoint;
	public Transform[] shots;//1:Single, 2:Laser, 3:Conic, 4:Rocket
	public float[] fireRates = {0.25f, 0f, 0.25f, 0.5f};
	public int[] weaponMaxBullets = { -1, 5, 10, 10 };// -1: means infinite, if isContinuousShot, the bullets means the time (in seconds) of shooting;
	public float currentAmmo;
	// Use this for initialization
	public bool continuousShot = false;
	public GameObject continuousShotInstance;
	void Start () {
		currentShot = defaultShot;
		fireRate = fireRates [0];
		currentAmmo = weaponMaxBullets [0];

	}

	public void ChangeWeapon(int numWeapon) {
		if(shots[numWeapon] !=null){
			currentShot = shots[numWeapon];
			fireRate = fireRates [numWeapon];
			currentAmmo = weaponMaxBullets [numWeapon];
		}else {
			currentShot = defaultShot;
			fireRate = fireRates [0];
			currentAmmo = weaponMaxBullets [0];
		}
	}

	// Update is called once per frame
	void Update () {
		if(fireCooldown > 0)
		{
			fireCooldown -= Time.deltaTime;
		}
	}

	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			if (!continuousShot) {
				fireCooldown = fireRate;
				Transform shotTransform = Instantiate (currentShot) as Transform;

				shotTransform.position = originPoint.transform.position;
				GetComponentInChildren<ShotgunAnimationController> ().startAttack ();
				ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript> ();
				if (shot != null) {
					shot.isEnemy = isEnemy;
				}	
				if (currentShot.name == "FinalLaser") {
					continuousShot = true;
					continuousShotInstance = shotTransform.gameObject;
					shotTransform.parent = gameObject.transform;
				} else {
					if (currentAmmo != -1) {
						currentAmmo--;
						if (currentAmmo <= 0) {
							if (GameObject.Find ("WeaponHUD") != null) {
								GameObject.Find ("WeaponHUD").GetComponent<WeaponHudScript> ().SetActiveBonus (0);
							}
							ChangeWeapon (0);		
						}
					}
				}
			}
		}
	}
	public void updateContinuousShot() {
		if (currentShot.name == "FinalLaser") {
			currentAmmo -= Time.deltaTime;
			if (currentAmmo < 0) {
				StopContinousShot ();
				ChangeWeapon (0);
			}
		}
	}
	public void StopContinousShot(){
		continuousShot = false;
		Destroy (continuousShotInstance);
	}
	public bool CanAttack
	{
		get
		{
			return fireCooldown <= 0.0f;
		}
	}
}
