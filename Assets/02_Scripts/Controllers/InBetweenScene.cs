using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InBetweenScene : MonoBehaviour {

    //Level Transition
    public GameObject transistionSprite;

    //Move to next level 
    private float nextLevelTimer = 2f;

    public TextMeshPro scoreText;

    //Start moving to next level
    private bool startNextLevelTransition = false;


    // Use this for initialization
    void Start () {
        Instantiate(transistionSprite, new Vector2(0, 0), transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {


        //Draw the current score
        scoreText.text = "SCORE: " + Globals.score.ToString();

        //Kick in the next scene transistion on key press
        if (Input.GetKey(Globals.inputK1["action"]) || Input.GetKey(Globals.inputK2["action"]) || Input.GetButtonDown(Globals.inputJ1["action"]) || Input.GetButtonDown(Globals.inputJ2["action"]) )
        {
            //Create the transistion fader and 
            GameObject myTransistion = Instantiate(transistionSprite, new Vector2(0, 0), transform.rotation);
            FadeInFadeOut myFadeInFadeout = myTransistion.GetComponent<FadeInFadeOut>();
            myFadeInFadeout.fadeDir = 1;
            myFadeInFadeout.fadeTimer = 0;
            startNextLevelTransition = true;
        }

        if (startNextLevelTransition)
        {
            nextLevelTimer -= Time.deltaTime;
            if ( nextLevelTimer <= 0)
            {
                //Increase the numeric level counter by 1.
                Globals.currentLevel += 1;

                //Handle reaching the end of the levels
                if (Globals.currentLevel > 4)
                {
                    Globals.currentLevel = 1;
                }
                //Load scene based on numeric level.
                SceneManager.LoadScene(Globals.levelToScene[Globals.currentLevel]);
            }
        }


    }
}
