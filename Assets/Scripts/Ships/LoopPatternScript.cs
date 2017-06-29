using UnityEngine;
using System.Collections;

public class LoopPatternScript : MonoBehaviour {
    [Tooltip("Vitesse de la boucle")]
    public int period = 400;
    [Tooltip("Vitesse du looping sur X")]
    public int amplitudeX;
    [Tooltip("Vitesse du looping sur Y")]
    public int amplitudeY;
    
    [Tooltip("Toutes les combien de secondes le vaisseaux fait un looping ?")]
    public float loopingRate = 2.0f;
    private float loopingCooldown = 0.0f;



    private int frameCount = 1;

    private Vector3 baseVelocity;
    private MoveScript moveScript;

    void Start()
    {
        moveScript = gameObject.GetComponent<MoveScript>();

        moveScript.direction.y = 1;
        loopingCooldown = loopingRate;

        //Set y velocity to 0 so ship don't move in diagonal
        moveScript.velocity.y = 0.0f;
        baseVelocity = moveScript.velocity;
    }

    void FixedUpdate()
    {
        //Use a cos and a sin wave to loop
        if (loopingCooldown <= 0.0f && frameCount < period)
        {
            frameCount++;
            moveScript.velocity.y = amplitudeY * Mathf.Sin((Mathf.PI * 2) * frameCount / period);
            moveScript.velocity.x = amplitudeX * Mathf.Cos((Mathf.PI * 2) * frameCount / period) + baseVelocity.x;
        }

        //End of the loop
        if(frameCount == period)
        {
            frameCount = 0;
            moveScript.velocity = baseVelocity;
            loopingCooldown = loopingRate;
        }

        if(loopingCooldown > 0.0f)
        {
            loopingCooldown -= Time.deltaTime;
        }
    }
}
