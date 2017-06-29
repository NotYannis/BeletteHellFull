using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor;

public class WavePatternManagementScript : MonoBehaviour {

    public List<GameObject> foxes;

    //For foxes spawning
    public float popRate = 0.5f;
    private float popCooldown = 0.0f;
    private bool poping = true;

    //For bonus spawning
	public Vector3 lastFoxPosition;
    private int index = 0;
	public bool hasBonus = false;
	public int bonusProbability = 5;
	// Use this for initialization

    

	void Start () {
        foxes = new List<GameObject>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            if (child != null)
            {
                child.gameObject.SetActive(false);
                foxes.Add(child.gameObject);
            }
        }
        foxes.OrderBy(t => t.transform.position.x).ToList();

		//To test...
		int num = Random.Range(1,10);
		hasBonus = (num<bonusProbability) ? true : false;
         
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (poping)
        {
	        if(popCooldown <= 0.0f)
            {
                popCooldown = popRate;
                foxes[index].SetActive(true);
                if (index < foxes.Count - 1 && foxes[index + 1].transform.position.x == foxes[index].transform.position.x)
                {
                    ++index;
                    foxes[index].SetActive(true);
                }
                ++index;
            }
            else
            {
                popCooldown -= Time.deltaTime;
            }
            if(index == foxes.Count)
            {
                poping = false;
            }
        }
		if(!poping){
			if(transform.childCount == 1) {
				if(transform.GetChild(0)!=null)
				{
					Transform transformFox = transform.GetChild(0);
					if(transformFox){
						lastFoxPosition = transformFox.position;
					}
				}
			}
		}

        if (transform.childCount == 0)
        {
			if(hasBonus) CreateBonus();
            Destroy(gameObject);
        }
	}

	void CreateBonus() {
		if(lastFoxPosition!=Vector3.zero) {
			int bonusType = Random.Range(1,5);
            GameObject bonusInstance = Instantiate(Resources.Load("Prefabs/Shmup/Bonus", typeof(GameObject))) as GameObject;
			if(bonusInstance.GetComponentInChildren<BonusScript>())
				bonusInstance.GetComponentInChildren<BonusScript>().currentState = (BonusScript.BonusType)bonusType;
			bonusInstance.transform.position = lastFoxPosition;
			bonusInstance.transform.parent = transform.parent;
        }
    }
}
