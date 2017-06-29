using UnityEngine;
using System.Collections;

public enum SPELL_TYPE
{
    SPEED,
    HEALTH,
}

public class SpellCard : Card {

    private SPELL_TYPE spellType;

    public SpellCard(int _manaCost, SPELL_TYPE _spellType, EditableVariables _vars)
    {
        vars = _vars;
        backCardImage = vars.jgCardSpellImage;

        //Get the illustration image for the card
        switch (spellType)
        {
            case SPELL_TYPE.SPEED:
                effectImage = vars.jgCardSpellSpeedImage;
                break;
            case SPELL_TYPE.HEALTH:
                effectImage = vars.jgCardSpeedHealthImage;
                break;
        }

        manaCost = _manaCost;
        spellType = _spellType;
    }

    //Change the state of the spawner. Return true if the state has changed.
    public override void MakeEffect(SendZoneManager sendZone)
    {
        switch (spellType)
        {
            case SPELL_TYPE.SPEED:
                sendZone.ChangeSpellState(SPAWN_SPELL_STATE.SPELL_SPEED);
                break;
            case SPELL_TYPE.HEALTH:
                sendZone.ChangeSpellState(SPAWN_SPELL_STATE.SPELL_HEALTH);
                break;
        }
    }

    public SPELL_TYPE GetSpellType()
    {
        return spellType;
    }

    public override void MakeFeedback(SendZoneManager sendZone)
    {
        sendZone.MakeSpellFeedback();
    }
}
