using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEfek : MonoBehaviour {

	[SerializeField]Vector2 midPoint;
	[SerializeField]float sizeMinCam;
	[SerializeField]float sizeMaksCam;
	[SerializeField]float timeDelayMap;
	public MonoBehaviour PlayerControll;
	public BatteryScript BS;

	Vector3 currentVelocity;
	bool isAnimating = false;
	bool dampActive = true;
	bool curutin = true;
	float i;

	void Awake(){
		i = sizeMinCam;
		GetComponent<CameraController> ().enabled = false;
		PlayerControll.enabled = false;
		BS.enabled = false;
		GameObject temp = GameObject.Find ("Audio1-5");
		if (temp != null) {
			Destroy (temp);
		}
	}
	public void StartAnim(){
		isAnimating = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (dampActive) {
			Vector3 pos = Vector3.SmoothDamp (transform.position, new Vector3 (midPoint.x, midPoint.y, -10), ref currentVelocity, 0.15f);
			transform.position = pos;
		}

		if (isAnimating) {
			GetComponent<Camera> ().orthographicSize = Mathf.Clamp (i = i + 0.5f, sizeMinCam, sizeMaksCam);
			//Debug.Log (GetComponent<Camera> ().orthographicSize > sizeMaksCam - 2);
			if (GetComponent<Camera> ().orthographicSize > sizeMaksCam-2 && curutin) {
				curutin = false;
				StartCoroutine (WaitingTurnBack (timeDelayMap));
			}

		} else if ((GetComponent<Camera> ().orthographicSize > sizeMaksCam-2 || !dampActive) && !isAnimating) {
//			Debug.Log ("masuk");
			GetComponent<Camera> ().orthographicSize = Mathf.Clamp (i = i - 1, sizeMinCam, sizeMaksCam);
			dampActive = false;
			if (GetComponent<Camera> ().orthographicSize < sizeMaksCam - 10) {
				
				GetComponent<CameraController> ().enabled = true;
				PlayerControll.enabled = true;
				BS.enabled = true;
			}
			if (GetComponent<Camera> ().orthographicSize < sizeMinCam+2) {
				//this.enabled = false;
			}
		
		} 


	}
		

	IEnumerator WaitingTurnBack(float time){
		yield return new WaitForSeconds (time);
		isAnimating = false;
	}
}
