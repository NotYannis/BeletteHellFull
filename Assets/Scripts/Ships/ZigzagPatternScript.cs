using UnityEngine;
using System.Collections;

public class ZigzagPatternScript : MonoBehaviour {
    [Tooltip("Toutes les combien de secondes le vaisseau va changer de direction ?")]
    public float timeBounds = 1.0f;
    private float timeBoundsCooldown;

    private bool goUp;
    private bool goDown;


    private MoveScript moveScript;

    void Start()
    {
        moveScript = gameObject.GetComponent<MoveScript>();
        //Begin at the middle of cooldown to stay centered
        timeBoundsCooldown = timeBounds / 2;
    }

    void FixedUpdate()
    {
        if(timeBoundsCooldown > 0.0f)
        {
            timeBoundsCooldown -= Time.deltaTime;
        }
        //Each time the timer hit 0, reverse y direction
        if(timeBoundsCooldown <= 0.0f)
        {
            moveScript.direction.y *= -1;
            timeBoundsCooldown = timeBounds;
        }
    }
}
