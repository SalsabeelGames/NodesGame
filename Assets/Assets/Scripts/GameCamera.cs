using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCamera : MonoBehaviour
{

    public Text textTime;
    public string numberLevel;
    float timeLeft = 30.0f;
    public GameObject dialog;
    public float imageXScale;
    public float imageYScale;

    void Start()
    {

        string LevelToLoadNumber = PlayerPrefs.GetString("LevelToLoad");
        this.gameObject.transform.Find("level_" + LevelToLoadNumber).gameObject.SetActive(true);

        if (LevelToLoadNumber == "1")
        {
            timeLeft = 15.0f;
        }
        else if (LevelToLoadNumber == "2")
        {
            timeLeft = 20.0f;
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

        dialog.transform.Find("front_image").gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }


    //Text outTime = ""
    void Update()
    {
        timeLeft -= Time.deltaTime;
        textTime.text = timeLeft.ToString("0.00").Replace(".", ":");
        if (timeLeft < 0)
        {
            //TODO:show fail dialog	
        }

        if (dialog.activeSelf)
        {
            if (imageXScale <= 0.37f)
            {
                imageXScale += Time.deltaTime * 0.90f;
            }

            if (imageYScale <= 0.82f)
            {
                imageYScale += Time.deltaTime * 0.90f;
            }
            dialog.transform.Find("front_image").gameObject.transform.localScale = new Vector3(imageXScale, imageYScale, 0);
        }
    }

    void setStar(int starNumber, bool isOn)
    {
        //name of star images should start from number 1.
        if (starNumber > 0)
        {
            GameObject star = this.gameObject.transform.Find("front_image").gameObject.transform.Find("star_" + starNumber).gameObject;
            star.SetActive(isOn);
        }
    }

    public void GoToNextLevel()
    {
        string LevelToLoadNumber = PlayerPrefs.GetString("LevelToLoad");
        if (LevelToLoadNumber == numberLevel)
        {

        }
        else
        {
            int nextLevelNumber = (Int32.Parse(LevelToLoadNumber) + 1);
            PlayerPrefs.SetString("LevelToLoad", "" + nextLevelNumber);
            int LastOpenLevel = Int32.Parse(PlayerPrefs.GetString("LastOpenLevel"));
            if (LastOpenLevel < nextLevelNumber)
            {
                PlayerPrefs.SetString("LastOpenLevel", "" + nextLevelNumber);
            }
            SceneManager.LoadScene("GameScene");
        }
    }

    public void onRestartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
	public void GoToMainScene ()
	{
		SceneManager.LoadScene ("main");	
	}
}
