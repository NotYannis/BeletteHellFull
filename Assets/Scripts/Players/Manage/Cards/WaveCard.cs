using UnityEngine;
using System.Collections;

public enum ENEMY_TYPE
{
    NORMAL,
    ROQUETTE,
}

public class WaveCard : Card {

    int numberOfEnemy;
    public GameObject waveToSend;
    public ENEMY_TYPE enemyType;

    public WaveCard(int _manaCost, int _numberOfEnemy, ENEMY_TYPE _enemyType, GameObject _waveToSend, EditableVariables _vars)
    {
        vars = _vars;

        backCardImage = vars.jgCardWaveImage;

        switch (enemyType)
        {
            case ENEMY_TYPE.NORMAL:
                effectImage = vars.jgCardFoxNormalImage;
                break;
            case ENEMY_TYPE.ROQUETTE:
                effectImage = vars.jgCardFoxRoquetteImage;
                break;
        }

        manaCost = _manaCost;
        numberOfEnemy = _numberOfEnemy;
        enemyType = _enemyType;
        waveToSend = _waveToSend;
    }

    //Launch the wave of foxes
    public override void MakeEffect(SendZoneManager sendZone){
        sendZone.LaunchEnemyWave(this);
    }

    public override void MakeFeedback(SendZoneManager sendZone)
    {
        sendZone.MakeWaveFeedback(this);
    }
}
