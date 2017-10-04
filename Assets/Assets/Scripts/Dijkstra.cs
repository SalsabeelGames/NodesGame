using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour {

	public string StartNodeNumber = "1";
	public string EndNodeNumber = "7"; 
    public List<NodeModel> nodes;


    void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		initLevel ();
		shortest_path("4", "3").ForEach(x => Debug.Log(x.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject.GetComponent<Text>().text));
    }

	void Update () {
		
	}

	private void initLevel() {
		NodeModel StartNode = this.nodes[0];
		foreach(NodeModel node in this.nodes){
			string nodeNumber = node.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject.GetComponent<Text> ().text;
			Button ButtonNode = node.gameObject.GetComponent<Button> ();
			ButtonNode.enabled = false;
			node.resizing = false;
			if(nodeNumber == EndNodeNumber || nodeNumber == StartNodeNumber ){
				if(nodeNumber == StartNodeNumber){
					StartNode = node;
				}
				changeColorNode (node,new Color(0.9f,0.2f,0.15f));
			}

		}
		InitChildNode (StartNode);

	}

	private void InitChildNode(NodeModel nod){
		List<NodeModel> childStartNode = nod.Nodes;
		foreach (NodeModel node in childStartNode) {
			Button ButtonNode = node.gameObject.GetComponent<Button> ();
			ButtonNode.enabled = true;
			node.resizing = true;
			changeColorNode (node,Color.gray);
		}
	}

	public void AddNode(NodeModel node){
		GameObject GO = node.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject;
		string NodeNumber = GO.GetComponent<Text> ().text;
		reConstructNodes ();
		InitChildNode (node);

	} 
	public void reConstructNodes(){
		foreach (NodeModel node in this.nodes) {
			string nodeNumber = node.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject.GetComponent<Text> ().text;
			Button ButtonNode = node.gameObject.GetComponent<Button> ();
			ButtonNode.enabled = false;
			node.resizing = false;
			if (nodeNumber == EndNodeNumber || nodeNumber == StartNodeNumber) {
				changeColorNode (node, new Color (0.9f, 0.2f, 0.15f));
			}
		}
	}

	private void changeColorNode(NodeModel node, Color color){
		node.gameObject.GetComponent<Image> ().color = color;
		GameObject innerImageGO = node.gameObject.transform.Find ("Leve").gameObject;
		innerImageGO.GetComponent<Image> ().color = color;

	}

    public List<NodeModel> shortest_path(string start, string finish)
    {
        var previous = new Dictionary<NodeModel, NodeModel>();
        var distances = new Dictionary<NodeModel, int>();
        var nodes = new List<NodeModel>();

        List<NodeModel> path = null;
        foreach (var node in this.nodes)
        {
			GameObject tempNode = node.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject;
			if (tempNode.GetComponent<Text>().text == start)
            {
                distances[node.GetComponent<NodeModel>()] = 0;
            }
            else
            {
                distances[node.GetComponent<NodeModel>()] = int.MaxValue;
            }

            nodes.Add(node.GetComponent<NodeModel>());
        }

        while (nodes.Count != 0)
        {
            nodes.Sort((x, y) => distances[x] - distances[y]);

            var smallest = nodes[0];
            nodes.Remove(smallest);
			GameObject tempSmallest = smallest.gameObject.transform.Find ("Leve").gameObject.transform.Find ("Number").gameObject;

			if (tempSmallest.GetComponent<Text>().text == finish)
            {
                path = new List<NodeModel>();
                while (previous.ContainsKey(smallest))
                {
                    path.Add(smallest);
                    smallest = previous[smallest];
                }

                break;
            }

            if (distances[smallest] == int.MaxValue)
            {
                break;
            }
            for (int i = 0; i < smallest.Nodes.Count; i++)
            {
                var neighbor = smallest.Nodes[i];
				int destance = Int32.Parse(smallest.Lines[i].gameObject.transform.Find ("Text").gameObject.GetComponent<Text>().text);
                var alt = distances[smallest] + destance;
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previous[neighbor] = smallest;
                }
            }
        }

        return path;
    }
}