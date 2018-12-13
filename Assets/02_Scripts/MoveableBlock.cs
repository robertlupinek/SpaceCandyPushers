using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour {

    //Should the destroy animations start or not.
    public bool destroyME;
    //Score 
    //Sound setup
    public AudioClip clipBoxHit;
    public float velToVol = 0.03F;
    public float maxVol = 0.05f;
    private float timer = 0.0f;
    private _2dxFX_NewTeleportation2 script;
    private AudioSource source;
    //Set to true if you already executed the death steps
    private bool dieOnce = false;

    private string printText = "A";

    // Use this for initialization
    void Start () {
        script = GetComponent<_2dxFX_NewTeleportation2>();
        source = GetComponent<AudioSource>();
        
    }

    //When object collides with the "Solid" or non trigger collider
    void OnCollisionEnter2D(Collision2D other)
    {
        //Play sound if velocity is high
        float hitVol = other.relativeVelocity.magnitude * velToVol;
        if ( hitVol > maxVol)
        {
            source.PlayOneShot(clipBoxHit, hitVol);
        }
        
    }

    // Update is called once per frame
    void Update () {


        if ( destroyME)
        {
            
            if (!dieOnce)
            {
                GameControllerScript.score += 1 * GameControllerScript.comboMulti;
                GameControllerScript.comboMulti += 1;
                GameControllerScript.comboTimer = 2f;
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
