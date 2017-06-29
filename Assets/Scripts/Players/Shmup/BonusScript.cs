using UnityEngine;
using System.Collections;

public class BonusScript : MonoBehaviour {

	public enum BonusType
	{
		None = 0,
		Laser = 1,
		Cone = 2,
		Rocket = 3,
		Shield = 4
	};
	public BonusType currentState = BonusType.None;//1:Laser, 2:Cone, 3:Rocket, 4:Shield
	public Animator animatorController;
	public GameObject bonusRender;

    // Update is called once per frame
    void Update () {
		if(animatorController == null) {
			animatorController = bonusRender.GetComponent<Animator>();
			if(animatorController)
			{
				animatorController.SetInteger("BonusType", (int)currentState);
			}
		}

	}
	void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.name == "PlayerShmup") {
            otherCollider.GetComponent<PlayerShmupController> ().ApplyBonusEffect (currentState);
			Destroy(transform.parent.gameObject);
		}
	}
}
