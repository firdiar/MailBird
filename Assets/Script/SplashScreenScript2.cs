using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript2 : MonoBehaviour {
	
	[SerializeField]bool _isFade = true;
	[SerializeField]float timeFade = 2;
	[SerializeField]float timeUnFade = 0.2f;
	private float timer;
	float lamaWaktuSplashScreen = 6f;
	public bool IsFade{
		get{ 
			return _isFade;
		}
		set{ 
			_isFade = value;
		}
	
	}
	bool isDone = true;
	// Use this for initialization
	void Start () {
		Splash ();
	}

	// Update is called once per frame
	void Update () {
		if (!isDone && IsFade) {
			isDone = true;
			this.GetComponent<RawImage> ().CrossFadeAlpha (0, timeFade, false);
		
		} else if(!isDone && !IsFade){
			isDone = true;
			this.GetComponent<RawImage> ().CrossFadeAlpha (1, timeUnFade, false);
		
		}
		timer += Time.deltaTime;
		Debug.Log (timer);
		if(timer > lamaWaktuSplashScreen){
			Application.LoadLevel("intro");
		}

	}

	public void Splash(){
		IsFade = true;
		isDone = false;
	}

	public void UnSplash(){
		IsFade = false;
		isDone = false;
		
	}
}
	
