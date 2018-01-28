using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatsController : MonoBehaviour {

	public RectTransform beatStatic;
	public RectTransform beatDump;
	public Text keyHit;
	public GameObject player;
	public float speedBeats;

	float fullTime;
	float time;
	float lateTime;
	public bool isBig = true;

	TurnGeneratorScript tb;
	[SerializeField]BatteryScript bs;

	Vector2 minSize;
	Vector2 maksSize;
	void Start(){
		maksSize = beatDump.sizeDelta;
	}

	void OnEnable(){
		Debug.Log ("aaa");
		minSize = beatStatic.sizeDelta;

		tb = player.GetComponent<PlayerController> ().generator.GetComponent<TurnGeneratorScript>();
		fullTime = tb.timeForTick;
		//back = 0;
	}
	
	// Update is called once per frame
	void Update () {

		time = tb.timeLeft;
		bool temp = bs.isEnd;

		if (temp) {
			Destroy (tb.gameObject);
			transform.parent.gameObject.SetActive (false);
		}

		if ( isBig ) {
//			Debug.Log (1);
			//Debug.Log (time + "  " + fullTime);
			float tempx = Mathf.Lerp (minSize.x, maksSize.x, time / fullTime);
			float tempy = Mathf.Lerp (minSize.y, maksSize.y, time / fullTime);

			beatDump.sizeDelta = new Vector2 (tempx, tempy);

		}else if (beatDump.sizeDelta == new Vector2( maksSize.x+30 , maksSize.y+30)){
			//Debug.Log (2);
			Vector2 size = beatDump.sizeDelta;
			beatDump.sizeDelta = new Vector2(Mathf.Clamp( size.x - speedBeats  ,  maksSize.x , size.x ) , Mathf.Clamp( size.y - speedBeats , maksSize.y  , size.y));
			if (beatDump.sizeDelta == new Vector2 (maksSize.x , maksSize.y )) {
				isBig = true;
			}
		} 

		else {
			//Debug.Log (3);
			isBig = false;
			Vector2 size = beatDump.sizeDelta;
			beatDump.sizeDelta = new Vector2(Mathf.Clamp( size.x + speedBeats , size.x , maksSize.x+30) , Mathf.Clamp( size.y + speedBeats , size.y , maksSize.y+30));

			lateTime = 5;
		}
	}
}
