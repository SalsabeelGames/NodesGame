using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour {

    public List<NodeModel> nodes;
    // Use this for initialization
    void Start () {
        shortest_path("1", "6").ForEach(x => Debug.Log(x.GetComponent<Text>().text));
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public List<NodeModel> shortest_path(string start, string finish)
    {
        var previous = new Dictionary<NodeModel, NodeModel>();
        var distances = new Dictionary<NodeModel, int>();
        var nodes = new List<NodeModel>();

        List<NodeModel> path = null;
        foreach (var node in this.nodes)
        {
            if (node.GetComponent<Text>().text == start)
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

            if (smallest.GetComponent<Text>().text == finish)
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
                int destance = Int32.Parse(neighbor.Lines[i].GetComponent<Text>().text);
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