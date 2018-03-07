using UnityEngine;
using System.Collections;
using System.Linq;

public class TapInput : MonoBehaviour {


	//Other Component Scripts....
	GenerateBalls generateBallsScriptTI;
	public GameObject[] specialBalls;
	string[] strSpecialBalls;
	float specialModeTimeCount = 8.0f;
	BallInBusket ballInBusketScriptTI;

	// Use this for initialization
	void Start () {
	
		// Special Balls Array........
		generateBallsScriptTI = GameObject.Find ("Cart").GetComponent<GenerateBalls> ();
		specialBalls = generateBallsScriptTI.specialBalls;
		//Special Balls String Array....
		strSpecialBalls = System.Array.ConvertAll (specialBalls, w => w.ToString ());

		ballInBusketScriptTI = GameObject.Find ("Cart").GetComponent<BallInBusket> ();
	}
	
	// Update is called once per frame
	void Update () {
	

		RaycastHit2D hit;
		// Getting Touch Or Click Input...........
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began ) || (Input.GetMouseButtonDown(0)) )
		{
			// Mouse Click...........
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.mousePosition)),Vector2.zero);

			// Touch Input...........
			//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint ((Input.GetTouch (0).position)),Vector2.zero);

			if(hit.collider != null){

				if (hit.collider.gameObject.CompareTag ("SpecialBall")){
					string cmprStr2 = string.Format("{0}",hit.collider.gameObject);
					cmprStr2 = cmprStr2.Replace("(Clone)","");
					//Debug.Log(cmprStr);
					if (strSpecialBalls.Any(cmprStr2.Contains)){
						
						Debug.Log (string.Format("{0}",hit.collider.name).Replace("(Clone)",""));
						
						generateBallsScriptTI.slowMode = false;
						generateBallsScriptTI.positivePointMode = false;
						generateBallsScriptTI.negativePoinMode = false;
						
						if(string.Format("{0}",hit.collider.name).Replace("(Clone)","") == "Time_ball"){
							generateBallsScriptTI.specialModeTimeCount = specialModeTimeCount;
							generateBallsScriptTI.specialBallTimeCount = 0.0f;
							generateBallsScriptTI.slowMode = true;
						}
						if(string.Format("{0}",hit.collider.name).Replace("(Clone)","") == "PlusP_ball"){
							generateBallsScriptTI.specialModeTimeCount = specialModeTimeCount;
							generateBallsScriptTI.specialBallTimeCount = 0.0f;
							generateBallsScriptTI.positivePointMode = true;
						}
						if(string.Format("{0}",hit.collider.name).Replace("(Clone)","") == "MinusP_ball"){
							generateBallsScriptTI.specialModeTimeCount = specialModeTimeCount;
							generateBallsScriptTI.specialBallTimeCount = 0.0f;
							generateBallsScriptTI.negativePoinMode = true;
						}
						if(string.Format("{0}",hit.collider.name).Replace("(Clone)","") == "Charge_ball"){
							if(ballInBusketScriptTI.missBalls <= 5){ballInBusketScriptTI.missBalls = 0;}
							else{ballInBusketScriptTI.missBalls -= 5;}
						}
						if(string.Format("{0}",hit.collider.name).Replace("(Clone)","") == "Star_ball"){
							ballInBusketScriptTI.points += 10;
						}
						
					}
					
					Destroy (hit.collider.gameObject);
				}
				//Debug.Log(hit.collider.name+hit.collider.gameObject.tag);
			}
		}
	}
}
