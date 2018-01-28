using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maksSpeed;
	public GameObject beats;
	public LayerMask tomap;
	[SerializeField]GameObject cloud;
	Rigidbody2D playerRB;
	Animator anim;
	AudioSource ad;

	bool isFancingRight;

	public GameObject generator{ get; set; }


	// Use this for initialization
	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();
		isFancingRight = true;
		anim = GetComponent<Animator> ();
		ad = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log( Mathf.Lerp (10, 100, assd));
		PlayerMove ();
		anim.SetFloat ("speed", playerRB.velocity.magnitude);




	}
	void PlayerMove(){
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		if (moveX < 0) {
			if (isFancingRight)
				Flip ();

			isFancingRight = false;
		} else if(moveX >0) {
			if (!isFancingRight)
				Flip ();
			isFancingRight = true;
		}
//		Debug.Log (moveX + "   " + moveY);
		playerRB.velocity = new Vector2 (moveX *maksSpeed, moveY*maksSpeed)*Time.deltaTime * 100;


	}
	public void SpawnCloud(){
		GameObject temp = Instantiate (cloud, this.transform.position + new Vector3( 0 , -1.4f , 0), Quaternion.identity);
		Destroy (temp, 1);
		ad.Play ();
	
	}

	void Flip(){
		Vector3 sc = transform.localScale;
		sc = new Vector3 (sc.x * -1, sc.y, sc.z);
		transform.localScale = sc;

	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "Generator") {
			generator = col.gameObject;
			beats.SetActive (true);
		}

//		if(col.gameObject.tag == "Wall"){
//			if(col.GetComponent<WallScript>() != null)
//				col.GetComponent<WallScript> ().SetActive ();
//		}

	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Generator") {
			generator = null;
			beats.SetActive (false);
		}

	}
	
	
	

}
