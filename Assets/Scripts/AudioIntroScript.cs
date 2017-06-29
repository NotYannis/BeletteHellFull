using UnityEngine;
using System.Collections;

public class AudioIntroScript : MonoBehaviour {
	
	public bool muteAllSounds;
	// Use this for initialization
	void Start () {
	//	if (muteAllSounds)
		//	AudioListener.volume = 0;
		//GetComponents<AudioSource>()[0].Play();//Player Laser init
		//StartCoroutine(PlayLaserLoopSound());
	}
	IEnumerator PlayLaserLoopSound(){
		yield return new WaitForSeconds(GetComponents<AudioSource>()[0].clip.length);
		GetComponents<AudioSource>()[1].Play();
		Debug.Log("PlayingMusicLoop");
	}
	// Update is called once per frame
	void Update () {
	
	}
}
