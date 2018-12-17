using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControllerScript : MonoBehaviour {

    //Level Transition
    public GameObject transistionSprite;

    //Level complete object count
    public GameObject ObjectToCount;
    private int objectCount;

    //Timer running low warning sound
    public AudioClip timerAlarmClip;

    //Score and timer text
    public TextMeshPro scoreText;
    public TextMeshPro timerText;
    private float oneSecTimer = 1f;

    //Move to next level 
    private float nextLevelTimer = 2f;

    //levelTimer is used to countdown and end the level if it reaches 0.
    private float levelTimer;
    private bool levelChangeOnce = false;

    //Set up the level meta data
    private Dictionary<string, float> timers = new Dictionary<string, float>();
    private Dictionary<string, float> scoreGoals = new Dictionary<string, float>();
    private Dictionary<string, float> timerGoals = new Dictionary<string, float>();

    //Audio sources
    //Sounds
    private AudioSource source;

    // Use this for initialization
    void Start () {
        //Setup times for each level
        timers.Add("level1", 300);
        timers.Add("level2", 300);
        timers.Add("level3", 300);
        timers.Add("level4", 300);
        timers.Add("level5", 300);
        timers.Add("level6", 300);
        timers.Add("level7", 300);
        timers.Add("level8", 300);
        levelTimer = timers[Globals.currentLevel];
        //Set up the Audio source
        source = GetComponent<AudioSource>();

        Instantiate(transistionSprite, new Vector2(0,0), transform.rotation);
    }

    // Update is called once per frame
    void Update () {

        //Setup the string to display for script mulitpliers.
        //Currently it is setup to display, "Score: 10 + 4"  for a score of 10 and a multplier of 4.
        string multiText = "";
        if (Globals.comboTimer > 0)
        {
            //Note that we have to set the comboMulti to - 1 since it is already incremented for the next score in the MoveableBlock class.
            multiText = "     +" + (Globals.comboMulti - 1).ToString();
        }
        //Update the score text to be displayed in the UI Canvas that is a child of the GameController.
        scoreText.text = "SCORE: " + Globals.score.ToString() + multiText;
        float minutesRemaining = 0;
        float secondsRemaining = 0;
        //Count down the time timer.
        if (levelTimer > 0 && objectCount > 0)
        {
            levelTimer -= Time.deltaTime;
            //Calculate minutes and seconds remaining to put on the clock
            minutesRemaining = Mathf.FloorToInt( levelTimer / 60 );
            secondsRemaining = Mathf.RoundToInt( levelTimer - ( 60 * minutesRemaining) );
        }
 

        //Setup a 1 second timer
        oneSecTimer -= Time.deltaTime;
        if ( oneSecTimer <= 0)
        {
            oneSecTimer = 1f;

            //Plan an alarm sound if the seconds remainings is below 20
            if (levelTimer <= 20 && levelTimer > 0 )
            {
                source.PlayOneShot(timerAlarmClip, 1);
            }
        }

        //Get object count
        objectCount = GameObject.FindGameObjectsWithTag("Destroyable").Length;


        if ( levelTimer <= 0 || objectCount == 0 )
        {
            //Start the screen fade
            if ( nextLevelTimer == 2)
            {
                //Create the transistion fader and 
                GameObject myTransistion = Instantiate(transistionSprite, new Vector2(0, 0), transform.rotation);
                FadeInFadeOut myFadeInFadeout = myTransistion.GetComponent<FadeInFadeOut>();
                myFadeInFadeout.fadeDir = 1;
                myFadeInFadeout.fadeTimer = 0;
            }
            nextLevelTimer -= Time.deltaTime;
            if (nextLevelTimer <= 0)
            {
                if (!levelChangeOnce)
                {
                    SceneManager.LoadScene("InbetweenScene");
                    levelChangeOnce = true;
                }
            }

        }

        

        string extraZero = "";
        if (secondsRemaining < 10)
            extraZero = "0";
        //Update the timer text  to be displayed in the UI Canvas that is a child of the GameController.
        timerText.text = minutesRemaining.ToString() + ":" + extraZero + secondsRemaining.ToString();

        //Count down the combo timer and reset the combo if the timer runs out.
        Globals.comboTimer -= Time.deltaTime;

        if (Globals.comboTimer <= 0)
        {
            Globals.comboMulti = 1;
        }

    }
}
