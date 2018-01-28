using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneraorScript : MonoBehaviour {

	public class WallCell
	{
		public bool isVisited = false;
		GameObject _wallDepan;
		GameObject _wallKanan;
		GameObject _wallBlakang;
		GameObject _wallKiri;
		public GameObject WallDepan{
			get{ return _wallDepan;}
			set{ _wallDepan = value; }
		}
		public GameObject WallBlakang{
			get{ return _wallBlakang;}
			set{ _wallBlakang = value; }
		}
		public GameObject WallKanan{
			get{ return _wallKanan;}
			set{ _wallKanan = value; }
		}
		public GameObject WallKiri{
			get{ return _wallKiri;}
			set{ _wallKiri = value; }
		}
		public Vector2 vector2Position;
		public Vector2 GenerateRandomPosition(){
			float jarak = Vector2.Distance (WallDepan.transform.position, WallBlakang.transform.position);
			jarak *= 0.5f;
			float posx = Random.Range (vector2Position.x - jarak / 1.8f, vector2Position.x + jarak / 2.0f);
			float posy = Random.Range (vector2Position.y - jarak / 2.0f, vector2Position.y + jarak / 1.8f);
			return new Vector2 (posx, posy);
		}

	}



	public bool createOnStart {
		private get;
		set;
	}

	public GameObject wall;
	public GameObject[] environmentPerCells;
	public int sizeTinggi;
	public int sizeLebar;
	public float panjangWall;
	public Vector3 startPos;
	[Range(0f , 1f)]
	[SerializeField]float delayMakeLabyrith = 0.001f;


	[Header("Create Turbin")]
	public float timeBeats;

	public Vector2[] minPos;

	public Vector2[] maxPos;


	public int countTurbin;
	public GameObject turbinPrefabs;

	GameObject MazeParent;
	[SerializeField]BatteryScript Battery;

	WallCell[,] arrWalls;
	GameObject tempWall;

	public bool isDoneCreated{ get; set;}


	public float GetLebarMap(){
		return sizeLebar * panjangWall;
	
	}
	public float GetTinggiMap(){
		return sizeTinggi * panjangWall;
	}

	void Update(){
		
		if (Battery.isEnd && (Battery.GetPercentage () == 100)) {
			
			arrWalls [sizeLebar - 1, sizeTinggi - 1].WallKanan.GetComponent<SpriteRenderer> ().color = Color.red; 

		}
		


	}

	// Use this for initialization
	void Awake () {
		
		isDoneCreated = false;
		arrWalls = new WallCell[sizeLebar, sizeTinggi];
		for (int i = 0; i < sizeTinggi; i++) {
			for (int j = 0; j < sizeLebar; j++) {
				//Debug.Log (i + " , " + j);
				arrWalls [j , i] = new WallCell ();
			
			}
		
		}
		StartCreateMaze ();

//		Debug.Log (arrWalls [1, 8].vector2Position);
//		Debug.Log (arrWalls [1, 8].WallDepan.transform.position);
//		Debug.Log (arrWalls [1, 8].WallKanan.transform.position);
//		Debug.Log (arrWalls [3,1].WallKiri.name);
	}


	//Membuat Maze
	public void StartCreateMaze(){
		MazeParent = new GameObject ();
		MazeParent.name = "MazeEnvironment";
		CreateBlankMaze ();

		StartCoroutine(CreateWalkThrough (0 , 0));
		MazeParent.transform.SetParent (GameObject.Find ("Environment").transform);

	
	}
	
	//Membuat maze awal
	void CreateBlankMaze(){
		Vector3 posAwal = startPos;


		//Membuat Wall Kanan / Kiri
		for (int i = 0; i <= sizeTinggi; i++) {
			for (int j = 0; j < sizeLebar; j++) {
				
				tempWall = (GameObject)Instantiate (wall , posAwal + new Vector3(  j*panjangWall + panjangWall/2 , i*panjangWall ,   0) , Quaternion.identity);
				tempWall.name = "Wall " + j.ToString() + " , " + i.ToString () + " Hor";
				if (i < sizeTinggi && j < sizeLebar && tempWall != null) {
					arrWalls [j, i].WallKiri = tempWall;
					if (i > 0) {
						arrWalls [j, i - 1].WallKanan = tempWall;
					}

				} else {
					arrWalls [j, i - 1].WallKanan = tempWall;
				
				}
				tempWall.transform.SetParent( MazeParent.transform);
			}
		}

		//Membuat Wall Depan / Blakang
		for (int i = 0; i < sizeTinggi; i++) {
			for (int j = 0; j <= sizeLebar; j++) {

				tempWall = (GameObject) Instantiate (wall , posAwal + new Vector3( j*panjangWall,i*panjangWall + panjangWall/2   , 0 ) , Quaternion.Euler(0 , 0 ,  90));
				tempWall.name = "Wall " + j.ToString() + " , " + i.ToString ()+" Ver";
				if (i < sizeTinggi && j < sizeLebar && tempWall != null) {
					arrWalls [j, i].WallDepan = tempWall;
					if (j > 0) {
						arrWalls [j - 1, i].WallBlakang = tempWall;
					}

				} else {
					arrWalls [j - 1, i].WallBlakang = tempWall;
				}
				tempWall.transform.SetParent( MazeParent.transform);
			}
		}
		int ke = 0;
		foreach (WallCell c in arrWalls) {
			float posx = c.WallKanan.transform.position.x;
			float posy = c.WallDepan.transform.position.y;

			c.vector2Position = new Vector2 (posx, posy);
			int i = Random.Range (0, 11);
			if(i <2){
				GameObject tmp = Instantiate (environmentPerCells[Random.Range(0 , environmentPerCells.Length)], c.GenerateRandomPosition(), Quaternion.identity, MazeParent.transform);
				
			}
			//tmp.name = "Path " + ke;
			ke++;
		}


	}

	/// <summary>
	/// Creates the walk throught for labiryth
	/// </summary>
	/// <param name="baris">baris saat ini</param>
	/// <param name="kolom">Kolom saat ini</param>
	IEnumerator CreateWalkThrough (int kolom = 0, int baris = 0){


		string walkThrough = GetNextPath (kolom , baris);
		List<Vector2> listStep = new List<Vector2> ();

		while (walkThrough != "Clear") {
			//Debug.Log (walkThrough);
			arrWalls [kolom, baris].isVisited = true;
			Vector2 nextStep = Vector2.zero;
			listStep.Clear ();
			string[] jalan = walkThrough.Split (',');


			foreach (string isi in jalan) {
				if (isi != " ") {
					string[] xy = isi.Split ('.');
					listStep.Add (new Vector2 (System.Convert.ToInt32( xy [0]), System.Convert.ToInt32( xy [1]) ));
				}
			}
			int count = listStep.Count;

			int random = Random.Range (0 , count);
			//Debug.Log (random);

			nextStep = listStep[random];
			//Debug.Log (listStep.Count+"  "+random +"   " + nextStep);
			if (nextStep.x != kolom) {
				if (nextStep.x > kolom) {
					Destroy(arrWalls[kolom , baris].WallBlakang);
				} else {
					Destroy(arrWalls[kolom , baris].WallDepan);
				}
			
			} else if (nextStep.y != baris) {
				if (nextStep.y > baris) {
					Destroy(arrWalls[kolom , baris].WallKanan);
				} else {
					Destroy(arrWalls[kolom , baris].WallKiri);
				}
			
			}

			kolom = (int)nextStep.x;
			baris = (int)nextStep.y;
			arrWalls [kolom, baris].isVisited = true;



			walkThrough = "";
			walkThrough = GetNextPath (kolom , baris);
			yield return new WaitForSeconds (delayMakeLabyrith);
//			Debug.Log (walkThrough);
			if (walkThrough == "Clear") {
				bool recreate = false;
				RecheckMap (ref kolom, ref baris , ref recreate);
				if (recreate) {
					walkThrough = GetNextPath (kolom , baris);
				} else {
					break;
				}
			}

		}


		for (int i = 0; i < countTurbin; i++) {
			int x = Random.Range ((int)minPos[i].x, (int)maxPos [i].x);
			int y = Random.Range ((int)minPos[i].y, (int)maxPos [i].y);
			//Debug.Log (x + "  " + y);
			GameObject temp = Instantiate (turbinPrefabs, arrWalls [x , y].vector2Position, Quaternion.identity);
			temp.GetComponent<TurnGeneratorScript> ().timeForTick = timeBeats;
		}
		isDoneCreated = true;
	}

	void RecheckMap (ref int kolom, ref int baris , ref bool recreate){
		bool belumTerlewat = false;

		for (int i = sizeTinggi - 1; i >= 0; i--) {

			for (int j = sizeLebar - 1; j >= 0 ; j--) {
				//Debug.Log (i + " , " + j);
				//Debug.Log(arrWalls [j , i].isVisited == false);
				if(arrWalls [j , i].isVisited == false){
					if (!belumTerlewat) {
						belumTerlewat = true;
						if (i + 1 < sizeTinggi) {
							kolom = j;
							baris = i + 1;
							recreate = true;
							return;

						}
					}
				}
				if (belumTerlewat && arrWalls [j, i].isVisited == true) {
					kolom = j;
					baris = i;
					string walkTemp = GetNextPath (kolom, baris);
					if (walkTemp == "Clear") {
						continue;
					}
					recreate = true;
					return;
				}
			}
		}

		recreate = false;

	
	}

	string GetNextPath(int kolom, int baris){
		string atas = " ";
		string bawah = " ";
		string kanan = " ";
		string kiri = " ";

		if ( (kolom - 1 >= 0) && arrWalls [kolom - 1, baris].isVisited == false) {
			atas = (kolom - 1).ToString()+"."+baris.ToString();
		}

		if ( (kolom + 1 < sizeLebar) && arrWalls [kolom + 1, baris].isVisited == false) {
			bawah = (kolom + 1).ToString()+"."+baris.ToString();
		}
		if ( (baris - 1 >= 0) && arrWalls [kolom, baris  - 1].isVisited == false) {
			kiri = kolom.ToString()+"."+(baris - 1).ToString();
		}
		if ( (baris + 1 < sizeTinggi) && arrWalls [kolom, baris  + 1].isVisited == false) {
			kanan = kolom.ToString()+"."+(baris + 1).ToString();
		}

		if ((atas == " ") &&(bawah == " ")&&(kanan == " ") && (kiri == " ")) {
			return "Clear";
		}

	
		return atas + "," + bawah + "," + kanan + "," + kiri;
	}


}
