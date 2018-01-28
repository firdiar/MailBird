using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWinLose : MonoBehaviour {

	public GameObject WinLose;
	public Button btnContinue;
	public Button btnRetry;
	public Button btnHome;
	public SplashScreenScript splashScreen;
	public Texture2D texture;

	[SerializeField]string nextScene;
	[SerializeField]string retry;
	[SerializeField]string home;

	public bool IsWin{get; set;}

	void OnEnable(){
		if (!IsWin) {
			btnContinue.interactable = false;
			transform.GetChild(1).GetComponent<RawImage> ().texture = texture;
		} else {
			btnContinue.interactable = true;
		}

	}

	public void NextScene(){
		splashScreen.UnSplash ();
		splashScreen.ChangeScene (nextScene);
	}
	public void Retry(){

		splashScreen.UnSplash ();
		splashScreen.ChangeScene (retry);
	}
	public void Home(){
		splashScreen.UnSplash ();
		splashScreen.ChangeScene (home);

	}
}
