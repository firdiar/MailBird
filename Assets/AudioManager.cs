using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip adTrue;
	public AudioClip adFalse;
	public AudioClip[] sound;
	AudioSource aus;

	void Start(){
		aus = GetComponent<AudioSource> ();
	}
	public void PlayTrue(){
		aus.clip = adTrue;
		aus.Play ();
		
	}
	public void PlayFalse(){
		aus.clip = adFalse;
		aus.Play ();


	}
	public bool isPlayingSame(int i){
		Debug.Log ("Nyala");
		return aus.clip == sound [i];
	
	}

	public void PlaySound(int indek){
		aus.clip = sound[indek];
		aus.Play ();
	}
}
