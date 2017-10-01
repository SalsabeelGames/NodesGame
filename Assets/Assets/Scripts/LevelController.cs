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
		recChangColor (AvailableNodes);
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		

	}

	public static void clickNode(int NodeNumber){

	}

	public void recChangColor(List<NodeModel> AvailableNodes){
		Debug.Log (AvailableNodes.Count);
		foreach (NodeModel av in AvailableNodes) {
			Debug.Log ("11");
			List<NodeModel> AvNodes = av.Nodes;
			changeColor (av.gameObject, NotValidColor);
			recChangColor ( av.Nodes);
			Debug.Log ("22");
		}
		Debug.Log ("33");
	}

	void changeColor(GameObject GO, Color color){
		
			GO.gameObject.GetComponent<Image> ().color = color;
			GameObject innerImageGO = GO.gameObject.transform.FindChild ("Leve").gameObject;
			innerImageGO.GetComponent<Image> ().color = color;

	}


}
