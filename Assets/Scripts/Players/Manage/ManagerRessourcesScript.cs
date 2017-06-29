using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ManagerRessourcesScript : MonoBehaviour {
    private EditableVariables vars;

	public int currentMana;
	private int maxMana;
	public float manaIncomeRate;

	private HealthScript health;
    private int lastHeartIndex;
    private int currentHealth;
    [System.NonSerialized]
    public int totalHp;
    public HealthScript mudashipHealth;

    private RectTransform shmupHealthBar;
    private RectTransform managementHealthBar;


    private List<Image> manaPoints;
    private List<Image> heartPoints;


    // Use this for initialization
    void Start () {
        vars = GameObject.Find("Scripts").GetComponent<EditableVariables>();

		//Get all the mana points on the bar
		manaPoints = new List<Image>();
		GameObject manaBar = GameObject.Find("PlayerGestionUI/Mana");
		for(int i = 0; i < manaBar.transform.childCount; ++i){
			Transform child = manaBar.transform.GetChild(i);
			Image s = child.GetComponent<Image>();
			if( s != null ){
				manaPoints.Add(s);
			}
		}
		manaPoints = manaPoints.OrderBy( t => t.transform.position.y).ToList();

		manaIncomeRate = vars.jgManaIncome;
        maxMana = manaPoints.Count;
		currentMana = maxMana;

        mudashipHealth = GameObject.Find("Level/Foreground/Mudaship").GetComponent<HealthScript>();
        mudashipHealth.hp = vars.jgMudashipHealth;
		totalHp = mudashipHealth.hp;
        shmupHealthBar = GameObject.Find("GUI/Canvas/PlayerGestionHP/ShipHP").GetComponent<RectTransform>();

        managementHealthBar = GameObject.Find("PlayerGestionUI/Rooms/ControlRoom/HealthScreen/HeartBar").GetComponent<RectTransform>();

        InvokeRepeating("IncrementMana", manaIncomeRate, manaIncomeRate);
    }

    public void IncrementMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += 1;
            manaPoints[currentMana - 1].enabled = true;
        }
    }

    public void ManaUpdate(int manaAdd)
    {
        int lastMana = currentMana;

        currentMana += manaAdd;

        for (int i = currentMana; i < lastMana; ++i)
        {
            manaPoints[i].enabled = false;
        }
    }

	public void UpdateShipHealthBar(){
        //Health bar in shmup screen
        if (mudashipHealth.hp > 0)
        {
            shmupHealthBar.localScale = new Vector3((float)mudashipHealth.hp / (float)totalHp, shmupHealthBar.localScale.y, shmupHealthBar.localScale.z);
            managementHealthBar.localScale = new Vector3(managementHealthBar.localScale.x, (float)mudashipHealth.hp / (float)totalHp, managementHealthBar.localScale.z);
        }
        else
        {
			shmupHealthBar.localScale = new Vector3(0.0f, shmupHealthBar.localScale.y, shmupHealthBar.localScale.z);
            managementHealthBar.localScale = new Vector3(managementHealthBar.localScale.y, 0.0f, managementHealthBar.localScale.z);
            GameObject.Find("Canvas").GetComponentInChildren<GameOverScript>().ActivateGameOver("Fox");
            GameObject.Find("ManagementGameOver").GetComponent<GameOverScript>().ActivateGameOver("Fox");
        }

    }
}
