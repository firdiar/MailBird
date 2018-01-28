using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	[SerializeField]GameObject mapChild;

	public void SetActive(){
		if(!mapChild.activeInHierarchy)
		mapChild.SetActive (true);
	}
}
