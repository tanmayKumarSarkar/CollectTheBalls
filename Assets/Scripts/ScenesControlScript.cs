using UnityEngine;
using System.Collections;

public class ScenesControlScript : MonoBehaviour {

	public string strLevel;
	public int noBallLevel;
	public GameObject[] selectedBalls;

	void Awake () {
	
		DontDestroyOnLoad (transform.gameObject);
		if (FindObjectsOfType (GetType ()).Length >1){
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
		strLevel = "";
		noBallLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
