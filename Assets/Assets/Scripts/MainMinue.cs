using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMinue : MonoBehaviour {

    public GameObject soundOff;
    public GameObject soundOn;
    bool isSoundOn = false;
    
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

    void Awake()
    {
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("192e38bc3f24f012cf6ce443cdc45db4");
    }

    public void Levels ()
	{
		SceneManager.LoadScene ("LevelsScene");	
	}
	public void GameClick ()
	{
        Amplitude.Instance.logEvent("GameScene start");
        PlayerPrefs.SetString("LevelToLoad",PlayerPrefs.GetString("LastOpenLevel", "1"));
        SceneManager.LoadScene ("GameScene");	
	}

    public void SoundClick()
    {
        isSoundOn = PlayerPrefs.GetInt("is_sound_on", 0) == 1 ? false : true;
        setSoundComponentOn(isSoundOn);
        PlayerPrefs.SetInt("is_sound_on", isSoundOn ? 1 : 0);
        if (isSoundOn)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        } else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
        }

    }

    void setSoundComponentOn(bool isSoundOn)
    {
        soundOn.SetActive(isSoundOn);
        soundOff.SetActive(!isSoundOn);
    }

}

