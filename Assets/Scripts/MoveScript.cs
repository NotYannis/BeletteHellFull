using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

    [Tooltip("Vitesse de déplacement")]
    public Vector2 velocity = new Vector2(1, 1);
    [Tooltip("Direction")]
    public Vector2 direction = new Vector2(1, 0);
    public bool followPlayer;

    private Rigidbody2D rig;

    void Start () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        Vector2 movement = new Vector2();
        if (followPlayer)
        {
			Transform player = null;
			if(GameObject.Find("Level/Foreground/PlayerShmup")!=null)
            	player = GameObject.Find("Level/Foreground/PlayerShmup").GetComponent<Transform>();
            if(player != null){
                Vector2 direction = new Vector2(transform.position.x - player.position.x, transform.position.y - player.position.y);
                float angle = Mathf.Atan2(direction.y, direction.x);
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * angle);

                movement = -direction;
                movement.Normalize();
                movement *= velocity.x;

            }
        }
        else
        {
            movement = new Vector2(velocity.x * direction.x, velocity.y * direction.y);
        }

        rig.velocity = movement;

        if(transform.position.x < -25.0f || transform.position.x > 25.0f)
        {
            Destroy(gameObject);
        }
	}
}
