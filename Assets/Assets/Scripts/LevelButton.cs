using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	public AudioSource source;

	void Start () {
		
	}
	

	void Update () {
		
	}

	public void clickLevel(){
		Debug.Log (getTextNode ());

		PlayerPrefs.SetString ("LevelToLoad", getTextNode());
		UnityEngine.SceneManagement.SceneManager.LoadScene ("GameScene");
	}

	private string getTextNode(){
		GameObject GO = this.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject;
		string NodeNumber = GO.GetComponent<Text> ().text;
		return NodeNumber;

	}
}
