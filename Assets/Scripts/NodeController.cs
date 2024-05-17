using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public List<NodeController> adjacentNodes;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
    public void AddAdjacentNode(NodeController node)
    {
        adjacentNodes.Add(node);
    }
    public NodeController SelecRandomAdjancent()
    {
        int index = Random.Range(0, adjacentNodes.Count);
        return adjacentNodes[index];
    }
}
