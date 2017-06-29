using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
*   This script is used to manage card creation at runtime
*
*
*/

public class CardScript : MonoBehaviour {
    public EditableVariables vars;
    private DeckScript deck;
    private SendZoneManager sendZoneAimed;

    public Card card;

    public int cost;
    public GameObject enemyWave;

    public bool isFoxroquette;

    public bool isSpell;
    public bool isBonus;

    public bool isResistance;
    public bool isSpeed;

    public bool isShipHealthBonus;
    public bool isShieldHealthBonus;

    public Button button;

    public bool move = false;
    private Vector3 moveFrom;
    private Vector3 moveTo;
    private GameObject objectToMove;
    public float step = 0.05f;

    public int cardIndex;
    public Vector3 position;

    // Use this for initialization
    void Awake () {
        vars = GameObject.Find("Scripts").GetComponent<EditableVariables>();
        deck = GameObject.Find("Cards").GetComponent<DeckScript>();

        position = transform.position;

        if (isSpell)
        {
            if (isResistance)
            {
                card = new SpellCard(cost, SPELL_TYPE.HEALTH, vars);
            }
            else if (isSpeed)
            {
                card = new SpellCard(cost, SPELL_TYPE.SPEED, vars);
            }
        }
        else if (isBonus)
        {
            if (isShipHealthBonus)
            {
                card = new BonusCard(cost, BONUS_TYPE.MUDASHIP, vars);
            }
            else if (isShieldHealthBonus)
            {
                card = new BonusCard(cost, BONUS_TYPE.WEAKSPOT, vars);
            }
        }
        else
        {
            if (isFoxroquette)
            {
                card = new WaveCard(cost, 0, ENEMY_TYPE.ROQUETTE, enemyWave, vars);
            }
            else
            {
                card = new WaveCard(cost, 0, ENEMY_TYPE.NORMAL, enemyWave, vars);
            }
        }
    }

    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate () {
        if (move)
        {
            transform.position = Vector3.MoveTowards(moveFrom, moveTo, step);
            moveFrom = transform.position;
            transform.localScale *= 0.9f;
            if (transform.position == moveTo)
            {
                card.MakeFeedback(sendZoneAimed);
                deck.DrawCard(cardIndex);
            }
        }
    }

    public void StartMove(Vector3 from, Vector3 to, SendZoneManager sendZone)
    {
        sendZoneAimed = sendZone;
        moveFrom = from;
        moveTo = to;
        move = true;
        step = Vector3.Distance(from, to) / 15;
    }
}
