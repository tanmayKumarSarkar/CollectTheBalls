using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

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

		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"Start"))
		{
			Application.LoadLevel ("StartMenu");
		}
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(2*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"How To Play"))
		{
			Application.LoadLevel ("HowToPlay");
		}
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(3*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"High Score"))
		{
			Application.LoadLevel ("HighScoreBoard");
		}
		if (GUI.Button(
			new Rect((adjustedWidth/12) ,(4*nativeHeight / 5) - (buttonHeight / 2),buttonWidth,buttonHeight),"Exit"))
		{
			Application.Quit();
		}
	}
	
	// Update is called once per frame
	void Update () {	

	}
}
