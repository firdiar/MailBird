using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGeneratorScript : MonoBehaviour {


	public float timeForTick = 5;

	[SerializeField]GameObject Battery = null;
	BatteryScript batteryscript;
	GameObject beats;
	[SerializeField]float bonusForEachStreakHit = 1f;
	[SerializeField]float bonusForEachStreakMiss = 1f;



	float BonusPointStreak = 0;
	float BonusDamageStreak = 0;
	public float timeLeft;

	bool isInGenerator = false;
	bool generateKeyRandom = true;
	string key;

	void Awake(){
		
		timeLeft = timeForTick;
		Battery = GameObject.Find ("Battery");
		batteryscript = Battery.GetComponent<BatteryScript> ();
	}

	// Update is called once per frame
	void Update () {
		
		if (!batteryscript.isEnd) {
			KeyRandom ();

			if (generateKeyRandom) {
				generateKeyRandom = false;
				key = KeyRandom ();
				Debug.Log (key);

			}
			

			if (isInGenerator) {
				if (beats == null) {
					beats = GameObject.Find ("Beats");
				}
				beats.GetComponent<BeatsController> ().keyHit.text = key.ToUpper ();
				timeLeft = Mathf.Clamp (timeLeft - Time.deltaTime, 0, timeForTick);
				if (timeLeft > 0)
					PlayGameTick (key, timeLeft);
			}

			if (timeLeft == 0) {
				beats.GetComponent<AudioManager> ().PlayFalse ();
				Instantiate (batteryscript.Font [5], beats.transform.position + new Vector3 (Random.Range(-125 , 125),Random.Range(-125 , 125) , 0) , Quaternion.identity , beats.transform);
				//Destroy (temp, 0.5f);
				timeLeft = timeForTick;
				generateKeyRandom = true;
				batteryscript.GetDamage (BonusDamageStreak, true);
				BonusDamageStreak += bonusForEachStreakMiss;
				BonusPointStreak = 0;
			}
		}

		
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player")
		isInGenerator = true;

	}
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "Player") {
			isInGenerator = false;
			key = KeyRandom ();
		}
	}

	string KeyRandom(){
		int keyInt = Random.Range (0, 15) + 1;

		switch (keyInt) {
		case 1:return "t";

		case 2:return "y";
			
		case 3:return "u";

		case 4:return "i";

		case 5:return "o";

		case 6:return "p";

		case 7:return "g";
			
		case 8:return "h";

		case 9:return "j";

		case 10:return "k";

		case 11:return "l";

		case 12:return "v";

		case 13:return "b";

		case 14:return "n";

		case 15:return "m";
		}
		return "";
	
	}



	void PlayGameTick(string keyGuess , float currentLeftTime){
		
		string userInput = Input.inputString;


		//jika yang ditekan bukan key untuk bergerak
		if (Input.anyKeyDown && (userInput != "w" && userInput != "a" && userInput != "s" && userInput != "d") &&
			!(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || 
				Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))) 

			{
			transform.GetChild (0).GetComponent<AnimRollUI> ().RotateBackDelay (-800, 0.2f);
			if (beats == null) {
				beats = GameObject.Find ("Beats");
			}
			beats.GetComponent<BeatsController> ().isBig = false;

				
				int val = 0;
			if (currentLeftTime > timeForTick * 0.7) {
				val = 1;
			} else if (currentLeftTime > timeForTick * 0.6) {
				val = 2;
			} else if (currentLeftTime > timeForTick * 0.4) {
				val = 3;
			} else if (currentLeftTime > timeForTick * 0.1) {
				val = 4;
			} else if (currentLeftTime > timeForTick * 0) {
				val = 5;
			}
			 
				
				if (keyGuess == userInput) {
				Debug.Log (true);
				batteryscript.GetPoint (val, BonusPointStreak);
				BonusPointStreak = BonusPointStreak + bonusForEachStreakHit * (2.0f/val);
				timeLeft = timeForTick;
				beats.GetComponent<AudioManager> ().PlayTrue ();
				Instantiate (batteryscript.Font [val - 1], beats.transform.position + new Vector3 (Random.Range(-125 , 125),Random.Range(-125 , 125) , 0) , Quaternion.identity , beats.transform);
				//Destroy (temp, 0.5f);

				BonusDamageStreak = 0;
				} else {
				Debug.Log (false);
				timeLeft = timeForTick;
				beats.GetComponent<AudioManager> ().PlayFalse ();
				Instantiate (batteryscript.Font [6], beats.transform.position + new Vector3 (Random.Range(-125 , 125),Random.Range(-125 , 125) , 0) , Quaternion.identity , beats.transform);
				//Destroy (temp, 0.5f);
				batteryscript.GetDamage (0, BonusDamageStreak, true);
				BonusDamageStreak += bonusForEachStreakMiss;
				BonusPointStreak = 0;
				}
			generateKeyRandom = true;

		}




	}
}
