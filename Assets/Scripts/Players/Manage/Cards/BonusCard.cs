using UnityEngine;
using System.Collections;

public enum BONUS_TYPE
{ 
    WEAKSPOT,
    MUDASHIP,
}

public class BonusCard : Card {

    private BONUS_TYPE bonusType;

    public BonusCard(int _manaCost, BONUS_TYPE _bonusType, EditableVariables _vars)
    {
        vars = _vars;
        backCardImage = vars.jgCardBonusImage;

        //Get the illustration image for the card
        switch (bonusType)
        {
            case BONUS_TYPE.WEAKSPOT:
                effectImage = vars.jgCardSpeedHealthImage;
                break;
            case BONUS_TYPE.MUDASHIP:
                effectImage = vars.jgCardBonusHealthMudashipImage;
                break;
        }

        manaCost = _manaCost;
        bonusType = _bonusType;
    }

    public override void MakeEffect(SendZoneManager sendZone)
    {
        sendZone.GetBonusCard(bonusType);
    }

    public override void MakeFeedback(SendZoneManager sendZone)
    {
        sendZone.MakeBonusFeedback(this);
    }

    public BONUS_TYPE GetBonusType()
    {
        return bonusType;
    }

}
