using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

	public static long endScore;
	public Text endScoreText;

	// Use this for initialization
	void Start () {
		this.endScoreText.text = "YOUR SCORE: " + endScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
