using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
*
* The deck manage card creation, utilisation, and destruction
*
*/

public class DeckScript : MonoBehaviour {
    private int cardSelected = -1;

    public GameObject[] deck;

    [System.NonSerialized]
	public GameObject[] hand = new GameObject[4];
    private ManagerRessourcesScript ressources;


	// Use this for initialization
	void Start () {
        ressources = GameObject.Find("PlayerGestionUI/ManagementScripts").GetComponent<ManagerRessourcesScript>();

        hand[0] = GameObject.Find("Cards/Card0");
        hand[1] = GameObject.Find("Cards/Card1");
        hand[2] = GameObject.Find("Cards/Card2");
        hand[3] = GameObject.Find("Cards/Card3");
        InitiateHand();
    }

	void InitiateHand(){
		int i;
		for(i = 0; i < hand.Length; ++i){
            CreateCard(i);
        }
    }

    //Draw a random card from the deck and init it on the hand
    private void CreateCard(int card)
    {
        GameObject newCard = deck[Random.Range(0, deck.Length)];

        Vector3 cardPos = hand[card].GetComponent<CardScript>().position;
        Destroy(hand[card]);
        hand[card] = Instantiate(newCard, cardPos, Quaternion.identity, gameObject.transform) as GameObject;

        //Connect the button to the function
        hand[card].GetComponent<CardScript>().button.onClick.AddListener(() => { GetCardIndex(card); });

        //If the card changed is the top card, make it transparent and inactive
        if (card == 3)
        {
            foreach(Image img in hand[card].GetComponentsInChildren<Image>())
            {
                img.color = new Color(255, 255, 255, 0.5f);
            }
            hand[card].GetComponent<CardScript>().button.interactable = false;
        }
    }

    //Called by the onclick function of the sendzones, trigger the card effect
	public void UseCard(SendZoneManager sendZone){
        Card currentCard = null;

        //Get the selected card
        if (cardSelected != -1)
        {
            currentCard = hand[cardSelected].GetComponent<CardScript>().card;
        }

        //Trigger the effect, feedbacks and consume the mana
        if (currentCard != null && EnoughMana(cardSelected))
        {
            currentCard.MakeEffect(sendZone);
            ressources.ManaUpdate(-hand[cardSelected].GetComponent<CardScript>().cost);
            hand[cardSelected].GetComponent<CardScript>().cardIndex = cardSelected;
            hand[cardSelected].GetComponent<CardScript>().StartMove(hand[cardSelected].transform.position, sendZone.transform.position, sendZone);
        }

        cardSelected = -1;
    }

    //Create a card form the top one, and create a new one at this emplacement
    public void DrawCard(int card)
    {
        Vector3 cardPos = hand[card].GetComponent<CardScript>().position;
        Destroy(hand[card]);

        //Instantiatethe new card by taking the one on the top
        hand[card] = Instantiate(hand[3], cardPos, Quaternion.identity, gameObject.transform) as GameObject;
        hand[card].GetComponent<CardScript>().button.onClick.AddListener(() => { GetCardIndex(card); });
        hand[card].GetComponent<CardScript>().button.interactable = true;

        foreach (Image img in hand[card].GetComponentsInChildren<Image>())
        {
            img.color = new Color(255, 255, 255, 1.0f);
        }

        //Create a new card on the top
        CreateCard(3);
    }

    public void GetCardIndex(int card)
    {
        cardSelected = card;
    }

    public bool EnoughMana(int card)
    {
        return ressources.currentMana - hand[card].GetComponent<CardScript>().cost >= 0;
    }
}
