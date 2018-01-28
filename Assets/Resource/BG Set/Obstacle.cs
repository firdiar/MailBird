using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Item {

	AudioManager am;



	void Awake(){
		StartCoroutine (SetAlpha (-30));
		am = GameObject.Find ("AudioEfek").GetComponent<AudioManager> ();

		}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			am.PlaySound (1);
			batteryscript.GetDamage (Random.Range (1, 3), false);
			StartCoroutine (SetAlpha (100));
			batteryscript.SetColor (Color.red, 3);

		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			Debug.Log ("Keluar Hit");
			Destroy (this.gameObject, 3);
		}
	}

	IEnumerator SetAlpha(float plus){
		int poin;
		if (plus > 0) {
			poin = 255;
		} else {
			poin = 0;
		}
		float i = 50;
		while(i != poin){
			
			Color A = GetComponent<SpriteRenderer> ().color;
			i = A.a;
			Debug.Log(i);
			i = Mathf.Clamp ( i + plus , 0, 255);
			A.a = i;
			Debug.Log(A.a);
			GetComponent<SpriteRenderer> ().color = A;
			yield return new WaitForSeconds(0.1f);
		} 
	
	}
}
