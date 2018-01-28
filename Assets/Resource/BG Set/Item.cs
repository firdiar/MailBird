using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	protected GameObject Battery = null;
	protected BatteryScript batteryscript;
	// Use this for initialization
	void Start () {
		Battery = GameObject.Find ("Battery");
		batteryscript = Battery.GetComponent<BatteryScript> ();
		
	}
	

}
