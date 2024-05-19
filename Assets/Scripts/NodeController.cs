using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeController : MonoBehaviour
{
    public Lista<(NodeController node, float weight)> adjacentNodes;

    void Awake() 
    {
        adjacentNodes = new Lista<(NodeController, float)>();
    }

    void Update()
    {

    }

    public void AddAdjacentNode(NodeController node, float weight)
    {
        adjacentNodes.Add((node, weight));

    }

    public (NodeController, float)  SelectRandomAdjacent()
    {
        int index = Random.Range(0, adjacentNodes.Length);
        return adjacentNodes.Get(index);
    }
}
