using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour {

    //Should the destroy animations start or not.
    public bool destroyME;
    //Score
    
    //Audio setup
    public AudioClip boxHitClip;
    public float velToVol = 0.03F;
    public float maxVol = 0.05f;
    private float timer = 0.0f;
    private _2dxFX_NewTeleportation2 script;
    private AudioSource source;
    //Set to true if you already executed the death steps
    private bool dieOnce = false;

    // Use this for initialization
    void Start () {
        script = GetComponent<_2dxFX_NewTeleportation2>();
        source = GetComponent<AudioSource>(); 
    }

    //When object collides with the "Solid" or non trigger collider
    void OnCollisionEnter2D(Collision2D other)
    {
        //Set the sound volume based on relative velocity on impact.
        //Volume is damped by the veToVol variable.
        float hitVol = other.relativeVelocity.magnitude * velToVol;
        //Only play the hit sound if the volume registers above the maxVol value.
        //This prevents unecessary or inaudible sounds from playing.
        if ( hitVol > maxVol)
        {
            source.PlayOneShot(boxHitClip, hitVol);
        }
        
    }

    // Update is called once per frame
    void Update () {


        if ( destroyME)
        {
            
            if (!dieOnce)
            {
                Globals.score += 1 * Globals.comboMulti;
                Globals.comboMulti += 1;
                Globals.comboTimer = 2f;
            }
            dieOnce = true;
            timer += Time.deltaTime;
            script.enabled = true;
            script._Fade = timer;
            if (timer >= 1)
                Destroy(gameObject);
        }
    }
}
