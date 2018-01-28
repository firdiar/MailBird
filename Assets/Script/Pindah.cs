using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pindah : MonoBehaviour {

	public string nextScene;


	// Update is called once per frame
	void Update () {
		
	}

	public void Next(){
		Application.LoadLevel (nextScene);
	}
}
