using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMinue : MonoBehaviour {

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
}
