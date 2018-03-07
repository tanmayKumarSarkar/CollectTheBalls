using UnityEngine;
using System.Collections;
using System.Linq;

public class GenerateBalls : MonoBehaviour {

	//screen
	float nativeWidth = 1080f;
	float nativeHeight = 1920f;

	//texture image
	//public Texture background;

	// Own Objects......
	public GameObject[] balls;
	public GameObject[] specialBalls;

	public float timeDelayStart = 0.90f ;
	public float timeDelayEnd = 1.20f ;
	public float SpecialTimeDelayStart = 10.0f;
	public float timeDelayFactor = 1.00f;

	public float gravityScaleRangeMin = 0.09f;
	public float gravityScaleRangeMax = 0.10f;

	// Special Effects.....
	public bool slowMode;
	public bool positivePointMode;
	public bool negativePoinMode;

	//Other Component Scripts....
	BallInBusket ballInBusketScriptGB;
	public GameObject[] selectedBalls;

	// UnSelected balls...........
	public GameObject[] UnSelectedBalls;

	float timeCount;
	float specialTimeCount;
	public float specialBallTimeCount;
	public float specialModeTimeCount;
	float longTimeCount;

	float timeDelay;
	float specialTimeDelay;
	float specialBallTimeDelay;
	int minute;
	int hour;
	int second;

	// Use this for initialization
	void Start () {
	
		timeCount = 0.0f;
		specialTimeCount = 0.0f;
		specialBallTimeCount = 0.0f;
		specialModeTimeCount = 0.0f;
		longTimeCount = 0.0f;

		slowMode = false;
		positivePointMode = false;
		negativePoinMode = false;

		// Selected Balls Array........
		ballInBusketScriptGB = GameObject.Find ("Cart").GetComponent<BallInBusket> ();
		selectedBalls = ballInBusketScriptGB.selectedBalls;

		// UnSelected Balls Array ..............
		UnSelectedBalls = balls.Except (selectedBalls).ToArray ();
	}

	void OnGUI()
	{
		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;
		
		
		// Scale width the same as height - cut off edges to keep ratio the same
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(ry, ry, 1));
		
		// Get width taking into account edges being cut off or extended
		float adjustedWidth = nativeWidth * (rx / ry);
		
