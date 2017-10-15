using Facebook.Unity;
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
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            var perms = new List<string>() { "public_profile", "email", "user_friends" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
        }
        else
        {
            Debug.Log("User cancelled login");
        }
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

