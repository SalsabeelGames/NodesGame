using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {
	public GameObject cv;
	// Use this for initialization
	void Start () {

		string LevelToLoadNumber = PlayerPrefs.GetString ("LevelToLoad");
		cv.gameObject.transform.Find ("level_" + LevelToLoadNumber).gameObject.SetActive (true);

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
	
	float timeLeft = 30.0f;
	//Text outTime = ""
	void Update()
	{
		timeLeft -= Time.deltaTime;
		Debug.Log (timeLeft.ToString("0.00").Replace(".",":"));
		if(timeLeft < 0)
		{
			
		}
	}
}
