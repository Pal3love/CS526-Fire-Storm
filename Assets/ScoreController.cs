using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	private int finalScore;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		finalScore = PlayerScript.score;
		scoreText.text = "YOUR SCORE: " + finalScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
