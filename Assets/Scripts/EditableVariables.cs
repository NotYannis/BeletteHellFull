using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


/*
* This file is for Game Designers. They can edit every variables in the 
* inspector with this script. Use it in every script to get variables.
* 
* Convention : Every variables relative to the Shmup Player begin with js
*              Every variables relative to the management player begin with jg
*/


public class EditableVariables : MonoBehaviour {

    public static EditableVariables instance;

    [Space(3)]
    [Header("Belette")]
    public int jsHealth;
    public float jsVelocity;
    public float jsInvicibility;

    [Space(3)]
    [Header("Belette Weapons")]
    public int jsBulletDamage;
    public float jsBulletVelocity;
    public float jsBulletFireRate;
    public int jsRocketDamage;
    public float jsRocketVelocity;
    public float jsRocketFireRate;
    public int jsRocketAmmo;
    public int jsConeDamage;
    public float jsConeVelocity;
    public float jsConeFireRate;
    public int jsConeAmmo;
    public float jsConeDirection;
    [Tooltip("The damage the laser will do each tick of damage (see below)")]
    public float jsLaserDamage;
    [Tooltip("How fast the laser tick (in milliseconds)")]
    public float jsLaserFireRate;
    public int jsShieldHealth;

    [Space(3)]
    [Header("Belette bonus")]
    public float jsLaserAmmo;


    [Space(3)]
    [Header("Fox")]
    public int jgMudashipHealth;
    public int jgWeakspotHealth;
    [Tooltip("How long is the mudaship vulnerable")]
    public float jgWeakspotDuration;
    public float jgManaIncome;

    [Space(3)]
    [Header("Fox enemies")]
    public int foxHealth;
    public float foxVelocity;
    public int foxRocketHealth;
    public float foxRoquetteVelocity;

    [Space(3)]
    [Header("Fox cards")]
    public int jgMudashipHealthBonus;
    public int jgWeakspotHealthBonus;
    public int jgFoxHealthBonus;
    public float jgFoxVelocityBonus;


    //These are jg's feedback images when using cards
    [System.NonSerialized]
    public GameObject jgSendingFoxRoquetteImage;
    [System.NonSerialized]
    public GameObject jgSendingFoxSpeedImage;
    [System.NonSerialized]
    public GameObject jgSendingFoxNormalImage;
    [System.NonSerialized]
    public GameObject jgSendingFoxResistantImage;
    [System.NonSerialized]
    public GameObject jgBonusWeakspotHealthImage;
    [System.NonSerialized]
    public GameObject jgBonusMudashipHealthImage;

    //These are images for the cards
    [System.NonSerialized]
    public Sprite jgCardSpellImage;
    [System.NonSerialized]
    public Sprite jgCardBonusImage;
    [System.NonSerialized]
    public Sprite jgCardWaveImage;
    [System.NonSerialized]
    public Sprite jgCardFoxNormalImage;
    [System.NonSerialized]
    public Sprite jgCardFoxRoquetteImage;
    [System.NonSerialized]
    public Sprite jgCardSpellSpeedImage;
    [System.NonSerialized]
    public Sprite jgCardSpeedHealthImage;
    [System.NonSerialized]
    public Sprite jgCardBonusHealthWeakspotImage;
    [System.NonSerialized]
    public Sprite jgCardBonusHealthMudashipImage;

    //Images for spawner
    [System.NonSerialized]
    public Sprite jgSpawnVulnerableImage;
    [System.NonSerialized]
    public Sprite jgSpawnNextImage;
    [System.NonSerialized]
    public Sprite jgSpawnBonusHealthWeakspotImage;
    [System.NonSerialized]
    public Sprite jgSpawnBonusHealthMudashipImage;
    [System.NonSerialized]
    public Sprite jgSpawnSpellSpeedImage;
    [System.NonSerialized]
    public Sprite jgSpawnSpellHealthImage;

    // Use this for initialization
    void Awake () {
	    if(instance != null)
        {
            Debug.Log("Error : multiple instance of EditableVariables");
        }
        instance = this;

        GetJgFeedbackImages();
        GetJgCardImages();
        GetJgSpawnImages();
    }

    private void GetJgFeedbackImages()
    {
        jgSendingFoxRoquetteImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/SendingFoxRoquetteImage");
        jgSendingFoxSpeedImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/SendingFoxSpeedImage");
        jgSendingFoxNormalImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/SendingFoxNormalImage");
        jgSendingFoxResistantImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/SendingFoxResistantImage");
        jgBonusWeakspotHealthImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/BonusHealthWeakspotImage");
        jgBonusMudashipHealthImage = Resources.Load<GameObject>("Prefabs/UI/Manage/Parts/BonusHealthMudashipImage");
    }

    private void GetJgCardImages()
    {
        jgCardSpellImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardSpellImage");
        jgCardBonusImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardBonusImage");
        jgCardWaveImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardWaveImage");
        jgCardFoxNormalImage = Resources.Load<Sprite>("Sprites/Enemies/FoxNormalImage");
        jgCardFoxRoquetteImage = Resources.Load<Sprite>("Sprites/Enemies/FoxRoquetteImage");
        jgCardSpellSpeedImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardSpellSpeedImage");
        jgCardSpeedHealthImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardSpellHealthImage");
        jgCardBonusHealthWeakspotImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardBonusHealthWeakspotImage");
        jgCardBonusHealthMudashipImage = Resources.Load<Sprite>("Sprites/UIManagement/Card/CardBonusHealthMudashipImage");
    }

    private void GetJgSpawnImages()
    {
        jgSpawnVulnerableImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnVulnerableImage");
        jgSpawnNextImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnNextImage");
        jgSpawnBonusHealthWeakspotImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnBonusHealthWeakspotImage");
        jgSpawnBonusHealthMudashipImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnBonusHealthMudashipImage");
        jgSpawnSpellSpeedImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnSpellSpeedImage");
        jgSpawnSpellHealthImage = Resources.Load<Sprite>("Sprites/UIManagement/Spawn/SpawnSpellHealthImage");
    }
}
