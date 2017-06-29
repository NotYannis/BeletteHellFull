using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {
    [Tooltip("Dégâts du projectile")]
    public int damage = 1;
    [Tooltip("Méchant ou gentil ?")]
    public bool isEnemy;
    [Tooltip("Durée de vie")]
    public int lifeTime = 10;

	public bool isIndestructible = false;
    // Use this for initialization
    void Start () {
		if(lifeTime!=-1) Destroy(gameObject, lifeTime);
	}


}
