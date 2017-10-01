using System;

[Serializable]
public class Vertix  {
    public NodeModel node;
    public int distance;

    public Vertix (NodeModel node1, int distance1)
    {
        this.node = node1;
        this.distance = distance1;
    }
}
