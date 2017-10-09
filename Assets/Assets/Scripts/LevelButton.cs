using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class LevelButton : MonoBehaviour {

	public AudioSource source;

	void Start () {
		
	}
	

	void Update () {
		
	}

	public void clickLevel(){
		Debug.Log (getTextNode ());
		int lastLevel = Int32.Parse (PlayerPrefs.GetString ("LevelToLoad"));
		int levelNumber = Int32.Parse (getTextNode());
		if (lastLevel < levelNumber) {
			PlayerPrefs.SetString ("LevelToLoad", getTextNode ());
		}
		Debug.Log (getTextNode ());
		Debug.Log (PlayerPrefs.GetString ("LevelToLoad"));

		UnityEngine.SceneManagement.SceneManager.LoadScene ("GameScene");
	}

	private string getTextNode(){
		GameObject GO = this.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject;
		string NodeNumber = GO.GetComponent<Text> ().text;
		return NodeNumber;

	}
}
