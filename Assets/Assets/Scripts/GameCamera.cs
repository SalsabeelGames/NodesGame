using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCamera : MonoBehaviour {
	public GameObject cv;
	public Text textTime;
	public string numberLevel;
	float timeLeft = 30.0f;

	void Start () {
		
		string LevelToLoadNumber = PlayerPrefs.GetString ("LevelToLoad");
		cv.gameObject.transform.Find ("level_" + LevelToLoadNumber).gameObject.SetActive (true);


		if(LevelToLoadNumber == "1"){
			timeLeft = 30.0f;
		}else if(LevelToLoadNumber == "2"){
			timeLeft = 40.0f;
		}

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
	

	//Text outTime = ""
	void Update()
	{
		timeLeft -= Time.deltaTime;
		textTime.text = timeLeft.ToString ("0.00").Replace (".", ":");
		if(timeLeft < 0)
		{
			
		}
	}

	public void GoToNextLevel(){
		string LevelToLoadNumber = PlayerPrefs.GetString ("LevelToLoad");
		if (LevelToLoadNumber == numberLevel) {

		}else{
			int nextLevelNumber = (Int32.Parse (LevelToLoadNumber) + 1);
			PlayerPrefs.SetString ("LevelToLoad",""+nextLevelNumber);
			int LastOpenLevel = Int32.Parse (PlayerPrefs.GetString ("LastOpenLevel"));
			if(LastOpenLevel < nextLevelNumber){
				PlayerPrefs.SetString ("LastOpenLevel","" +nextLevelNumber );
			}
			SceneManager.LoadScene("GameScene");
		}
	}
}
