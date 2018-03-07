using UnityEngine;
using System.Collections;

public class TiltControl : MonoBehaviour {

	public float  speed = 6.0f ;
	public bool pauseEnabled;
	public bool otherMenuOpened;
	BallInBusket ballInBusketScript;

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;
	float rx;
	float ry;
	float adjustedWidth;
	private GUISkin skin;

	public float sensitivity = 6.00f;
	public float smoothInputHorizontal;
	

	// Use this for initialization
	void Start () {
	
		skin = Resources.Load("ButtonGUISkin") as GUISkin;
		pauseEnabled = false;
		otherMenuOpened = false;
		Time.timeScale = 1;

		ballInBusketScript = GameObject.Find ("Cart").GetComponent<BallInBusket> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		// Tilt Control.........
		Vector2 dir = Vector2.zero;				
		dir.x = Input.acceleration.x;
		dir.y = 0;		
		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
		*/
			
		//Left And Right Touch Control......>>>>>>
		/*
		Vector2 dir1 = Vector2.zero;
		dir1.y = 0;
		if (Input.touchCount > 0 && Input.GetTouch (0).position.x < Screen.width / 2) {
			dir1.x = -1;
		}
		else if (Input.touchCount > 0 && Input.GetTouch (0).position.x > Screen.width / 2) {
			dir1.x = 1;
		}
		speed = Mathf.Lerp (speed, speed, Time.deltaTime*speed);
		// Move object
		transform.Translate (dir1 * speed);
		*/

		Vector2 dir1 = Vector2.zero;
		dir1.y = 0;
		if (Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Began || 
		                             Input.GetTouch (0).phase == TouchPhase.Stationary ||
		                             Input.GetTouch (0).phase == TouchPhase.Moved ) ) {
			smoothInputHorizontal = Mathf.Lerp (smoothInputHorizontal, 1, Time.deltaTime * sensitivity);
		}
		else{
			smoothInputHorizontal = Mathf.Lerp (smoothInputHorizontal, 0, Time.deltaTime * sensitivity);
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).position.x < Screen.width / 2) {
			dir1.x = -1 * smoothInputHorizontal;;
		}
		else if (Input.touchCount > 0 && Input.GetTouch (0).position.x > Screen.width / 2) {
			dir1.x = 1 * smoothInputHorizontal;;
		}
		dir1 *= Time.deltaTime;
		transform.Translate (dir1 * speed);

		//End Of Touch Controll......>>>>>>


		// Keybord Control.......>>>>>>>>>>>

		Vector2 dir = new Vector2 (Input.GetAxis ("Horizontal"), 0);
		
		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		//speed = Mathf.Lerp (speed, speed, Time.deltaTime*speed);
		// Move object
		transform.Translate (dir * speed);

		//End Of KeyBoard Control.....>>>>>>>>

		//Clamp transform position
		//Here transform's x position will get clamped to a value of -3 to 3 change it as per your requirement
		transform.position = new Vector2(Mathf.Clamp(transform.position.x,-2.75f,2.7f),-4.5f);

		otherMenuOpened = ballInBusketScript.otherMenuOpened;

		//back button....
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown (KeyCode.Menu)) && !otherMenuOpened){
			if (pauseEnabled == true){
				pauseEnabled = false;
				Time.timeScale = 1;
			}
			else if (pauseEnabled == false){
				pauseEnabled =true;
				Time.timeScale = 0;
			}
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

		float buttonWidth = adjustedWidth/3.2f;
		float buttonHeight = 200;

		//new GUIStyle
		GUIStyle style = GUI.skin.GetStyle ("Button");
		style.fontSize = 88;

		if (pauseEnabled){
			if (GUI.Button(
				new Rect((adjustedWidth/2) - (15.5f*buttonWidth/10),(nativeHeight / 2) - (buttonHeight / 2),buttonWidth,buttonHeight),"Resume"))
			{
				pauseEnabled = false;
				Time.timeScale = 1;
			}
			if (GUI.Button(
				new Rect((adjustedWidth/2) - (5*buttonWidth/10),(nativeHeight / 2) - (buttonHeight / 2),buttonWidth,buttonHeight),"Restart"))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
			if (GUI.Button(
				new Rect((adjustedWidth/2) + (5.5f*buttonWidth/10),(nativeHeight / 2) - (buttonHeight / 2),buttonWidth,buttonHeight),"Menu"))
			{
				Application.LoadLevel ("Menu");
			}

			//pausing game...
			//pauseEnabled = false;
			//Time.timeScale = 1;
		}
	}

}
