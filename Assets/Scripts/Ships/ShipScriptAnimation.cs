using UnityEngine;
using System.Collections;

public class ShipScriptAnimation : MonoBehaviour {
    private Vector3 originalSpeed;

    [System.NonSerialized]
    public bool isSpawning;
	public bool isResistance;
    public bool isSpeed;
    [System.NonSerialized]
    public bool isDead;
	// Use this for initialization
	void Start () {
		if(isResistance) {
			GetComponent<Animator>().SetBool("isHeavy", true);
		}
		if(isSpeed) {
			GetComponent<Animator>().SetBool("isSpeed", true);
        }
        isSpawning = true;
        originalSpeed = gameObject.GetComponent<MoveScript>().velocity;
        gameObject.GetComponent<MoveScript>().velocity = Vector3.zero;
        gameObject.GetComponent<HealthScript>().destructible = false;
    }

    // Update is called once per frame
    void Update () {
        if (isSpawning)
        {
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SpawnAnimation"))
            {
                gameObject.GetComponent<MoveScript>().velocity = originalSpeed;
                gameObject.GetComponent<HealthScript>().destructible = true;
                isSpawning = false;
            }
        }
    }

    public void LateUpdate() {
        if (isDead)
        {
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                Destroy(gameObject);
            }
        }
    }

    public void deathTtrigger()
    {
        GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MoveScript>().velocity = Vector3.zero;
        isDead = true;

		GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("all_weapons_rocket_explosion");
    }
}
