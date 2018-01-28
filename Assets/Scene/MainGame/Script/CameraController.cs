using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float damping = 1;
	public float offsetCamY = 5;
	public float offsetCamX = 5;
	public float minusInRight = 0;

	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	float lebarX;
	float tinggiY;
	MazeGeneraorScript MGS;

	// Use this for initialization
	void Start () {

		m_LastTargetPosition = target.position;
		transform.parent = null;
		MGS = GameObject.Find ("MazeGenerator").GetComponent<MazeGeneraorScript> ();
		lebarX = MGS.GetLebarMap () - offsetCamX;
		tinggiY = MGS.GetTinggiMap ()-offsetCamY;
		//Camera.SetupCurrent(this.GetComponent<Camera> ());
		//Debug.Log (lebarX + "   " + tinggiY);
		
	}

	
	// Update is called once per frame
	void Update () {
		if (transform.position != m_LastTargetPosition) {
			Vector3 pos = Vector3.SmoothDamp (transform.position, target.position + new Vector3( 0 , 0 , -10), ref m_CurrentVelocity, damping);
			pos = new Vector3 (  Mathf.Clamp(pos.x , MGS.startPos.x +  offsetCamX , MGS.startPos.x + lebarX - minusInRight ), Mathf.Clamp(pos.y , MGS.startPos.y +  offsetCamY , MGS.startPos.y +  tinggiY ), pos.z);
			transform.position = pos;
		}

		m_LastTargetPosition = target.position;
		
	}
}
