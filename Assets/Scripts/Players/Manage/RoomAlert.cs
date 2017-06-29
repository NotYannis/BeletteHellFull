using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomAlert : MonoBehaviour {

    private Image roomAlert;
    private Animator[] workingFoxes = new Animator[2];
    private Animator[] lamps = new Animator[3];
    private bool alarm;

    private float alertSpeed = -0.03f;

    public float shakeAmt = 0;
    private bool shake = false;

    private GameObject room;
    private Vector3 roomPosition;




    // Use this for initialization
    void Start () {
        roomAlert = GameObject.Find("Alert").GetComponent<Image>();
        workingFoxes[0] = GameObject.Find("Console").GetComponent<Animator>();
        workingFoxes[1] = GameObject.Find("BoilerRoom").GetComponent<Animator>();
        room = GameObject.Find("Rooms");
        lamps = GameObject.Find("Lamps").GetComponentsInChildren<Animator>();
        roomPosition = room.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (alarm)
        {
            if(roomAlert.color.a > 0.0f && roomAlert.color.a < 0.5f)
            {
                roomAlert.color = new Color(roomAlert.color.r, roomAlert.color.g, roomAlert.color.b, roomAlert.color.a + alertSpeed);
            }
            else
            {
                alertSpeed *= -1;
                roomAlert.color = new Color(roomAlert.color.r, roomAlert.color.g, roomAlert.color.b, roomAlert.color.a + alertSpeed);
            }
        }

        if (shake)
        {
            RoomShake();
        }
    }

    public void StartPanic()
    {
        alarm = true;
        workingFoxes[0].speed = 2.0f;
        workingFoxes[1].speed = 2.0f;
        foreach(Animator anim in lamps)
        {
            anim.speed = 2.0f;
        }
    }

    public void StopPanic()
    {
        alarm = false;
        roomAlert.color = new Color(roomAlert.color.r, roomAlert.color.g, roomAlert.color.b, 0.0f);
        alertSpeed = -0.03f;

        workingFoxes[0].speed = 1.0f;
        workingFoxes[1].speed = 1.0f;

        foreach (Animator anim in lamps)
        {
            anim.speed = 1.0f;
        }
    }

    void RoomShake()
    {
        if (shakeAmt > 0)
        {

            float quakeYAmt = Random.value * shakeAmt * 2 - shakeAmt;
            float quakeXAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = roomPosition;
            pp.y += quakeXAmt;
            pp.x += quakeYAmt;
            room.transform.position = pp;
        }
    }

    public void StartShaking()
    {
        shake = true;
        Invoke("StopShaking", 0.2f);
    }

    public void StopShaking()
    {
        shake = false;
        room.transform.position = roomPosition;
    }
}
