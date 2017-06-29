using UnityEngine;
using System.Collections;
//using UnityEditor;

public class HealthScript : MonoBehaviour {
    [Tooltip("Points de vie (no shit)")]
    public int hp = 1;
    [Tooltip("Méchant ou gentil ?")]
    public bool isEnemy;
    [Tooltip("Invincible ?")]
    public bool destructible;
	public bool isDeath = false;

    public void Damage(int damageCount)
    {
        if (destructible)
        {
			checkInvencibility();
            hp -= damageCount;
			checkPlayerHUD (damageCount);
            if (WeakPointPlayerShmupValidation())
            {
                WeakspotBlink();
            }
            if(gameObject.name == "Mudaship")
            {
                BossPartBlink();
            }
        }
        if (hp <= 0)
        {
            if(gameObject.GetComponentInParent<CollisionHandlerScript>() == null)
            {
				if(!isDeath){
                    if (name == "PlayerShmup")
                    {
                        DestroyPlayerShmup();
                    }
                    
                    else if (gameObject.GetComponent<ShipScriptAnimation>() != null)
                    {
                        ShipScriptAnimation anim = gameObject.GetComponent<ShipScriptAnimation>();
                        anim.deathTtrigger();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
				isDeath = true;
            }
            else if (name == "Mudaship" && !isDeath)
            {
                DestroyMudaship();
                isDeath = true;
            }
        }
    }

	private void DestroyPlayerShmup(){
		GameObject.Find("PlayerRender").GetComponent<Animator>().SetBool("death",true);
		GameObject.Find("JetpackRender").SetActive(false);
		GameObject.Find("PlayerShotgun").SetActive(false);
		//GetComponents<AudioSource>()[2].Play();
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("js_death");
		GameObject.Find("Canvas").GetComponentInChildren<GameOverScript>().ActivateGameOver("Belette");
        GameObject.Find("ManagementGameOver").GetComponent<GameOverScript>().ActivateGameOver("Belette");
    }
    private void checkPlayerHUD(int damageCount) {
		if (name == "PlayerShmup") {
			GameObject PlayerShmupHUD = GameObject.Find ("PlayerShmupHUD");
			if (PlayerShmupHUD!=null) {
				PlayerShmupHUD.GetComponent<PlayerShumpHUDController>().UpdateLife (damageCount);
			}
		}
	}
	private void checkInvencibility() {
		InvencibleScript invencible = GetComponent<InvencibleScript>();
		if(invencible!=null){
			destructible = false;
			invencible.StartInvencibility();
		}
	}

    private void DestroyMudaship()
    {
        GameObject mudaship = GameObject.Find("Mudaship");
		mudaship.GetComponent<SpriteRenderer> ().enabled = true;
        for(int i = 0; i < mudaship.transform.childCount; ++i)
        {
            Destroy(mudaship.transform.GetChild(i).gameObject);
        }
        Rigidbody2D mudashipRig = mudaship.AddComponent<Rigidbody2D>();
        mudashipRig.gravityScale = 0.05f;
        mudashipRig.AddTorque(50.0f, ForceMode2D.Force);
        mudaship.GetComponent<Animator>().enabled = true;
        mudaship.GetComponent<BossMovementScript>().enabled = false;
        Destroy(mudaship, 15.0f);

        SendZoneManager[] sendZoneContainer = GameObject.Find("Ship").GetComponentsInChildren<SendZoneManager>();
        foreach(SendZoneManager sz in sendZoneContainer)
        {
            sz.enabled = false;
        }
        GameObject.Find("ManagementScripts").SetActive(false);
		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("jg_death");
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //If a shot touch something, damage it
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if(shot != null)
		{
			if(shot.isEnemy != isEnemy)
			{
				if(!isDeath)
					Damage(shot.damage);
				if(!shot.isIndestructible) Destroy(shot.gameObject);
			}
        }

		//If JS is touched by a belette, kill him
		HealthScript health = otherCollider.gameObject.GetComponent<HealthScript>();
		if(health != null)
		{
			if (isEnemy && !health.isEnemy)
			{
                if (WeakPointPlayerShmupValidation())
                {
                    health.Damage(1);
                }
                else
                {
                    health.Damage(1);
                    Damage(hp);
                }
			}
		}


    }

    private void WeakspotBlink()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.2f, 0.2f);
        Invoke("WeakspotStopBlink", 0.15f);
    }

    private void WeakspotStopBlink()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    private void BossPartBlink()
    {
        if (GameObject.Find("Mudaship").transform.childCount > 0)
        {
            GameObject.Find("BossCoque").GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.2f, 0.2f);
            GameObject.Find("BossCabine").GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.2f, 0.2f);
            foreach (SpriteRenderer spr in GameObject.Find("weakpoints").GetComponentsInChildren<SpriteRenderer>())
            {
                spr.color = new Color(1.0f, 0.2f, 0.2f);
            }
        }
        Invoke("BossStopBlink", 0.15f);
    }

    private void BossStopBlink()
    {
		if (GameObject.Find("Mudaship").transform.childCount > 0) {
			GameObject.Find ("BossCoque").GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f);
			GameObject.Find ("BossCabine").GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f);
			foreach (SpriteRenderer spr in GameObject.Find("weakpoints").GetComponentsInChildren<SpriteRenderer>()) {
				spr.color = new Color (1.0f, 1.0f, 1.0f);
			}
		}
    }

    bool WeakPointPlayerShmupValidation(){
		return name.Contains("weakpoint");
	}


    

}
