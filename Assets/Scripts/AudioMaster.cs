using UnityEngine;
using System.Collections;

public class AudioMaster : MonoBehaviour {

	uint bankID;
	// Use this for initialization
	public bool loadBankByScript;
	public void Start () {
        if (loadBankByScript)
        {
			AkSoundEngine.LoadBank ("BeletteHell_Soundbank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayEvent(string eventName){
		AkSoundEngine.PostEvent (eventName, gameObject);
	}
	public void CallEvent(string eventName){
		AkSoundEngine.PostEvent (eventName, gameObject);
	}
	public void StopEvent(string eventName, int fadeout){
		uint eventID;
		eventID = AkSoundEngine.GetIDFromString (eventName);
		AkSoundEngine.ExecuteActionOnEvent (eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	}

	public void PauseEvent(string eventName, int fadeout){
		uint eventID;
		eventID = AkSoundEngine.GetIDFromString (eventName);
		AkSoundEngine.ExecuteActionOnEvent (eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	}

	public void ResumeEvent(string eventName, int fadeout){
		uint eventID;
		eventID = AkSoundEngine.GetIDFromString (eventName);
		AkSoundEngine.ExecuteActionOnEvent (eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
	}
}
