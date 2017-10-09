using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
	public GameObject cv;
	// Use this for initialization
	void Start () {
        if (this.gameObject.GetComponent<AudioSource>() != null)
        {
            if (PlayerPrefs.GetInt("is_sound_on", 1) == 1)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                this.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
