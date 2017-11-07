using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public List<LevelButton> levelsButtons;

    void Start()
    {
        int levelnumber = Int32.Parse(PlayerPrefs.GetString("LastOpenLevel", "1"));

        for (int i = 0; (i < levelnumber && i < levelsButtons.Count); i++)
        {
            GameObject LeveNode = levelsButtons[i].gameObject.transform.Find("Leve").gameObject;
            LeveNode.SetActive(true);
        }

        for (int i = levelnumber; i < levelsButtons.Count; i++)
        {
            Button ButtonNode = levelsButtons[i].gameObject.GetComponent<Button>();
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void changeColor(GameObject GO, Color color)
    {
        GO.gameObject.GetComponent<Image>().color = color;
        GameObject innerImageGO = GO.gameObject.transform.Find("Leve").gameObject;
        innerImageGO.GetComponent<Image>().color = color;

    }

	public void GoToMainScene ()
	{
		SceneManager.LoadScene ("main");	
	}

}
