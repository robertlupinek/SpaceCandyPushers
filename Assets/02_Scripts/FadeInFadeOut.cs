using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour {

    public float fadeDir = -1f;
    public float fadeTimer = 2f;
    private _2dxFX_NoiseAnimated shaderScript;
    // Use this for initialization
    void Start () {
        shaderScript = GetComponent<_2dxFX_NoiseAnimated>();

    }
	
	// Update is called once per frame
	void Update () {

        fadeTimer += Time.deltaTime * fadeDir;
        shaderScript._Alpha = fadeTimer;

        if ( fadeTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
