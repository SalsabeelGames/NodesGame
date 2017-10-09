using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public List<LevelButton> levelsButtons;

	void Start () {
		
		if (PlayerPrefs.GetString ("LastOpenLevel") == "") {
			PlayerPrefs.SetString ("LastOpenLevel","1");
		}

		int levelnumber = Int32.Parse (PlayerPrefs.GetString ("LastOpenLevel"));

		for(int i = 0; (i< levelnumber && i<levelsButtons.Count); i++ ){
			GameObject LeveNode = levelsButtons [i].gameObject.transform.Find ("Leve").gameObject;
			LeveNode.SetActive(true);

		}

		for(int i =levelnumber; i<levelsButtons.Count; i++ ){
			Button ButtonNode = levelsButtons[i].gameObject.GetComponent<Button> ();
			ButtonNode.enabled = false;

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

    }
	
	// Update is called once per frame
	void Update () {
		

	}

	public static void clickNode(int NodeNumber){

	}

	public void recChangColor(List<NodeModel> AvailableNodes){
		foreach (NodeModel av in AvailableNodes) {
			List<NodeModel> AvNodes = av.Nodes;
			//changeColor (av.gameObject, NotValidColor);
			//recChangColor ( av.Nodes);
		}
	}

	void changeColor(GameObject GO, Color color){
		
			GO.gameObject.GetComponent<Image> ().color = color;
			GameObject innerImageGO = GO.gameObject.transform.Find ("Leve").gameObject;
			innerImageGO.GetComponent<Image> ().color = color;

	}


}
