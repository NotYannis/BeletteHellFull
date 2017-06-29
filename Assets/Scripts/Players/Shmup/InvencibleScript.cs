using UnityEngine;
using System.Collections;

public class InvencibleScript : MonoBehaviour {

	public bool hasInvencibilite = false;
	public float invincibleTime = 0;
	public float currentInvincibleTime = 0;
	public bool isBlinkable = false;
	public float currentBlinkableTime = 0;
	public float blinkableTime = 0;

	[Tooltip("Invincible ?")]
	public bool destructible;
	// Use this for initialization
	void Start () {
	
	}
	public void StartInvencibility(){
		destructible = false;
		hasInvencibilite = true;
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("js_lifedown");
		//GetComponents<AudioSource>()[1].Play();
	}
	// Update is called once per frame
	void Update () {
		if(hasInvencibilite) UpdateInvencibleBehaviour();
	}

	void UpdateInvencibleBehaviour() {
		if(!destructible){
			currentInvincibleTime += Time.deltaTime;
			if(currentInvincibleTime > invincibleTime) {
				currentInvincibleTime = 0;
				destructible = true;
				if(GetComponent<HealthScript>()) 
					GetComponent<HealthScript>().destructible = true;
			}
			if(isBlinkable) {
				currentBlinkableTime += Time.deltaTime;
				if(currentBlinkableTime >=blinkableTime || destructible){
					currentBlinkableTime = 0;
					SpriteRenderer[] spriteList = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer sprite in spriteList){
						Color tmp = sprite.color;
						tmp.a = (destructible)? 1f : ((tmp.a == 1f)? 0.25f:1f);
						sprite.color = tmp;
					}
				}
			}
		}
	}
}
