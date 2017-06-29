using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeakspotScript : MonoBehaviour
{
    private EditableVariables vars;

    private List<SendZoneManager> sendZones;
    private List<SendZoneManager> disabledWeakspots;

    public float shootingWindowDuration;
    private float shootingWindowCooldown;

    public float skullPopDuration = 0.4f;
    private float skullPopCooldown;

    private int currentWeakspot = -1;
    private int nextWeakspot;
    public bool shootingWindow;

    private HealthScript currentWeakspotHp;

    private GameObject mudashipWeakPoints;
    private GameObject mudaship;
    private RoomAlert panicRoom;


    // Use this for initialization
    void Start()
    {
        vars = GameObject.Find("Scripts").GetComponent<EditableVariables>();

        SendZoneManager[] sendNones;

        sendNones = GameObject.Find("PlayerGestionUI/Ship").GetComponentsInChildren<SendZoneManager>();
        sendZones = sendNones.ToList<SendZoneManager>();

        disabledWeakspots = new List<SendZoneManager>();

        shootingWindowDuration = vars.jgWeakspotDuration;

        panicRoom = GameObject.Find("ManagementScripts").GetComponent<RoomAlert>();
        mudaship = GameObject.Find("Level/Foreground/Mudaship");

        GetWeakspot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentWeakspotHp.hp <= 1 && !shootingWindow)
        {
            GetWeakspot();
        }

        if (shootingWindow)
        {
            panicRoom.StartPanic();
            shootingWindowCooldown -= Time.deltaTime;

            if (shootingWindowCooldown <= 0)
            {
                panicRoom.StopPanic();
                shootingWindow = false;
                ResetWeakPoints();
                skullPopDuration = 0.4f;
                skullPopCooldown = 0.0f;
            }
        }
    }

    public void GetWeakspot()
    {
        //Last weakspot down case
        if (nextWeakspot == -1)
        {
            sendZones[0].SetVulnerable(false);
            disabledWeakspots.Add(sendZones[0]);

            foreach(SendZoneManager sz in disabledWeakspots)
            {
                sz.weakspotAttached.GetComponent<Collider2D>().enabled = false;
            }

            sendZones.RemoveAt(0);
            GameObject.Find("WeakSpotHealth5").GetComponent<Image>().enabled = false;

            shootingWindow = true;
			GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("jg_shield_down");
            shootingWindowCooldown = shootingWindowDuration;
            mudaship.GetComponent<Collider2D>().enabled = true;
        }

        if (sendZones.Count > 0)
        {
            //First activation
            if (currentWeakspot == -1)
            {
                nextWeakspot = Random.Range(0, sendZones.Count);
                currentWeakspot = Random.Range(0, sendZones.Count);
                while (nextWeakspot == currentWeakspot)
                {
                    currentWeakspot = Random.Range(0, sendZones.Count);
                }

                sendZones[nextWeakspot].SetNext(true);
                sendZones[currentWeakspot].SetVulnerable(true);
            }
            else
            {
                //Remove current weakspot
                sendZones[currentWeakspot].SetVulnerable(false);

                disabledWeakspots.Add(sendZones[currentWeakspot]);
                sendZones.RemoveAt(currentWeakspot);

                //Update shump healtbar
                GameObject.Find("WeakSpotHealth" + disabledWeakspots.Count).GetComponent<Image>().enabled = false;
                if (nextWeakspot >= currentWeakspot || nextWeakspot > sendZones.Count)
                {
                    nextWeakspot--;
                }

                currentWeakspot = nextWeakspot;

                //Make next weakspot vulnerable
                sendZones[nextWeakspot].SetNext(false);
                sendZones[nextWeakspot].SetVulnerable(true);

                //If it's not the last weakspot, choose the next one
                if (sendZones.Count > 1)
                {
                    nextWeakspot = Random.Range(0, sendZones.Count);
                    while (nextWeakspot == currentWeakspot)
                    {
                        nextWeakspot = Random.Range(0, sendZones.Count);
                    }
                    sendZones[nextWeakspot].SetNext(true);
                }
                else
                {
                    nextWeakspot = -1;
                }
				GameObject.Find ("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("jg_impact_destroyweakpoint");
            }

            currentWeakspotHp = sendZones[currentWeakspot].weakspotAttached.GetComponent<HealthScript>();
        }
    }

    private void ResetWeakPoints()
    {
        mudaship.GetComponent<Collider2D>().enabled = false;

        foreach (SendZoneManager sd in disabledWeakspots)
        {
            sendZones.Add(sd);
            sd.weakspotAttached.GetComponent<Collider2D>().enabled = true;
            sd.weakspotAttached.GetComponentsInChildren<Animator>()[1].enabled = false;
            sd.weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[2].enabled = false;
            sd.weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[3].enabled = false;
        }

        nextWeakspot = -2;
        currentWeakspot = -1;
        disabledWeakspots.Clear();

        foreach(Image img in GameObject.Find("WeakspotBar").GetComponentsInChildren<Image>())
        {
            img.enabled = true;
        }

        GetWeakspot();
    }
}
