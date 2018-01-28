using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {

	[SerializeField]bool _isFade = true;
	[SerializeField]float timeFade = 2;
	[SerializeField]float timeUnFade = 0.2f;
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
			StartCoroutine (setAsBack (timeFade));
		} else if (!isDone && !IsFade) {
			isDone = true;
			this.GetComponent<RawImage> ().CrossFadeAlpha (1, timeUnFade, false);
			StartCoroutine (setAsBack (timeFade));
		
		} 

	}

	IEnumerator setAsBack(float time){
		yield return new WaitForSeconds (time);
		transform.SetAsFirstSibling ();


	}

	public void Splash(){
		IsFade = true;
		isDone = false;
		transform.SetAsLastSibling ();
	}

	public void UnSplash(){
		IsFade = false;
		isDone = false;
		transform.SetAsLastSibling ();
	}
	public void ChangeScene(string nameScene){

		Application.LoadLevel (nameScene);
	}
}
