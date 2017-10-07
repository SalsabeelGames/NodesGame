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
        soundOn.SetActive(isSoundOn);
        soundOff.SetActive(!isSoundOn);
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
}
