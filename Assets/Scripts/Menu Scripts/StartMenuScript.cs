using UnityEngine;
using System.Collections;

public class StartMenuScript : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;

	ScenesControlScript scsScriptSMS;
	
	// Use this for initialization
	void Start () {
		
		skin = Resources.Load("MenuBtnGUISkin") as GUISkin;
		scsScriptSMS = GameObject.Find ("ScenesControlObjectScript").GetComponent<ScenesControlScript> ();
		scsScriptSMS.strLevel = "";
		scsScriptSMS.noBallLevel = 0;
		
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
		Lblstyle.fontSize = (int)adjustedWidth / 12;

		GUI.Label (
			new Rect ((adjustedWidth / 12), (nativeHeight / 5) - (buttonHeight / 2), buttonWidth, buttonHeight), "Select Difficulty");
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(2*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"Easy"))
		{
			scsScriptSMS.strLevel = "Easy";
			scsScriptSMS.noBallLevel = 2;
			Application.LoadLevel ("SelectBallsMenu");
		}
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(3*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"Medium"))
		{
			scsScriptSMS.strLevel = "Medium";
			scsScriptSMS.noBallLevel = 3;
			Application.LoadLevel ("SelectBallsMenu");
		}
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(4*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"Hard"))
		{
			scsScriptSMS.strLevel = "Hard";
			scsScriptSMS.noBallLevel = 4;
			Application.LoadLevel ("SelectBallsMenu");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("Menu");
		}
	}
}
