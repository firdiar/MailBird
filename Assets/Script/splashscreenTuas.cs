using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashscreenTuas : MonoBehaviour {
	private float timer;
	float lamaWaktuSplashScreen = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > lamaWaktuSplashScreen){
			Application.LoadLevel("intro6");
		}
	}
}
