using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMinue : MonoBehaviour {

    public GameObject soundOff;
    public GameObject soundOn;
    
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
        SceneManager.LoadScene ("GameScene");	
	}

    public void SoundClick()
    {
        if (PlayerPrefs.GetInt("is_sound_on") == 0)
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            setSoundComponentOn(true);
        } else
        {
            this.gameObject.GetComponent<AudioSource>().Stop();
            setSoundComponentOn(false);
        }

    }

    void setSoundComponentOn(bool isSoundOn)
    {
        PlayerPrefs.SetInt("is_sound_on", isSoundOn ? 1 : 0);
        soundOn.SetActive(isSoundOn);
        soundOff.SetActive(!isSoundOn);
    }

}

