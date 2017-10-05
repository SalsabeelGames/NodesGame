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
	public Boolean resizing = false;
	public Boolean selected = false;


    int Toggel = -1;
    private void Start()
    {
		char nodeName;
		Image image;
    }

    private void Update()
    {
		if (resizing) {
			scallNodeAnmimation ();
		}
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
        sizeLastButton += Time.deltaTime * Toggel * 0.15f;
        this.gameObject.transform.localScale = new Vector3(sizeLastButton, sizeLastButton, 0);
    }

	public void clickNode(){
		selected = true;
		changeColorNode (this, Color.green);
		SendMessageUpwards ("AddNode", this);

	}
		
	private void changeColorNode(NodeModel node, Color color){
		node.gameObject.GetComponent<Image> ().color = color;
		GameObject innerImageGO = node.gameObject.transform.Find ("Leve").gameObject;
		innerImageGO.GetComponent<Image> ().color = color;

	}
}