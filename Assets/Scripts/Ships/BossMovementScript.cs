using UnityEngine;
using System.Collections;

public class BossMovementScript : MonoBehaviour {
    [Tooltip("Amplitude sur Y")]
    public float amplitude = 5;
    [Tooltip("Vitesse de la vague")]
    public int period = 200;

    private int frameCount = 1;


    void Start()
    {

    }

    void FixedUpdate()
    {
        //Use a sine wave for the pattern
        frameCount++;
        float y = amplitude * Mathf.Sin((Mathf.PI * 2) * frameCount / period);
        transform.position = new Vector3(transform.position.x, y - 0.5f, transform.position.z);
    }
}