		//GUI.Box(new Rect(8, 8, adjustedWidth - 20, 1900), "Box");
		//GUI.DrawTexture (new Rect(0,0,adjustedWidth - 20, 1900),background);
	}
	
	// Update is called once per frame
	void Update () {

		timeCount += Time.deltaTime;
		specialTimeCount += Time.deltaTime;
		specialBallTimeCount += Time.deltaTime;
		specialModeTimeCount -= Time.deltaTime;
		longTimeCount = Time.time;
		//Debug.Log (longTimeCount +"  "+ timeCount);

		timeDelay = Random.Range (timeDelayStart, timeDelayEnd);
		specialTimeDelay = Random.Range (SpecialTimeDelayStart, SpecialTimeDelayStart + 5.00f);
		specialBallTimeDelay = Random.Range (timeDelayStart * timeDelayFactor, timeDelayEnd * timeDelayFactor);

		// Setting All Modes To False After TimeUp........
		if (specialModeTimeCount <= 0.0f){
			slowMode = false;
			positivePointMode = false;
			negativePoinMode = false;
		}
		if (specialModeTimeCount <= 4.0f ){
			slowMode = false;
			negativePoinMode = false;
		}

		// Create General Balls.........
		if (timeCount >= timeDelay) {
			if (!slowMode && !positivePointMode && !negativePoinMode){
				CreateBalls ();
			}
			timeCount = 0.0f;
		}

		// Create Special Balls...........
		if (specialTimeCount >= specialTimeDelay) {
			if (!slowMode && !positivePointMode && !negativePoinMode){
				CreateSpecialBalls ();
			}
			specialTimeCount = 0.0f;
		}

		// Create Special Modes...........
		if(specialModeTimeCount > 4.0f){
			if (specialBallTimeCount >= specialBallTimeDelay){
				if (slowMode && !positivePointMode && !negativePoinMode){
					CreateSlowBalls ();
				}
				if (!slowMode && positivePointMode && !negativePoinMode){
					CreatePositiveBalls ();
				}
				if (!slowMode && !positivePointMode && negativePoinMode){
					CreateNegativeBalls ();
				}
				specialBallTimeCount = 0.0f;
			}
			//specialModeTimeCount -= Time.deltaTime;
		}

	}

	void FixedUpdate () {


	}

	// Create Normal Balls--------------->>>>>>
	void CreateBalls () {

		GameObject ballLeft = (GameObject)Instantiate (balls [Random.Range (0, balls.Length)]);
		GameObject ballRight = (GameObject)Instantiate (balls [Random.Range (0, balls.Length)]);

		ballLeft.transform.position = new Vector3 (Random.Range(-2.5f,-0.06f), 6.00f, 0.01f);
		ballLeft.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin, gravityScaleRangeMax);
		ballRight.transform.position = new Vector3 (Random.Range(0.06f,2.5f), 7.00f, 0.01f);
		ballRight.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin, gravityScaleRangeMax);

		Destroy (ballLeft, 20);
		Destroy (ballRight, 20);

		/*GameObject balls = (GameObject) Instantiate (ball);
		balls.transform.position = new Vector3 (Random.Range(-2.4f,2.4f), 6, 0);
		balls.rigidbody2D.gravityScale = Random.Range (0.1f, 0.5f);
		//balls.renderer.material.shader = Shader.Find ("Diffuse");
		balls.renderer.material.color = new Color(Random.value, Random.value, Random.value,1.0f);
		Destroy (balls, 5);*/
	}

	// Create Special Balls--------------->>>>>>
	void CreateSpecialBalls () {
		
		GameObject ballLeft = (GameObject)Instantiate (specialBalls [Random.Range (0, specialBalls.Length)]);
		GameObject ballRight = (GameObject)Instantiate (specialBalls [Random.Range (0, specialBalls.Length)]);
		
		ballLeft.transform.position = new Vector3 (Random.Range(-2.5f,-0.1f), 8.0f, 0.01f);
		ballLeft.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.4f, gravityScaleRangeMax/1.4f);
		ballRight.transform.position = new Vector3 (Random.Range(0.0f,2.5f), 6.00f, 0.01f);
		ballRight.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.4f, gravityScaleRangeMax/1.4f);
		
		Destroy (ballLeft, 20);
		Destroy (ballRight, 20);
	}

	// Create Slow Balls--------------->>>>>>
	void CreateSlowBalls () {
		
		GameObject ballLeft = (GameObject)Instantiate (balls [Random.Range (0, balls.Length)]);
		GameObject ballRight = (GameObject)Instantiate (balls [Random.Range (0, balls.Length)]);
		
		ballLeft.transform.position = new Vector3 (Random.Range(-2.5f,0.0f), 6.50f, 0.01f);
		ballLeft.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/2.0f, gravityScaleRangeMax/2.0f);
		ballRight.transform.position = new Vector3 (Random.Range(0.1f,2.5f), 7.00f, 0.01f);
		ballRight.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/2.0f, gravityScaleRangeMax/2.0f);
		
		Destroy (ballLeft, 35);
		Destroy (ballRight, 35);
	}

	// Create Positive Point Balls--------------->>>>>>
	void CreatePositiveBalls () {
		
		GameObject ballLeft = (GameObject)Instantiate (selectedBalls [Random.Range (0, selectedBalls.Length)]);
		GameObject ballRight = (GameObject)Instantiate (selectedBalls [Random.Range (0, selectedBalls.Length)]);
		
		ballLeft.transform.position = new Vector3 (Random.Range(-2.5f,0.0f), 6.00f, 0.01f);
		ballLeft.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.25f, gravityScaleRangeMax/1.25f);
		ballRight.transform.position = new Vector3 (Random.Range(0.1f,2.5f), 7.00f, 0.01f);
		ballRight.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.35f, gravityScaleRangeMax/1.35f);
		
		Destroy (ballLeft, 25);
		Destroy (ballRight, 25);
	}

	// Create Negative Point Balls--------------->>>>>>
	void CreateNegativeBalls () {

		GameObject ballLeft = (GameObject)Instantiate (UnSelectedBalls [Random.Range (0, UnSelectedBalls.Length)]);
		GameObject ballRight = (GameObject)Instantiate (UnSelectedBalls [Random.Range (0, UnSelectedBalls.Length)]);
		
		ballLeft.transform.position = new Vector3 (Random.Range(-2.5f,0.0f), 6.50f, 0.01f);
		ballLeft.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.4f, gravityScaleRangeMax/1.4f);
		ballRight.transform.position = new Vector3 (Random.Range(0.1f,2.5f), 7.00f, 0.01f);
		ballRight.rigidbody2D.gravityScale = Random.Range (gravityScaleRangeMin/1.4f, gravityScaleRangeMax/1.4f);
		
		Destroy (ballLeft, 25);
		Destroy (ballRight, 25);
	}

}
