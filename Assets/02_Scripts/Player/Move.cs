using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    public float speed = 1.5f;
    public GameObject myBullet; 
    //Allow player to switch animations
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
    public AudioClip phaseOutClip;

    //Speed burst
    private int speedDir = 1;
    private float speedTimer = 0f;
    private float speedBurst = 0;
    private float speedBurstReloadTimer = 0f;
    public _2dxFX_Hologram3 effectShader;



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

        //Shader
        effectShader = GetComponent <_2dxFX_Hologram3>();
    }


    void Update()
    {

        if ( speedBurstReloadTimer > 0 )
        {
            speedBurstReloadTimer -= Time.deltaTime;

        } 

        //Increase speed if hte speed burst button is pressed;
        if ( speedTimer > 0)
        {
            speedBurst = 2;
            speedTimer -= Time.deltaTime;
        }
        else
        {
            //effectShader.enabled = false;
            speedBurst = 1;
        }
        
        //Move based on the keyboard key pressed
        if (Input.GetKey(inputK["key_left"]) || Input.GetAxis(inputJ["Horizontal"]) < 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.left * speed * Time.deltaTime;
            rigidBody.velocity = new Vector2(-speed*speedBurst, rigidBody.velocity.y);
            animator.SetBool("run", true);
            //transform.localEulerAngles = new Vector3(0, 180, 0);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(inputK["key_right"]) || Input.GetAxis(inputJ["Horizontal"]) > 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.right * speed * Time.deltaTime;
            rigidBody.velocity = new Vector2(speed * speedBurst, rigidBody.velocity.y);
            animator.SetBool("run", true);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(inputK["key_up"]) || Input.GetAxis(inputJ["Vertical"]) > 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.up * speed * Time.deltaTime;
            animator.SetBool("run", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed * speedBurst);

        }
        if (Input.GetKey(inputK["key_down"]) || Input.GetAxis(inputJ["Vertical"]) < 0)
        {
            //Tranform postion vs setting velocity.
            //transform.position += Vector3.down * speed * Time.deltaTime;
            animator.SetBool("run", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed * speedBurst);
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
        if (Input.GetKey(inputK["action"]) && speedBurstReloadTimer <= 0 )
        {
            speedBurstReloadTimer = 1;
            //effectShader.enabled = true;
            speedTimer = 0.4f;
            //Set up instantiate
            float xoffset = 0.5f;
            if (spriteRenderer.flipX)
            {
                xoffset = -0.5f;
            }
            //Instantiate(myBullet, new Vector2(transform.position.x + xoffset, transform.position.y) , transform.rotation);
        }

    }

    //When object collides with the "Solid" or non trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        //Colliding with Collider2D when this Collider2D is NOT set as a trigger
        if (other.gameObject.tag == "Beam" && !effectShader.enabled)
        {
            effectShader.enabled = true;
            source.pitch = 1;
            source.PlayOneShot(phaseOutClip, 0.8f);
        }
    }

    //When object collides with the Trigger collider ( non solid )
    void OnCollisionEnter2D(Collision2D other)
    {
        //Colliding with Collider2D when this Collider2D is set as a trigger

        if (other.gameObject.tag == "Destroyable" && effectShader.enabled)
        {
            //Destroy(other.gameObject);
            MoveableBlock script;
            script = other.gameObject.GetComponent<MoveableBlock>();
            //Play sound only if destroyME hasn't been set yet :)
            if (!script.destroyME)
            {
                source.pitch = 1 + Globals.comboMulti * 0.1f;
                source.PlayOneShot(phaseOutClip, 0.8f);
            }
            script.destroyME = true;

            effectShader.enabled = false;
        }
    }
}
