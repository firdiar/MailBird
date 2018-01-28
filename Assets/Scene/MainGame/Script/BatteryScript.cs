using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

	[SerializeField]AudioManager audioManagerScene;
	[SerializeField]float minusPercentagePerFrame = 0.1f;
	[SerializeField]float startPercentageBattery = 60;
	[Header("Poin")]
	[SerializeField]float Perfect = 2;
	[SerializeField]float Good = 1.5f;
	[SerializeField]float Great = 1;
	[SerializeField]float Common = 0.5f;
	[SerializeField]float Bad = 0.3f;
	[SerializeField]float Miss = 1;
	[SerializeField]float Boo = 1; //unituk salah ketik
	[SerializeField]GameObject[] _font = new GameObject[7];
	public GameObject[] Font{
		get { 
			return _font;
		}
	}

	[SerializeField]GameObject UIWinLose;
	RectTransform rectThisTransform;
	RectTransform percentageBattery;
	public bool isEnd{ get; private set;}
	float batteryHP = 1;
	float MAKS_BATTERY;
	float MIN_BATTERY;
	// Use this for initialization
	void Start () {
		isEnd = false;
		rectThisTransform = GetComponent<RectTransform> ();
		percentageBattery = transform.GetChild (0).GetComponent<RectTransform> ();
		MAKS_BATTERY = 0;
		MIN_BATTERY = -rectThisTransform.sizeDelta.y;
		batteryHP = Mathf.InverseLerp (0, 100, startPercentageBattery);

	}

	public float GetPercentage(){
		return batteryHP * 100;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log (Time.deltaTime);
//		Debug.Log (batteryHP);
		if (isEnd == false) {
			//Debug.Log ("masuk sini");
			Vector2 pos = percentageBattery.localPosition;
			pos = new Vector2 (pos.x, Mathf.Lerp (MIN_BATTERY, MAKS_BATTERY, batteryHP));
			//Debug.Log (batteryHP);
			percentageBattery.localPosition = pos;
			if (batteryHP == 0) {
				Debug.Log ("Lose");
				isEnd = true;
				UIWinLose.GetComponent<UIWinLose> ().IsWin = false;
				UIWinLose.SetActive (true);

			} else if (batteryHP == 1) {
				GameObject[] ListGenerator = GameObject.FindGameObjectsWithTag ("Generator");
				isEnd = true;
				if (ListGenerator.Length > 1) {
					StartCoroutine (StartAgain ());
				} else {
					UIWinLose.GetComponent<UIWinLose> ().IsWin = true;
					UIWinLose.SetActive (true);
				}
			}
			GetDamage (minusPercentagePerFrame, false);
		}

		if(batteryHP >0.3){
			if(!audioManagerScene.isPlayingSame(0)){
				audioManagerScene.PlaySound(0);
			}
		}else{
			if(!audioManagerScene.isPlayingSame(1)){
				audioManagerScene.PlaySound(1);
			}
					
		}


		
	}

	IEnumerator StartAgain(){
		yield return new WaitForSeconds (0.5f);
		isEnd = false;
	}

	/// <summary>
	/// GetDamage Based On Miss
	/// </summary>
	public void GetDamage( float bonusDamage , bool WithMiss){
		float poin = 0;
		if (WithMiss)
			poin = Miss;

		float pureDamage = (poin+bonusDamage)/100.0f;
		batteryHP = Mathf.Clamp( batteryHP - pureDamage , 0 , 1 );
	}

	/// <summary>
	/// Persentase berkurangnya baterai
	/// </summary>
	/// <param name="damage">Damage yang diterima baterai 1 - 100</param>
	public void GetDamage(float damage , float bonusDamage){
		float pureDamage = (damage + bonusDamage)/100.0f;
		batteryHP = Mathf.Clamp( batteryHP - pureDamage , 0 , 1 );
	}

	public void GetDamage(float damage , float bonusDamage , bool addBoo){
		float salah = 0;
		if (addBoo){
			salah = Boo;
		}
		float pureDamage = (damage+bonusDamage+salah)/100.0f;
		batteryHP = Mathf.Clamp( batteryHP - pureDamage , 0 , 1 );
	}

	/// <summary>
	/// menambahkan persentase baterai berdasar nilai key (1(perfect)-5(bad))
	/// </summary>
	/// <param name="value">Value (1(perfect)-5(bad))</param>
	/// <param name="Bonus">Bonus Persentase baterai</param>
	public void GetPoint(int value , float Bonus){
		switch (value) {
		case 1:
			GetPoint (Perfect + Bonus);
			break;

		case 2:
			GetPoint (Good + Bonus);
			break;

		case 3:
			GetPoint (Great + Bonus);
			break;

		case 4:
			GetPoint (Common + Bonus);
			break;

		case 5:
			GetPoint (Bad + Bonus);
			break;



		}
	
	}

	/// <summary>
	/// Menambarkan persen Batterai berdasarkan persen(1-100)
	/// </summary>
	/// <param name="pointAddPercentage">menambahkan persentase baterai (1-100)</param>
	public void GetPoint(float pointAddPercentage){
		float purePoint = pointAddPercentage/100.0f;
		batteryHP = Mathf.Clamp(batteryHP + purePoint , 0 , 1);
	}

	public void SetColor(Color c , int loop){
		SetColor (c);
		Invoke ("ReturnColor" , 2);

	}
	void ReturnColor(){
		GetComponent<UnityEngine.UI.RawImage> ().color = Color.white;
	}
	void SetColor(Color c){
		GetComponent<UnityEngine.UI.RawImage> ().color = c;
	}


}
