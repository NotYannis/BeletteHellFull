using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum SPAWN_SPELL_STATE
{
    NONE,
    SPELL_HEALTH,
    SPELL_SPEED,
}

public class SendZoneManager : MonoBehaviour {
    private EditableVariables vars;
    private SendZoneFeedbacksHandler sendzoneFeedbacks;
    private ManagerRessourcesScript resources;

    public bool isVulnerable;
    public bool hasHealthBonus;

    //The position where the foxes spawns
    public Vector3 spawnPosition;
    public GameObject weakspotAttached;

    public Image currentSpellImage;

    SPAWN_SPELL_STATE spawnSpellState;

    void Start()
    {
        vars = GameObject.Find("Scripts").GetComponent<EditableVariables>();
        sendzoneFeedbacks = GameObject.Find("PlayerGestionUI/ManagementScripts").GetComponent<SendZoneFeedbacksHandler>();

        resources = GameObject.Find("PlayerGestionUI/ManagementScripts").GetComponent<ManagerRessourcesScript>();
        currentSpellImage = GetComponent<Image>();

        spawnSpellState = SPAWN_SPELL_STATE.NONE;
    }

    //change the spell state of the sendzone. Return true if the state has changed
    public void ChangeSpellState(SPAWN_SPELL_STATE newSpellState)
    {
        //Change the spell only if it's a new one
        if(spawnSpellState != newSpellState)
        {
            spawnSpellState = newSpellState;
        }
    }

    //Make the effect of the bonus card. Return true if the card has effect
    public void GetBonusCard(BONUS_TYPE bonusCard)
    {
        switch (bonusCard)
        {
            case BONUS_TYPE.MUDASHIP:
                HealthScript mudashipHealth = GameObject.Find("Mudaship").GetComponent<HealthScript>();

                mudashipHealth.hp += vars.jgMudashipHealthBonus;
                //Drop to maximum health if it's above
                if(mudashipHealth.hp > vars.jgMudashipHealth)
                {
                    mudashipHealth.hp = vars.jgMudashipHealth;
                }

                resources.UpdateShipHealthBar();
                break;
            case BONUS_TYPE.WEAKSPOT:
                //Add health to the weakspot
                weakspotAttached.GetComponent<HealthScript>().hp += vars.jgWeakspotHealthBonus;

                sendzoneFeedbacks.MakeBonusFeedback(bonusCard, this);

                break;
        }
    }
    
    //Launch the wave on the right spot and make some feedbacks
    public void LaunchEnemyWave(WaveCard wave)
    {
        GameObject waveCopy = Instantiate(wave.waveToSend, spawnPosition, Quaternion.identity, GameObject.Find("Foreground").transform) as GameObject;

        //Set health and speed from editablevariable script
        for (int i = 0; i < waveCopy.transform.childCount; ++i)
        {
            Transform enemy = waveCopy.transform.GetChild(i);
            switch (wave.enemyType)
            {
                case ENEMY_TYPE.NORMAL:
                    enemy.GetComponent<HealthScript>().hp = vars.foxHealth;
                    enemy.GetComponent<MoveScript>().velocity.x = vars.foxVelocity;
                    break;
                case ENEMY_TYPE.ROQUETTE:
                    enemy.GetComponent<HealthScript>().hp = vars.foxRocketHealth;
                    enemy.GetComponent<MoveScript>().velocity.x = vars.foxRoquetteVelocity;
                    break;
            }
        }

        //Depending of the spell, foxes in the wave will have different stats and sprites
        switch (spawnSpellState)
        {
            case SPAWN_SPELL_STATE.SPELL_HEALTH:
                for (int i = 0; i < waveCopy.transform.childCount; ++i)
                {
                    Transform enemy = waveCopy.transform.GetChild(i);
                    enemy.GetComponent<HealthScript>().hp += vars.jgFoxHealthBonus;
                    enemy.GetComponent<ShipScriptAnimation>().isResistance = true;
                }
                break;
            case SPAWN_SPELL_STATE.SPELL_SPEED:
                for (int i = 0; i < waveCopy.transform.childCount; ++i)
                {
                    Transform enemy = waveCopy.transform.GetChild(i);
                    enemy.GetComponent<MoveScript>().velocity.x += vars.jgFoxVelocityBonus;
                    enemy.GetComponent<ShipScriptAnimation>().isSpeed = true;
                }
                break;
        }

        //Make some sounds
        switch (wave.enemyType)
        {
            case ENEMY_TYPE.NORMAL:
                GameObject.Find("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("all_spawn");
                break;
            case ENEMY_TYPE.ROQUETTE:
                GameObject.Find("GamePlayScript").GetComponent<AudioMaster>().PlayEvent("jg_spawn_rocket");
                break;
        }
    }

    //Make the weakspot vulnerable
    public void SetVulnerable(bool setV){
        if(setV)
        {
            isVulnerable = true;

            weakspotAttached.GetComponent<HealthScript>().destructible = true;
            weakspotAttached.GetComponent<HealthScript>().hp = vars.jgWeakspotHealth;
            
            //Activate Target
            weakspotAttached.GetComponentInChildren<Animator>().enabled  = true;
            weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;

            Collider2D[] weakspotColliders = weakspotAttached.GetComponents<BoxCollider2D>();
            foreach(Collider2D wsc in weakspotColliders)
            {
                wsc.enabled = true;
            }
			weakspotAttached.GetComponent<WeakPointSFX> ().isCurrentWeakPoint = true;
        }
        else
        {
            isVulnerable = false;

            weakspotAttached.GetComponent<HealthScript>().destructible = false;
            weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
            
            //Deactivate Target
            weakspotAttached.GetComponentInChildren<Animator>().enabled = false;

            //Activate smoke
            weakspotAttached.GetComponentsInChildren<Animator>()[1].enabled = true;
            weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[2].enabled = true;

            //Activate skull
            weakspotAttached.GetComponentsInChildren<SpriteRenderer>()[3].enabled = true;

            Collider2D[] weakspotColliders = weakspotAttached.GetComponents<BoxCollider2D>();
			weakspotColliders [1].enabled = false;

			weakspotAttached.GetComponent<WeakPointSFX> ().isCurrentWeakPoint = false;
        }

        sendzoneFeedbacks.MakeVulnerableFeedback(setV, this);
    }

    //Make some feedback for the next vunerable weakspot
    public void SetNext(bool setN){
        sendzoneFeedbacks.MakeNextFeedback(setN, this);
    }


    public void MakeWaveFeedback(WaveCard wave)
    {
        sendzoneFeedbacks.MakeSendingWaveFeedback(this, spawnSpellState, wave.enemyType);
        ChangeSpellState(SPAWN_SPELL_STATE.NONE);
        sendzoneFeedbacks.ChangeSpellImage(spawnSpellState, this);

    }

    public void MakeBonusFeedback(BonusCard card)
    {
        sendzoneFeedbacks.MakeBonusFeedback(card.GetBonusType(), this);
    }

    public void MakeSpellFeedback()
    {
        sendzoneFeedbacks.ChangeSpellImage(spawnSpellState, this);
    }
}
