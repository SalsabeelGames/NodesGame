using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour
{

    public string StartNodeNumber = "1";
    public string EndNodeNumber = "7";
    public List<NodeModel> nodes;
    public GameObject level;
    NodeModel StartNode;
    NodeModel EndNode;
    public List<NodeModel> clickedNodes;
    private int shortest = 999;
    void Start()
    {
        NodeModel StartNode = this.nodes[0];
        NodeModel EndNode = this.nodes[0];
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        initLevel();
        getScore();
        //shortest_path(StartNodeNumber, EndNodeNumber).ForEach(x => Debug.Log(x.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject.GetComponent<Text>().text));
    }

    void Update()
    {

    }

    private void initLevel()
    {

        foreach (NodeModel node in this.nodes)
        {
            string nodeNumber = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject.GetComponent<Text>().text;
            Button ButtonNode = node.gameObject.GetComponent<Button>();
            ButtonNode.enabled = false;
            node.resizing = false;

            if (nodeNumber == EndNodeNumber || nodeNumber == StartNodeNumber)
            {
                if (nodeNumber == StartNodeNumber)
                {
                    StartNode = node;
                    StartNode.selected = true;
                }
                if (nodeNumber == EndNodeNumber)
                {
                    EndNode = node;
                }
                changeColorNode(node, new Color(0.9f, 0.2f, 0.15f));
            }
            else
            {

                changeColorNode(node, Color.gray);
            }

        }
        InitChildNode(StartNode);
        clickedNodes.Add(StartNode);
    }

    private void InitChildNode(NodeModel nod)
    {
        if (nod.Equals(EndNode))
        {
            int score = getScore();
            if (score == 0)
            {
                SendMessageUpwards("viewFailScreen", score);
            }
            else
            {
                SendMessageUpwards("viewWinScreen", score);
            }
            return;
        }
        List<NodeModel> childStartNode = nod.Nodes;
        foreach (NodeModel node in childStartNode)
        {
            Button ButtonNode = node.gameObject.GetComponent<Button>();
            ButtonNode.enabled = true;
            node.resizing = true;
            if (node.Equals(StartNode) || node.Equals(EndNode) || node.selected)
            {
                continue;
            }
            changeColorNode(node, Color.gray);
        }
    }

    private int getScore()
    {
        shortest_path(StartNodeNumber, EndNodeNumber);
        int lengthPlayer = getlengthPlayerPath();
        if (lengthPlayer == shortest)
        {
            return 3;
        }
        else if ((shortest + 5) > lengthPlayer)
        {
            return 2;
        }
        else if ((shortest + 10) > lengthPlayer)
        {
            return 1;
        }
        return 0;
    }

    private int getlengthPlayerPath()
    {
        int length = 0;
        for (int i = 1; i < clickedNodes.Count; i++)
        {
            string nodeOneNumber = getTextNode(clickedNodes[i - 1]);
            string nodetwoNumber = getTextNode(clickedNodes[i]);
            GameObject line;
            try
            {
                string nameLine = "L_" + nodeOneNumber + "_" + nodetwoNumber;
                line = this.transform.Find("BG").gameObject.transform.Find(nameLine).gameObject;
            }
            catch
            {
                string nameLine = "L_" + nodetwoNumber + "_" + nodeOneNumber;
                line = this.transform.Find("BG").gameObject.transform.Find(nameLine).gameObject;
            }
            int destance = Int32.Parse(line.transform.Find("Text").gameObject.GetComponent<Text>().text);
            length += destance;
        }
        return length;
    }

    private string getTextNode(NodeModel node)
    {
        GameObject GO = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject;
        string NodeNumber = GO.GetComponent<Text>().text;
        return NodeNumber;

    }

    public void AddNode(NodeModel node)
    {
        int count = clickedNodes.Count;
        if (count > 1)
        {
            if (getTextNode(node) == getTextNode(clickedNodes[count - 2]))
            {
                string nodeOne = getTextNode(clickedNodes[count - 2]);
                string nodetwo = getTextNode(clickedNodes[count - 1]);

                foreach (LineModel line in node.Lines)
                {
                    string nameLine = line.gameObject.transform.name;
                    if (nameLine == "L_" + nodeOne + "_" + nodetwo || nameLine == "L_" + nodetwo + "_" + nodeOne)
                    {
                        line.gameObject.GetComponent<Image>().color = Color.gray;
                    }

                }
                if (clickedNodes[count - 1].count == 0)
                {
                    clickedNodes[count - 1].selected = false;
                    changeColorNode(clickedNodes[count - 1], Color.gray);
                }
                clickedNodes.RemoveAt(count - 1);
            }
            else
            {
                if (node.selected)
                {
                    node.count++;
                }
                clickedNodes.Add(node);
                count = clickedNodes.Count;
                string nodeOne = getTextNode(clickedNodes[count - 2]);
                string nodetwo = getTextNode(clickedNodes[count - 1]);
                foreach (LineModel line in node.Lines)
                {
                    string nameLine = line.gameObject.transform.name;
                    if (nameLine == "L_" + nodeOne + "_" + nodetwo || nameLine == "L_" + nodetwo + "_" + nodeOne)
                    {
                        line.gameObject.GetComponent<Image>().color = Color.green;
                    }

                }
            }

        }
        else
        {
            clickedNodes.Add(node);
            count = clickedNodes.Count;
            string nodeOne = getTextNode(clickedNodes[count - 2]);
            string nodetwo = getTextNode(clickedNodes[count - 1]);
            foreach (LineModel line in node.Lines)
            {
                string nameLine = line.gameObject.transform.name;
                if (nameLine == "L_" + nodeOne + "_" + nodetwo || nameLine == "L_" + nodetwo + "_" + nodeOne)
                {
                    line.gameObject.GetComponent<Image>().color = Color.green;
                }

            }

        }


        GameObject GO = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject;
        string NodeNumber = GO.GetComponent<Text>().text;
        reConstructNodes();
        InitChildNode(node);

    }

    public void reConstructNodes()
    {
        foreach (NodeModel node in this.nodes)
        {
            string nodeNumber = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject.GetComponent<Text>().text;
            Button ButtonNode = node.gameObject.GetComponent<Button>();
            ButtonNode.enabled = false;
            node.resizing = false;
            if (nodeNumber == EndNodeNumber || nodeNumber == StartNodeNumber)
            {
                changeColorNode(node, new Color(0.9f, 0.2f, 0.15f));
            }
        }
    }

    private void changeColorNode(NodeModel node, Color color)
    {
        node.gameObject.GetComponent<Image>().color = color;
        GameObject innerImageGO = node.gameObject.transform.Find("Leve").gameObject;
        innerImageGO.GetComponent<Image>().color = color;

    }

    public List<NodeModel> shortest_path(string start, string finish)
    {
        var previous = new Dictionary<NodeModel, NodeModel>();
        var distances = new Dictionary<NodeModel, int>();
        var nodes = new List<NodeModel>();

        List<NodeModel> path = null;
        foreach (var node in this.nodes)
        {
            GameObject tempNode = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject;
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
            GameObject tempSmallest = smallest.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject;

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
                int destance = Int32.Parse(smallest.Lines[i].gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text);
                var alt = distances[smallest] + destance;
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previous[neighbor] = smallest;
                }
            }

        }
        this.shortest = new List<int>(distances.Values)[distances.Count - 1];
        return path;
    }
}