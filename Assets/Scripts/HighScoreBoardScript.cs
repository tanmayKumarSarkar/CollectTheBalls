using UnityEngine;
using System.Collections;

public class HighScoreBoardScript : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;

	// Use this for initialization
	void Start () {
	
		skin = Resources.Load("MenuBtnGUISkin") as GUISkin;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("Menu");
		}
	}

	void OnGUI () {
		
		GUI.skin = skin;
		rx = Screen.width / nativeWidth;
		ry = Screen.height / nativeHeight;
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		// Get width taking into account edges being cut off or extended
		adjustedWidth = nativeWidth * (rx / ry);
		
		float buttonWidth = adjustedWidth/1.2f;
		float buttonHeight = 230;

		// new GUI Style.......
		GUIStyle Btnstyle = GUI.skin.GetStyle ("Button");
		Btnstyle.fontSize = (int)adjustedWidth / 8;
		GUIStyle Lblstyle = GUI.skin.GetStyle ("Label");
		Lblstyle.fontSize = (int)adjustedWidth / 11;

		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 5.5f) - (buttonHeight / 2), buttonWidth, buttonHeight), "Highscore Board");
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 3.0f) - (buttonHeight / 2), buttonWidth, buttonHeight), "Easy      "+GetHighScore("HighScoreEasy"));
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 2.0f) - (buttonHeight / 2), buttonWidth, buttonHeight), "Medium    "+GetHighScore("HighScoreMedium"));
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 1.5f) - (buttonHeight / 2), buttonWidth, buttonHeight), "Hard      "+GetHighScore("HighScoreHard"));
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(nativeHeight ) /1.3f,buttonWidth,buttonHeight),"OK"))
		{
			Application.LoadLevel ("Menu");
		}
	}
	
	string GetHighScore (string highScoreCheckKey) {
		
		string highScore = "0";
		if(!PlayerPrefs.HasKey (highScoreCheckKey)){
			PlayerPrefs.SetInt(highScoreCheckKey,0);
		}
		else{
			highScore = PlayerPrefs.GetInt (highScoreCheckKey).ToString ();
		}
		return highScore;
	}
}
