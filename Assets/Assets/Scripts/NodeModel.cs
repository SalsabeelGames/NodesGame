using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[Serializable] public class MyDictionary : SerializableDictionary<NodeModel, int> { }

public class NodeModel : MonoBehaviour {

    //public MyDictionary verticesList;
	public List<NodeModel> Nodes;
	public List<LineModel> Lines;
    float sizeLastButton = 1.25f;
    int Toggel = -1;
    private void Start()
    {
		char nodeName;
		Image image;
    }

    private void Update()
    {
        scallNodeAnmimation();
    }

    private void scallNodeAnmimation()
    {
        if (sizeLastButton > 1.5f)
        {
            sizeLastButton = 1.5f;
            Toggel *= -1;
        }
        else if (sizeLastButton < 1.3f)
        {
            sizeLastButton = 1.3f;
            Toggel *= -1;
        }
        sizeLastButton += Time.deltaTime * Toggel * 0.1f;
        this.gameObject.transform.localScale = new Vector3(sizeLastButton, sizeLastButton, 0);
    }

	public void clickNode(){
		GameObject GO = this.gameObject.transform.FindChild ("Leve").gameObject.transform.FindChild ("Number").gameObject;
		Debug.Log(GO.GetComponent<Text> ().text);
		changeColor (this.gameObject, Color.green);

	}

	void changeColor(GameObject GO, Color color){

		GO.gameObject.GetComponent<Image> ().color = color;
		GameObject innerImageGO = GO.gameObject.transform.FindChild ("Leve").gameObject;
		innerImageGO.GetComponent<Image> ().color = color;

	}
}