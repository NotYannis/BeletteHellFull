using UnityEngine;
using System.Collections;

public class ShmupShieldScript : MonoBehaviour {

	public int damage = 10;
	public int hp = 1;
	public bool disable = false;
	public Transform playerTransform;
	// Use this for initialization
	void Start () {
		playerTransform = GameObject.Find("PlayerShmup").GetComponent<Transform>();
	}
	public void StartShieldDestroy(){

		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("js_shield_destroy");
	}
	void Damage(int damageCount){
		hp -= damageCount;
		if (hp <= 0)
		{
			disable = true;
			if(GetComponent<CircleCollider2D>()!=null) Destroy (GetComponent <CircleCollider2D>());
			//GetComponents<AudioSource>()[1].Play();
			GetComponent<Animator>().SetBool("isDestroyed",true);
		}
	}
	// Update is called once per frame
	void Update () {
		if(playerTransform!=null){
			Vector3 playerPosition = playerTransform.position;
			transform.position = new Vector3(playerPosition.x-0.03999996f,playerPosition.y-0.35f,-5);
		}
			
	}
	public void DestroyShield(){
		GameObject.Find ("PlayerShmup").GetComponent<HealthScript> ().destructible = true;
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		if(!disable) {
			ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
			if(shot != null)
			{
				if(shot.isEnemy)
				{
					Damage(shot.damage);
					if(!shot.isIndestructible) Destroy(shot.gameObject);
				}
			}
			//If JS is touched by a belette, kill him
			HealthScript health = otherCollider.gameObject.GetComponent<HealthScript>();
			if(health != null)
			{
				if (health.isEnemy)
				{
					if(!health.gameObject.name.Contains("weakpoint")) health.Damage(health.hp);
					Damage(damage);
				}
			}
		}

	}
}
