using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    [Tooltip("Prefab du projectile")]
    public Transform shotPrefab;
    [Tooltip("Cadence de tir (en secondes)")]
    public float fireRate = 0.25f;

    private float fireCooldown;

    void Start () {
        fireCooldown = fireRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else if(shotPrefab.GetComponent<ShotScript>().isEnemy && fireCooldown < 0)
        {
            Attack(true);
            fireCooldown = fireRate;
        }
	}

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            fireCooldown = fireRate;
            Transform shotTransform = Instantiate(shotPrefab) as Transform;
            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if(shot != null)
            {
                shot.isEnemy = isEnemy;
            }

        }
    }

    public bool CanAttack
    {
        get
        {
            return fireCooldown <= 0.0f;
        }
    }
}
