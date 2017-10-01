using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clickLevel(int levelNumber){


		PlayerPrefs.SetInt ("LevelToLoad", levelNumber);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("main");
	}
}
