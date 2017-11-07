using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[Serializable] public class MyDictionary : SerializableDictionary<NodeModel, int> { }

public class NodeModel : MonoBehaviour
{

    //public MyDictionary verticesList;
    public List<NodeModel> Nodes;
    public List<LineModel> Lines;
    float sizeLastButton = 1.25f;
    public Boolean resizing = false;
    public Boolean selected = false;
    public AudioSource soundNode;
    public int count = 0;
    int Toggel = -1;

    //public List<LineModel> list;
    private void Start()
    {
        string indexThisNode = getTextNode(this);
        Lines.Clear();
        foreach (NodeModel node in Nodes)
        {
            string indexNode = getTextNode(node);
            LineModel line;
            try
            {
                string nameLine = "L_" + indexThisNode + "_" + indexNode;
                line = this.transform.parent.Find(nameLine).gameObject.GetComponent<LineModel>();
            }
            catch
            {
                string nameLine = "L_" + indexNode + "_" + indexThisNode;
                line = this.transform.parent.Find(nameLine).gameObject.GetComponent<LineModel>();
            }
            Lines.Add(line);
        }


    }

    private void Update()
    {
        if (resizing)
        {
            scallNodeAnmimation();
        }
    }

    private string getTextNode(NodeModel node)
    {
        GameObject GO = node.gameObject.transform.Find("Leve").gameObject.transform.Find("Number").gameObject;
        string NodeNumber = GO.GetComponent<Text>().text;
        return NodeNumber;

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

    public void clickNode()
    {
        if (soundNode != null)
        {
            if (PlayerPrefs.GetInt("is_sound_on", 1) == 1)
            {
                soundNode.Play();
            }
            else
            {
                soundNode.Stop();
            }
        }
        selected = true;
        changeColorNode(this, Color.green);
        SendMessageUpwards("AddNode", this);

    }

    private void changeColorNode(NodeModel node, Color color)
    {
        node.gameObject.GetComponent<Image>().color = color;
        GameObject innerImageGO = node.gameObject.transform.Find("Leve").gameObject;
        innerImageGO.GetComponent<Image>().color = color;

    }
}