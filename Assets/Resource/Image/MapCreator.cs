using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log (col.tag);
		if(col.gameObject.tag == "Wall"){
			if(col.GetComponent<WallScript>() != null)
				col.GetComponent<WallScript> ().SetActive ();
		}

	}
}
