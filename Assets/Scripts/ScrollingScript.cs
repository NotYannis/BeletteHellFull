using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScrollingScript : MonoBehaviour {
    [Tooltip("Vitesse défilement")]
    public Vector2 velocity = new Vector2(2, 2);

    [Tooltip("Direction du défilement")]
    public Vector2 direction = new Vector2(-1, 0);

    [Tooltip("Le mouvement est-il lié à la caméra ?")]
    public bool isLinkedToCamera = false;

    [Tooltip("Est-ce qu'il faut boucler ?")]
    public bool isLooping = false;

    private List<SpriteRenderer> backgroundPart;
    private float cameraLeftBound;
    private float cameraRightBound;

    void Start()
    {
        //Initialize an array with all the element of the parent to loop
        if (isLooping)
        {
            backgroundPart = new List<SpriteRenderer>();

            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if(r != null)
                {
                    backgroundPart.Add(r);
                }
            }
            backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();
        }
    }

    void Update () {
        Vector3 movement = new Vector3(velocity.x * direction.x, velocity.y * direction.y, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            //Camera.main.transform.Translate(movement);
        }

        //Loop decor elements
        if (isLooping)
        {
            //Get the fiist element of the array
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if(firstChild != null)
            {
                cameraLeftBound = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
                cameraRightBound = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;

                //If outside of the camera viision, replace it at the beggining of the screen
                if ( firstChild.transform.position.x < cameraLeftBound - firstChild.bounds.size.x )
                {
                    //if (firstChild.isVisible == false)
                    {
                        float nextPosition = cameraRightBound + firstChild.bounds.size.x;

						firstChild.transform.position = new Vector3(-nextPosition * direction.x, firstChild.transform.position.y,
                            firstChild.transform.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
	}
}
