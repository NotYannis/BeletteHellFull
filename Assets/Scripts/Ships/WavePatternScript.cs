using UnityEngine;
using System.Collections;

public class WavePatternScript : MonoBehaviour {
    [Tooltip("Amplitude sur Y")]
    public int amplitude = 5;
    [Tooltip("Vitesse de la vague")]
    public int period = 200;

    private int frameCount = 1;

    private MoveScript moveScript;

	void Start () {
        moveScript = gameObject.GetComponent<MoveScript>();
        moveScript.direction.y = 1;
	}
	
	void FixedUpdate () {
        //Use a sine wave for the pattern
        frameCount++;
        moveScript.velocity.y = amplitude * Mathf.Sin((Mathf.PI * 2) * frameCount / period);
    }
}
