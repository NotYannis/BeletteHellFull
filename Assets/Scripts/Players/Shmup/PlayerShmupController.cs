using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShmupController : MonoBehaviour {

    [Tooltip("Vitesse de déplacement")]
    public Vector2 velocity = new Vector2(50,50);
    [Tooltip("Nombre de vies")]
    public int life;

    public GameObject deathScreen;

    private Vector2 movement;
    private Rigidbody2D rig;
	private PlayerWeaponScript weapon;
    private BoxCollider2D collider;

	public XboxController controller;

	public GameObject playerRender;
	public Animator playerAnim;

	public GameObject playerShotgunRender;
	public Animator playerShotgunAnim;

	public GameObject playerWeapon;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        weapon = playerWeapon.GetComponent<PlayerWeaponScript>();
        collider = GetComponent<BoxCollider2D>();
		playerAnim = playerRender.GetComponent<Animator> ();
		playerShotgunAnim = playerShotgunRender.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
		ChangeWeapon();
        Shoot();
        ScreenCollision();
	}

    private void Move()
    {
		float verticalMove = Input.GetAxis("Vertical");
		if(verticalMove == 0 ) verticalMove = XCI.GetAxis(XboxAxis.LeftStickY, controller);
		float horizontalMove = Input.GetAxis("Horizontal");
		if(horizontalMove == 0 ) horizontalMove = XCI.GetAxis(XboxAxis.LeftStickX, controller);
		if (horizontalMove != 0) {
			if (horizontalMove > 0) {
				playerAnim.SetBool ("moveRight", true);
				playerAnim.SetBool ("moveLeft", false);
			}
			if (horizontalMove < 0) {
				playerAnim.SetBool ("moveRight", false);
				playerAnim.SetBool ("moveLeft", true);
			}
		} else {
			playerAnim.SetBool ("moveRight", false);
			playerAnim.SetBool ("moveLeft", false);
		}
        movement = new Vector2(velocity.x * horizontalMove, velocity.y * verticalMove);
        rig.velocity = movement;
    }
	//This method is temporal
	private void ChangeWeapon() {
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			weapon.ChangeWeapon(0);
		}else if(Input.GetKeyDown(KeyCode.Alpha2)) {
			weapon.ChangeWeapon(1);
		}else if(Input.GetKeyDown(KeyCode.Alpha3)) {
			weapon.ChangeWeapon(2);
		}else if(Input.GetKeyDown(KeyCode.Alpha4)) {
			weapon.ChangeWeapon(3);
		}else if(Input.GetKeyDown(KeyCode.Alpha5)) {
			//weapon.ChangeWeapon(4);
			CreateShield();
		}else if(Input.GetKeyDown(KeyCode.Alpha6)) {
			//weapon.ChangeWeapon(5);
			CreateShield();
		}
	}
    private void Shoot()
    {
		bool shot = Input.GetButton("Fire") || XCI.GetButton(XboxButton.A, controller) || XCI.GetButton(XboxButton.LeftBumper, controller);
		if (weapon.continuousShot) {
			bool shotReleased = Input.GetButtonUp ("Fire") || XCI.GetButtonUp (XboxButton.A, controller) || XCI.GetButtonUp (XboxButton.LeftBumper, controller);
			if (shotReleased) {
				weapon.StopContinousShot ();
			}
			weapon.updateContinuousShot ();
		} else {
			if (shot) {
				
				weapon.Attack(false);
			}	
		}
    }

    private void ScreenCollision()
    {
        float dist = (transform.position - Camera.main.transform.position).z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + GetComponent<Collider2D>().bounds.size.x/2;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - GetComponent<Collider2D>().bounds.size.y/2;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + GetComponent<Collider2D>().bounds.size.x/2;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - GetComponent<Collider2D>().bounds.size.x/2;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);
    }

    public void CreateDeathScreen(){
    	Instantiate(deathScreen, new Vector2(0.0f, 0.0f), Quaternion.identity);
    	this.enabled = false;
    }

	public void ApplyBonusEffect(BonusScript.BonusType bonusType){
		if (bonusType != BonusScript.BonusType.Shield) {
			if (GameObject.Find ("WeaponHUD") != null) {
				GameObject.Find ("WeaponHUD").GetComponent<WeaponHudScript> ().SetActiveBonus ((int)bonusType);
			}
			weapon.ChangeWeapon ((int)bonusType);
		}else {
			CreateShield();
			if (GameObject.Find ("WeaponHUD") != null) {
				GameObject.Find ("WeaponHUD").GetComponent<WeaponHudScript> ().SetActiveBonus (0);
			}
		}
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("js_bonus");
		//GetComponents<AudioSource>()[0].Play();
	}
	private void CreateShield(){
		GameObject ShmupShield = GameObject.Find("ShmupShield");
		GameObject ShmupShieldClone = GameObject.Find("ShmupShield(Clone)");
		if(ShmupShield == null && ShmupShieldClone == null){
			GameObject shieldInstance = Instantiate(Resources.Load("Prefabs/Shmup/ShmupShield", typeof(GameObject))) as GameObject;
			shieldInstance.transform.parent = transform.parent;
			shieldInstance.transform.localPosition = new Vector3(-0.04f,-0.35f,0);
			GetComponent<HealthScript>().destructible = false;
		}
	}
}
