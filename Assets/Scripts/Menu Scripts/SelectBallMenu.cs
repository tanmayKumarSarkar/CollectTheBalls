using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectBallMenu : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;

	bool boolBallSelected;

	public Texture2D[] balls_tx;
	public GameObject[] balls_obj;
	public List<GameObject> balls_selected;
	public int levelNoBalls; 
	int listCount;

	public AudioClip AddBtnSound;

	ScenesControlScript scsScriptSBM;


	// Use this for initialization
	void Start () {
	
		skin = Resources.Load("SelectBallMenuGUISkin") as GUISkin;
		listCount = 1;
		boolBallSelected = true;

		// Getting No Of Balls From Script.....
		scsScriptSBM = GameObject.Find ("ScenesControlObjectScript").GetComponent<ScenesControlScript> ();
		levelNoBalls = scsScriptSBM.noBallLevel;Debug.Log (levelNoBalls);
	}

	void OnGUI () {
	
		GUI.skin = skin;
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		
		float buttonWidth = adjustedWidth/3f;
		float buttonHeight = 250;

		GUIStyle style = GUI.skin.GetStyle ("Button");
		style.fixedWidth = adjustedWidth / 3.5f;
		style.fixedHeight = nativeHeight / 11f;

		//GUI Label..........
		GUIStyle lbl1Style = GUI.skin.GetStyle ("Label");
		lbl1Style.fontSize = (int)(adjustedWidth / 11.60f);
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 12) , adjustedWidth/1.2f, 250f), "Select "+levelNoBalls+" balls !!");

		// GUI Layout Area........
		GUILayout.BeginArea(
			new Rect((adjustedWidth/12) ,(nativeHeight /4.7f),adjustedWidth/1.2f,nativeHeight /1.6f),"box");
			

		GUILayout.BeginVertical("box");
		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button (balls_tx [0])) {
			AddBallsToList (0);		}		
		if (GUILayout.Button (balls_tx [1])) {
			AddBallsToList (1);	}		
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button (balls_tx [2])) {
			AddBallsToList (2);	}		
		if (GUILayout.Button (balls_tx [3])) {
			AddBallsToList (3);	}		
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button (balls_tx [4])) {
			AddBallsToList (4);	}		
		if (GUILayout.Button (balls_tx [5])) {
			AddBallsToList (5);	}		
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button (balls_tx [6])) {
			AddBallsToList (6);	}		
		if (GUILayout.Button (balls_tx [7])) {
			AddBallsToList (7);	}		
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal("box");
		if (GUILayout.Button (balls_tx [8])) {
			AddBallsToList (8);	}		
		if (GUILayout.Button (balls_tx [9])) {
			AddBallsToList (9);	}		
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		GUILayout.EndArea ();


		//Selected Images Show......
		GUILayout.BeginArea(
			new Rect((adjustedWidth/12) ,(nativeHeight /1.4f),adjustedWidth/1.2f,nativeHeight /8f),"box2");
		GUILayout.BeginHorizontal("box");
		if (balls_selected.Any ()) {
			for (int i = 0; i < levelNoBalls; i++){
				if(balls_selected.Count > i){if (balls_selected.ElementAt (i) != null){
					
						GUILayout.Box (balls_tx[GetIndexOfBalls_tx (i)]); }}
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();

		// Button Style..........
		style.fontSize = (int)(adjustedWidth / 11.5f);
		style.fixedWidth = adjustedWidth / 2.6f;
		// Ok And Cancel Button.........
		if (GUI.Button (
			new Rect ((adjustedWidth / 12), (10*nativeHeight / 12), adjustedWidth/2.6f , 250f), "Ok")) 
		{
			if(balls_selected.Count == levelNoBalls){
				scsScriptSBM.selectedBalls = balls_selected.ToArray ();
				Application.LoadLevel ("Level");
			}
			else{
				boolBallSelected = false;
			}
		}
		if (GUI.Button (
			new Rect ((6.4f*adjustedWidth / 12), (10*nativeHeight / 12), adjustedWidth / 2.6f, 250f), "Cancel")) 
		{
			Application.LoadLevel ("Menu");
		}

		// All Balls Are NotSelected........
		if (boolBallSelected  == false && balls_selected.Count != levelNoBalls){
			GUIStyle lbl2Style = GUI.skin.GetStyle ("Label");
			lbl2Style.fontSize = (int)(adjustedWidth / 15.35f);
			GUI.Label (new Rect ((adjustedWidth / 7), (9.355f*nativeHeight / 12), adjustedWidth/1.5f , 300f),"** Select "+levelNoBalls+" Balls");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("StartMenu");
		}
	}

	void AddBallsToList (int ArrIndex){

		if(!balls_selected.Find (t =>t.name == balls_obj[ArrIndex].name.ToString())){
			if (listCount <=levelNoBalls){
				balls_selected.Add (balls_obj[ArrIndex]);
			}
			if (listCount >levelNoBalls){
				balls_selected.RemoveAt(0);
				balls_selected.Add (balls_obj[ArrIndex]);
			}
			listCount ++;
			AudioSource.PlayClipAtPoint(AddBtnSound, transform.position);
		}
		boolBallSelected = true;
	}

	int GetIndexOfBalls_tx (int listIndex){

		string strBllSltdNm = balls_selected.ElementAt (listIndex).name.ToLower ();

		//Getting The Index From ball_tx[] array which are in Lists.........
		int index = System.Array.FindIndex (balls_tx, w => w.name == strBllSltdNm);
		//Debug.Log (balls_tx[listIndex].name+" :: "+strBllSltdNm+":"+index);
		return index;
	}
}
