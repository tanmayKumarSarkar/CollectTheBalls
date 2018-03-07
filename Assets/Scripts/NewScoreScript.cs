using UnityEngine;
using System.Collections;

public class NewScoreScript : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;
	
	ScenesControlScript scsScriptNSS;
	string highScoreCheckKey;

	// Use this for initialization
	void Start () {
	
		skin = Resources.Load("MenuBtnGUISkin") as GUISkin;
		scsScriptNSS = GameObject.Find ("ScenesControlObjectScript").GetComponent<ScenesControlScript> ();
		highScoreCheckKey = "HighScore"+scsScriptNSS.strLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
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
		Btnstyle.fontSize = (int)adjustedWidth / 10;
		GUIStyle Lblstyle = GUI.skin.GetStyle ("Label");
		Lblstyle.fontSize = (int)adjustedWidth / 11;
		
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "New Highscore !!");
		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 2) - (buttonHeight / 2), buttonWidth, buttonHeight), GetHighScore());
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(nativeHeight ) /1.4f,buttonWidth,buttonHeight),"OK"))
		{
			Application.LoadLevel ("Level");
		}
	}

	string GetHighScore () {

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
