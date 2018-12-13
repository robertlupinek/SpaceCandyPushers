using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    //Set "globals"
    public static float score;
    public static float comboMulti = 1f;
    public static float comboTimer;
    public static string printText = "score ";

    public GameObject ObjectToCount;
    public Text scoreText;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {

        string multiText = "";
        if (comboTimer > 0)
        {
            multiText = "     +" + (comboMulti - 1).ToString();
        }

        scoreText.text = "SCORE: " + score.ToString() + multiText;

        comboTimer -= Time.deltaTime;
        if (comboTimer <= 0)
        {
            comboMulti = 1;
        }

    }
}
