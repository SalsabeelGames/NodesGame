using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCamera : MonoBehaviour
{

    public Text textTime;
    public int numberLevel;
    float timeLeft = 30.0f;
    public GameObject dialog;
    public float imageXScale;
    public float imageYScale;
    public AudioSource sound;
    private bool countTime = true;
    
    void Start()
    {
        PlayerPrefs.SetInt("countLevels", numberLevel);
        string LevelToLoadNumber = PlayerPrefs.GetString("LevelToLoad", "1");
        this.gameObject.transform.Find("level_" + LevelToLoadNumber).gameObject.SetActive(true);

        if (LevelToLoadNumber == "1")
        {
            timeLeft = 15.0f;
        }
        else if (LevelToLoadNumber == "2")
        {
            timeLeft = 20.0f;
        }

        if (sound != null)
        {
            if (PlayerPrefs.GetInt("is_sound_on", 1) == 1)
            {
                sound.Play();
            }
            else
            {
                sound.Stop();
            }
        }

        dialog.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }


    //Text outTime = ""
    void Update()
    {
        if (countTime)
        {
            timeLeft -= Time.deltaTime;
            textTime.text = timeLeft.ToString("0.0").Replace(".", ":");
        }
        if (timeLeft < 0)
        {
            //TODO:show fail dialog	
            viewFailScreen();
        }

        if (dialog.activeSelf)
        {
            if (imageXScale <= 1f)
            {
                imageXScale += Time.deltaTime * 1.90f;
            }

            if (imageYScale <= 1f)
            {
                imageYScale += Time.deltaTime * 1.90f;
            }
            dialog.transform.localScale = new Vector3(imageXScale, imageYScale, 0);
        }
    }

    public void GoToNextLevel()
    {
        int LevelToLoadNumber = Int32.Parse(PlayerPrefs.GetString("LevelToLoad"));
        if (LevelToLoadNumber == numberLevel)
        {

        }
        else
        {
            int nextLevelNumber = LevelToLoadNumber + 1;
            PlayerPrefs.SetString("LevelToLoad", "" + nextLevelNumber);
            int LastOpenLevel = Int32.Parse(PlayerPrefs.GetString("LastOpenLevel"));
            if (LastOpenLevel < nextLevelNumber)
            {
                PlayerPrefs.SetString("LastOpenLevel", "" + nextLevelNumber);
            }
            SceneManager.LoadScene("GameScene");
        }
    }

    private void viewFailScreen()
    {
        dialog.transform.Find("Label").GetComponent<Text>().text = "Try Again";
        dialog.transform.Find("next").GetComponent<Button>().interactable = false;
        dialog.SetActive(true);
        countTime = false;
        timeLeft = 1;
    }

    private void viewWinScreen(int countStars)
    {
        dialog.transform.Find("Label").GetComponent<Text>().text = "Congratulations";
        dialog.transform.Find("next").GetComponent<Button>().interactable = true;
        dialog.SetActive(true);
        setStars(countStars);
        countTime = false;
    }

    private void setStars(int count)
    {
        if (count > 0)
        {
            for (int i = 1; i <= count; i++)
            {
                setStar(i, true);
            }
        }
    }
    private void setStar(int starNumber, bool isOn)
    {
        //name of star images should start from number 1.
        if (starNumber > 0)
        {
            GameObject star = dialog.transform.Find("star_" + starNumber).gameObject;
            star.SetActive(isOn);
        }
    }

    public void onRestartClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GoToMainScene()
    {
        SceneManager.LoadScene("main");
    }


}
