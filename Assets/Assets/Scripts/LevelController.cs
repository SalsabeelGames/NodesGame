using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public GameObject cv;
	public NodeModel StartNode;
	public Color NotValidColor; 
	void Start () {
		List<NodeModel> AvailableNodes= StartNode.Nodes;
		//recChangColor (AvailableNodes);

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
