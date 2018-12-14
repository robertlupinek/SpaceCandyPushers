using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    public float speed = 1.5f;
    public GameObject myBullet; 
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    //This determines which player you are player 1 or player 2.
    //This is used to assign your inputK and inputJ values in the Awake method.
    public int whichPlayer = 1;
    private Dictionary<string, KeyCode> inputK = new Dictionary<string, KeyCode>();
    private Dictionary<string, string> inputJ = new Dictionary<string, string>();

    //Sounds
    private AudioSource source;
    public AudioClip clipBoink;



    // This function is called just one time by Unity the moment the game loads
    private void Awake()
    {
        //Set the current player's input dictionary to match what is assigned to that player.
        if ( whichPlayer == 1)
        {
            inputK = Globals.inputK1;
            inputJ = Globals.inputJ1;
        }

        //Set the current player's input dictionary to match what is assigned to that player.
        if (whichPlayer == 2)
        {
            inputK = Globals.inputK2;
            inputJ = Globals.inputJ2;
        }

        // get a reference to the SpriteRenderer component on this gameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        //Sounds
        source = GetComponent<AudioSource>();


    }


    void Update()
    { 
    
        //myText.text = printText;

        if (Input.GetKey(inputK["key_left"]) || Input.GetAxis(inputJ["Horizontal"]) < 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.left * speed * Time.deltaTime;
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            animator.SetBool("run", true);
            //transform.localEulerAngles = new Vector3(0, 180, 0);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(inputK["key_right"]) || Input.GetAxis(inputJ["Horizontal"]) > 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.right * speed * Time.deltaTime;
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            animator.SetBool("run", true);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(inputK["key_up"]) || Input.GetAxis(inputJ["Vertical"]) > 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.up * speed * Time.deltaTime;
            animator.SetBool("run", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);

        }
        if (Input.GetKey(inputK["key_down"]) || Input.GetAxis(inputJ["Vertical"]) < 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.down * speed * Time.deltaTime;
            animator.SetBool("run", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
        }
        if (!Input.GetKey(inputK["key_left"]) && !Input.GetKey(inputK["key_right"]) && !Input.GetKey(inputK["key_up"]) && !Input.GetKey(inputK["key_down"]) && Input.GetAxis(inputJ["Horizontal"]) == 0 && Input.GetAxis(inputJ["Vertical"]) == 0)
        {
            animator.SetBool("run", false);
            //Stop all horizonal velocity
            //rigidBody.velocity = new Vector2(0f, 0f);
        }
        //Constrain downward velocity
        if (rigidBody.velocity.y < -4)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -4);
        }

        //Create a block?
        if (Input.GetKey(inputK["action"] ))
        {
            //Set up instantiate
            float xoffset = 0.5f;
            if (spriteRenderer.flipX)
            {
                xoffset = -0.5f;
            }
            //Instantiate(myBullet, new Vector2(transform.position.x + xoffset, transform.position.y) , transform.rotation);
        }

    }
}
