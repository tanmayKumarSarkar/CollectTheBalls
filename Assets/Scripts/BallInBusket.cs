using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class BallInBusket : MonoBehaviour {

	// Own Objects.......
	public GameObject[] selectedBalls;
	string[] strSltdBalls;
	public int points = 0;
	public int missBalls = 0;

	public bool pauseEnabled;
	public bool otherMenuOpened;
	string highScoreCheckKey;

	//Other Component Scripts....
	GenerateBalls generateBallsScriptBIB;
	public GameObject[] specialBalls;
	string[] strSpecialBalls;
	float specialModeTimeCount = 12.0f;
	ScenesControlScript scsScriptBIB;

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	public Texture2D ballPointsTexture;
	public Texture2D ballCancelPointsTexture;
	private GUISkin skin;

	public AudioClip pointSound;
	public AudioClip missPointSound;
	public AudioClip SpecialPointSound;

	// Use this for initialization
	void Start () {
	
		skin = Resources.Load("ButtonGUISkin") as GUISkin;
		pauseEnabled = false;
		otherMenuOpened = false;
		Time.timeScale = 1;

		// Special Balls Array........
		generateBallsScriptBIB = GameObject.Find ("Cart").GetComponent<GenerateBalls> ();
		specialBalls = generateBallsScriptBIB.specialBalls;
		//Special Balls String Array....
		strSpecialBalls = Array.ConvertAll (specialBalls, w => w.ToString ());

		//Getting Selected Balls Array.....
		scsScriptBIB = GameObject.Find ("ScenesControlObjectScript").GetComponent<ScenesControlScript> ();
		selectedBalls = scsScriptBIB.selectedBalls;
		// Key HighScoreEasy Or.......
		highScoreCheckKey = "HighScore"+scsScriptBIB.strLevel;
		//Score RESET
		//if(scsScriptBIB.strLevel=="Easy"){PlayerPrefs.SetInt (highScoreCheckKey,0);}
		// Selected Balls String Array.....
		strSltdBalls = Array.ConvertAll (selectedBalls, x => x.ToString ());
	}
	
	// Update is called once per frame
	void Update () {

		if (missBalls > 20){
			pauseEnabled = true;
			otherMenuOpened = true;
			Time.timeScale = 0;

			if (CheckForHighScore()){Application.LoadLevel("NewHighScore");}
		}
	}

	void OnTriggerEnter2D (Collider2D collider){

		if(collider.gameObject.CompareTag ("Ball")){
			//Debug.Log (selectedBalls[0]);
			string cmprStr = string.Format("{0}",collider.gameObject);
			cmprStr = cmprStr.Replace("(Clone)","");
			//Debug.Log(cmprStr);
			if (strSltdBalls.Any(cmprStr.Contains)){
				points ++;
				AudioSource.PlayClipAtPoint(pointSound, transform.position);
				//Debug.Log ("Point"+points);
			}
			if (!strSltdBalls.Any(cmprStr.Contains)){
				AudioSource.PlayClipAtPoint(missPointSound, transform.position);
				missBalls ++;
				//Debug.Log ("::   miss"+missBalls);
			}

			Destroy (collider.gameObject);
		}

		if (collider.gameObject.CompareTag ("SpecialBall")){
			string cmprStr2 = string.Format("{0}",collider.gameObject);
			cmprStr2 = cmprStr2.Replace("(Clone)","");
			//Debug.Log(cmprStr);
			if (strSpecialBalls.Any(cmprStr2.Contains)){

				Debug.Log (string.Format("{0}",collider.name).Replace("(Clone)",""));

				generateBallsScriptBIB.slowMode = false;
				generateBallsScriptBIB.positivePointMode = false;
				generateBallsScriptBIB.negativePoinMode = false;

				if(string.Format("{0}",collider.name).Replace("(Clone)","") == "Time_ball"){
					generateBallsScriptBIB.specialModeTimeCount = specialModeTimeCount;
					generateBallsScriptBIB.specialBallTimeCount = 0.0f;
					generateBallsScriptBIB.slowMode = true;
				}
				if(string.Format("{0}",collider.name).Replace("(Clone)","") == "PlusP_ball"){
					generateBallsScriptBIB.specialModeTimeCount = specialModeTimeCount;
					generateBallsScriptBIB.specialBallTimeCount = 0.0f;
					generateBallsScriptBIB.positivePointMode = true;
				}
				if(string.Format("{0}",collider.name).Replace("(Clone)","") == "MinusP_ball"){
					generateBallsScriptBIB.specialModeTimeCount = specialModeTimeCount;
					generateBallsScriptBIB.specialBallTimeCount = 0.0f;
					generateBallsScriptBIB.negativePoinMode = true;
				}
				if(string.Format("{0}",collider.name).Replace("(Clone)","") == "Charge_ball"){
					if(missBalls <= 5){missBalls = 0;}
					else{missBalls -= 5;}
				}
				if(string.Format("{0}",collider.name).Replace("(Clone)","") == "Star_ball"){
					points += 10;
				}
				AudioSource.PlayClipAtPoint(SpecialPointSound, transform.position);
			}

			Destroy (collider.gameObject);
		}
	}

	void OnGUI() {

		GUI.skin = skin;
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		
		//GUI.Box(new Rect(8, 8, adjustedWidth - 20, 1900), "Box");
		//GUI.DrawTexture (new Rect(0,0,adjustedWidth - 20, 1900),background);

		if (pauseEnabled ) {
			PopUpRestart();
		}

		DisplayPointCount ();
		DisplayCancelPointCount ();
	}

	void DisplayPointCount () {

		Rect ballPointsIconRect = new Rect(30, 20, adjustedWidth/14, 130);
		GUI.DrawTexture(ballPointsIconRect, ballPointsTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 100;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = Color.gray;
		
		Rect labelRect = new Rect(ballPointsIconRect.xMax + 10, ballPointsIconRect.y+5, 60, 130);
		GUI.Label(labelRect, points.ToString(), style);
	}

	void DisplayCancelPointCount () {

		Rect ballCancelPointsIconRect = new Rect((adjustedWidth - adjustedWidth/5) , 20, adjustedWidth/14, 130);
		GUI.DrawTexture(ballCancelPointsIconRect, ballCancelPointsTexture);                         
		
		GUIStyle style = new GUIStyle();
		style.fontSize = 100;
		style.fontStyle = FontStyle.BoldAndItalic;
		style.normal.textColor = Color.red;
		
		Rect labelRect = new Rect(ballCancelPointsIconRect.xMax + 5, ballCancelPointsIconRect.y+5, 60, 130);
		GUI.Label(labelRect, missBalls.ToString(), style);
	}

	void PopUpRestart () {

		float buttonWidth = adjustedWidth/3.2f;
		float buttonHeight = 200;

		GUIStyle style = GUI.skin.GetStyle ("Button"); //new GUIStyle

		if (GUI.Button(
			new Rect((adjustedWidth/2) - (7*buttonWidth/6),(nativeHeight / 2) - (buttonHeight / 2),buttonWidth,buttonHeight),"Restart",style))
		{
			otherMenuOpened = false;
			Application.LoadLevel (Application.loadedLevel);
		}
		if (GUI.Button(
			new Rect((adjustedWidth/2) + (1*buttonWidth/6),(nativeHeight / 2) - (buttonHeight / 2),buttonWidth,buttonHeight),"Menu"))
		{
			otherMenuOpened = false;
			Application.LoadLevel ("Menu");
		}
	}

	bool CheckForHighScore () {
		bool isHighScore = false;
		if(!PlayerPrefs.HasKey (highScoreCheckKey)){
			PlayerPrefs.SetInt(highScoreCheckKey,0);
		}
		else{
			if(points > PlayerPrefs.GetInt (highScoreCheckKey)){
				PlayerPrefs.SetInt(highScoreCheckKey,points);
				isHighScore = true;
			}
		}
		Debug.Log (PlayerPrefs.GetInt (highScoreCheckKey)+points);
		return isHighScore;
	}

}
