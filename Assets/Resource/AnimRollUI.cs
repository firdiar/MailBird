using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRollUI : MonoBehaviour {

	public float speed;
	float temp;

	// Update is called once per frame
	void Update () {

		transform.Rotate (0, 0, speed * Time.deltaTime);
		
	}

	public void RotateBackDelay(float speedUpdate , float time){

		temp = speed;
		speed = speedUpdate;
		StartCoroutine (Delay (time));


	}

	IEnumerator Delay(float time){
		yield return new WaitForSeconds (0.2f);
		speed = temp;

	}
}
