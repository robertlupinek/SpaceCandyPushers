using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBlock : MonoBehaviour {
   
    //Initial horizontal direction to move ( negative for reverse direction )
    public float hDir = 1f;
    //Initial horizontal direction to move ( negative for reverse direction )
    public float vDir = 0f;
    public float slideTimerMax = 2f;
    //Time for changing direction
    private float slideTimer = 2f;
    public float hSpeed = 1f;
    public float vSpeed = 0f;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        slideTimer = slideTimerMax;
	}
	
	// Update is called once per frame
	void Update () {

        rigidBody.velocity = new Vector2(hSpeed * hDir , vSpeed * vDir);

        slideTimer -= Time.deltaTime;

        if ( slideTimer <= 0)
        {
            vDir = vDir * -1;
            hDir = hDir * -1;
            slideTimer = slideTimerMax;
        }

    }
}
