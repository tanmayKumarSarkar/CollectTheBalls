using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class MissingBalls : MonoBehaviour {

	public GameObject[] selectedBalls;
	string[] strSltdBalls;
	BallInBusket ballInBusketScriptMB;

	GenerateBalls generateBallsScriptMB;

	public AudioClip missPointSound;

	// Use this for initialization
	void Start () {

		ballInBusketScriptMB = GameObject.Find ("Cart").GetComponent<BallInBusket> ();
		selectedBalls = ballInBusketScriptMB.selectedBalls; 
		strSltdBalls = Array.ConvertAll (selectedBalls, x => x.ToString ());

		generateBallsScriptMB = GameObject.Find ("Cart").GetComponent<GenerateBalls> ();
	}

	void OnTriggerEnter2D (Collider2D collider){

		if(collider.gameObject.CompareTag ("Ball")){
			//Debug.Log (selectedBalls[0]);
			string cmprStr = string.Format("{0}",collider.gameObject);
			cmprStr = cmprStr.Replace("(Clone)","");
			//Debug.Log(cmprStr);
			if (strSltdBalls.Any(cmprStr.Contains)){
				if (!generateBallsScriptMB.positivePointMode){
					ballInBusketScriptMB.missBalls ++;
					AudioSource.PlayClipAtPoint(missPointSound, transform.position);
				}
				//Debug.Log ("::   miss "+ballInBusket.missBalls);
			}						
			Destroy (collider.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
