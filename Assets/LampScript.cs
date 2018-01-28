using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour {

	[SerializeField]BatteryScript bs;
	[SerializeField]Texture2D[] txture;
	// Update is called once per frame
	void Update () {
		if (bs.GetPercentage () > 85) {
			transform.GetChild (0).GetComponent<UnityEngine.UI.RawImage> ().texture = txture[0];
			transform.GetChild (0).GetComponent<UnityEngine.UI.RawImage> ().uvRect = new Rect (0.04f , 0.19f , 0.93f , 0.74f);
		} else {
			transform.GetChild (0).GetComponent<UnityEngine.UI.RawImage> ().texture = txture[1];
			transform.GetChild (0).GetComponent<UnityEngine.UI.RawImage> ().uvRect = new Rect (0 , 0 , 1 , 1);
		}
		
	}
}
