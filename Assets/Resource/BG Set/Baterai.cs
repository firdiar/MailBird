using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baterai : Item {

	AudioManager am;


	void Awake(){
		am = GameObject.Find ("AudioEfek").GetComponent<AudioManager> ();

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			batteryscript.GetPoint (Random.Range (5, 10));
			am.PlaySound (0);
			batteryscript.SetColor (Color.green, 3);

			Destroy (this.gameObject);

		}
	}
}
