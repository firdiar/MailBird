using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

	[SerializeField]MazeGeneraorScript mzg;
	// Update is called once per frame
	void Update () {
		if (mzg.isDoneCreated) {

			Camera.main.GetComponent<CameraEfek>().StartAnim();
			gameObject.SetActive (false);
			
		}
	}
}
