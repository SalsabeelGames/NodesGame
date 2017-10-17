using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class GameCamera : MonoBehaviour
{
    public GameObject friendScorePanel;
    public GameObject friendScrollList;
    public Text textTime;
    public int countLevel;
    float timeLeft = 30.0f;
    public GameObject dialog;
    public float imageXScale;
    public float imageYScale;
    public AudioSource sound;
    private bool countTime = true;
    
    void Start()
    {
        PlayerPrefs.SetInt("countLevels", countLevel);
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
        if (LevelToLoadNumber == countLevel)
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

        setScore();

        //share result on facebook
        FB.API("/app/scores?fields=score,user.limit(3)", HttpMethod.GET, handleScoresResponse);
    }

    void setScore()
    {
        int totalScore = 0;
        for (int i = 0; i < countLevel; i++)
        {
            totalScore += PlayerPrefs.GetInt("Score_" + i, 0);
        }

        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = totalScore.ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
        {
            Debug.Log("score submitted sucessfully " + result.RawResult);
        }, scoreData);
    }

    void handleScoresResponse(IGraphResult result)
    {
        UnityEngine.Debug.Log(result.RawResult);

        IDictionary<String, object> data = result.ResultDictionary;
        List<object> scoreList = (List<object>) data["data"];

        foreach(object obj in scoreList)
        {
            var entryObj = (Dictionary<string, object>) obj;
            var userObj = (Dictionary<string, object>) entryObj["user"];
            Debug.Log(userObj["name"].ToString() + " , " + entryObj["score"].ToString());

            GameObject scorePanel;
            scorePanel = Instantiate(friendScorePanel) as GameObject;
            scorePanel.transform.SetParent(friendScrollList.transform, false);

            Transform fName = scorePanel.transform.Find("friendName");
            Transform fScore = scorePanel.transform.Find("friendScore");
            Transform fAvatar = scorePanel.transform.Find("friendAvatar");

            Text fNameText = fName.GetComponent<Text>();
            Text fScoreText = fScore.GetComponent<Text>();
            Image fAvatarImage = fAvatar.GetComponent<Image>();

            fNameText.text = userObj["name"].ToString();
            fScoreText.text = entryObj["score"].ToString();

            FB.API(userObj["id"].ToString() + "/picture?width=120&height=120", HttpMethod.GET, delegate (IGraphResult graphResult)
            {
                if(graphResult.Error != null)
                {
                    Debug.Log(graphResult.RawResult);
                }
                else
                {
                    Sprite sp = Sprite.Create(graphResult.Texture, new Rect(0, 0, 120, 120), new Vector2(0, 0));
                    fAvatarImage.sprite = sp;
                }
            });
        }
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
