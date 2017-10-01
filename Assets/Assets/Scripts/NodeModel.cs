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
    private void Start()
    {
		char nodeName;
		Image image;
    }

    private void Update()
    {
        
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