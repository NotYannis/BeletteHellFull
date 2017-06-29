using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SendZoneFeedbacksHandler : MonoBehaviour {
    public static SendZoneFeedbacksHandler Instance;
    private EditableVariables vars;

    //How many time bonus feedbacks will stay
    public float feedbackBonusTime;
    public float feedbackEnemyTime;

    //Feedback images
    private GameObject sendingNormalWaveImage;
    private GameObject sendingSpeedWaveImage;
    private GameObject sendingResWaveImage;
    private GameObject sendingRoquetteWaveImage;
    private GameObject bonusWeakspotHealth;
    private GameObject bonusMudashipHealth;
    private Sprite vulnerableImage;
    private Sprite nextImage;
    private Sprite spellHealthImage;
    private Sprite spellSpeedImage;


    void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple instance of SendZoneFeedbacksHandler");
        }
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        vars = GameObject.Find("Scripts").GetComponent<EditableVariables>();

        //Load images from the variable script
        vulnerableImage = vars.jgSpawnVulnerableImage;
        nextImage = vars.jgSpawnNextImage;
        spellHealthImage = vars.jgSpawnSpellHealthImage;
        spellSpeedImage = vars.jgSpawnSpellSpeedImage;
        bonusWeakspotHealth = vars.jgBonusWeakspotHealthImage;
        bonusMudashipHealth = vars.jgBonusMudashipHealthImage;

        //These are the moving foxes on the sendzone
        sendingNormalWaveImage = vars.jgSendingFoxNormalImage;
        sendingSpeedWaveImage = vars.jgSendingFoxSpeedImage;
        sendingResWaveImage = vars.jgSendingFoxResistantImage;
        sendingRoquetteWaveImage = vars.jgSendingFoxRoquetteImage;
    }
	
    //Depending on the kind of wave, launch a moving fox on the sendzone
    public void MakeSendingWaveFeedback(SendZoneManager sendZone, SPAWN_SPELL_STATE spellState, ENEMY_TYPE enemy)
    {

        GameObject enemyFeedback = sendingNormalWaveImage;


        if (enemy == ENEMY_TYPE.ROQUETTE)
        {
            enemyFeedback = sendingRoquetteWaveImage;
        }
        else
        {
            switch(spellState){
                case SPAWN_SPELL_STATE.NONE:
                    enemyFeedback = sendingNormalWaveImage;
                    break;
                case SPAWN_SPELL_STATE.SPELL_HEALTH:
                    enemyFeedback = sendingResWaveImage;
                    break;
                case SPAWN_SPELL_STATE.SPELL_SPEED:
                    enemyFeedback = sendingSpeedWaveImage;
                    break;
            }
        }
        enemyFeedback = Instantiate(enemyFeedback, sendZone.transform.position, Quaternion.identity, sendZone.transform) as GameObject;
        //destroy it after some times
        Destroy(enemyFeedback, feedbackEnemyTime);
    }

    //Make a feedback image for a bonus, that stay feedbacktime seconds
    public void MakeBonusFeedback(BONUS_TYPE bonusType, SendZoneManager sendZone)
    {
        GameObject bonusFeedback = bonusWeakspotHealth;

        switch (bonusType)
        {
            case BONUS_TYPE.WEAKSPOT:
                bonusFeedback = bonusWeakspotHealth;
                break;
            case BONUS_TYPE.MUDASHIP:
                bonusFeedback = bonusMudashipHealth;
                break;
        }
        bonusFeedback = Instantiate(bonusFeedback, sendZone.transform.position, Quaternion.identity, sendZone.transform) as GameObject;

        Destroy(bonusFeedback, feedbackBonusTime);
    }

    public void ChangeSpellImage(SPAWN_SPELL_STATE newSpellState, SendZoneManager sendZone)
    {
        //Change the background image
        switch (newSpellState)
        {
            case SPAWN_SPELL_STATE.NONE:
                sendZone.currentSpellImage.color = new Color(255, 255, 255, 0);
                break;
            case SPAWN_SPELL_STATE.SPELL_HEALTH:
                sendZone.currentSpellImage.color = new Color(255, 255, 255, 255);
                sendZone.currentSpellImage.sprite = spellHealthImage;
                break;
            case SPAWN_SPELL_STATE.SPELL_SPEED:
                sendZone.currentSpellImage.color = new Color(255, 255, 255, 255);
                sendZone.currentSpellImage.sprite = spellSpeedImage;
                break;
        }
    }

    public void MakeVulnerableFeedback(bool setV, SendZoneManager sendZone){
        if(setV){
            sendZone.GetComponentsInChildren<Image>()[1].enabled = true;
            sendZone.GetComponentsInChildren<Image>()[1].sprite = vulnerableImage;

        }
        else{
            sendZone.GetComponentsInChildren<Image>()[1].enabled = false;
        }
    }

    public void MakeNextFeedback(bool setN, SendZoneManager sendZone)
    {
        if(setN){
            sendZone.GetComponentsInChildren<Image>()[1].enabled = true;
            sendZone.GetComponentsInChildren<Image>()[1].sprite = nextImage;
        }
        else{
            sendZone.GetComponentsInChildren<Image>()[1].enabled = false;
        }
    }
}


