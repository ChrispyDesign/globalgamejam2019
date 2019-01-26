using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Music : MonoBehaviour {

	
	void Start () {
        SoundManager.instance.audio[0].volume = Mathf.Lerp(0.01f, 0.5f, 3f);

        SoundManager.instance.Play("Background_Music");
    }
	
	
}
