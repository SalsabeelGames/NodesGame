using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMinue : MonoBehaviour {

    public GameObject soundOff;
    public GameObject soundOn;
    bool isSoundOn = true;
	// Use this for initialization
	void Start () {
        if (this.gameObject.GetComponent<AudioSource>() != null)
        {
            if (PlayerPrefs.GetInt("is_sound_on", 1) == 1)
            {
                this.gameObject.GetComponent<AudioSource>().Play();
                setSoundComponentOn(true);
            }
            else
            {
                this.gameObject.GetComponent<AudioSource>().Stop();
                setSoundComponentOn(false);
            }
        }
        Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Levels ()
	{
		SceneManager.LoadScene ("LevelsScene");	
	}
	public void GameClick ()
	{
		SceneManager.LoadScene ("GameScene");	
	}

    public void SoundClick()
    {
        setSoundComponentOn(isSoundOn);
        PlayerPrefs.SetInt("is_sound_on", isSoundOn ? 1 : 0);
        if (isSoundOn)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        } else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

        isSoundOn = !isSoundOn;
    }

    void setSoundComponentOn(bool isSoundOn)
    {
        soundOn.SetActive(isSoundOn);
        soundOff.SetActive(!isSoundOn);
    }

}

