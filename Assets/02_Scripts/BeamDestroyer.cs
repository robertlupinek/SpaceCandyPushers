using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDestroyer : MonoBehaviour {


    private AudioSource source;
    public AudioClip phaseOutClip;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }



    //When object collides with the Trigger collider ( non solid )
    void OnTriggerEnter2D(Collider2D other)
    {
        //Colliding with Collider2D when this Collider2D is set as a trigger

        if (other.gameObject.tag == "Destroyable")
        {
            //Destroy(other.gameObject);
            MoveableBlock script;
            script = other.GetComponent<MoveableBlock>();
            //Play sound only if destroyME hasn't been set yet :)
            if (!script.destroyME)
            {
                source.pitch = 1 + GameControllerScript.comboMulti * 0.1f;
                source.PlayOneShot(phaseOutClip, 0.8f);
            }
            script.destroyME = true;
        }

    }

    //When object collides with the "Solid" or non trigger collider
    void OnCollisionEnter2D(Collision2D other)
    {
        //Colliding with Collider2D when this Collider2D is NOT set as a trigger
    }

    // Update is called once per frame
    void Update ()
    {

    }
          
}
