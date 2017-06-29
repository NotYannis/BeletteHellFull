using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D otherCollider)
    {
        foreach(Image img in GetComponentsInChildren<Image>())
        {
            img.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D otherCollider)
    {
        foreach (Image img in GetComponentsInChildren<Image>())
        {
            img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
