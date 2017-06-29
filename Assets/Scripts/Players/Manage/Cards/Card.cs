using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card {
    protected EditableVariables vars;

    public int manaCost;
    public Sprite backCardImage;
    public Sprite effectImage;

    //Ths is the main function of every cards. Return true if the card as an effect
    public virtual void MakeEffect(SendZoneManager sendZone) { }

    public virtual void MakeFeedback(SendZoneManager sendZone) { }

}
