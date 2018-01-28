using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class splashDes : MonoBehaviour {

	[SerializeField]float scaleTime;
	
	void Awake(){
		GetComponent<RawImage> ().CrossFadeAlpha (0, scaleTime, false);
		Destroy (this.gameObject, scaleTime);
	
	}
}
