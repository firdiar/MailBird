using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class komponendefault : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //print("posis x = "+transform.position.x);
       // transform.position = new Vector2(2f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * 0.1f);
	}
}
