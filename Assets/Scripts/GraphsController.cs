using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphsController : MonoBehaviour
{
    public GameObject nodePrefabs;
    public TextAsset nodePositionTxt;
    public string[] arrayNodePositions;
    public string[] currentNodePositions;
    public Lista<GameObject> AllNodes; 
    public TextAsset nodeConectionsTxt;
    public string[] arrayNodeConections;
    public string[] currentNodeConections;
    public EnemyControlle enemy;

    void Start()
    {
        AllNodes = new Lista<GameObject>(); 
        CreateNode();
        CreateConnections();

        SetupEnemyPath();
    }

    void CreateNode()
    {
        if (nodePositionTxt != null)
        {
            arrayNodePositions = nodePositionTxt.text.Split('\n');
            for (int i = 0; i < arrayNodePositions.Length; i++)
            {
                currentNodePositions = arrayNodePositions[i].Split(',');
                Vector2 position = new Vector2(float.Parse(currentNodePositions[0]), float.Parse(currentNodePositions[1]));
                GameObject tmp = Instantiate(nodePrefabs, position, transform.rotation);
                AllNodes.Add(tmp);
            }
        }
    }

    void CreateConnections()
    {
        if (nodeConectionsTxt != null)
        {
            arrayNodeConections = nodeConectionsTxt.text.Split('\n');
            for (int i = 0; i < arrayNodeConections.Length; i++)
            {
                currentNodeConections = arrayNodeConections[i].Split(',');
                int fromNodeIndex = int.Parse(currentNodeConections[0]);
                int toNodeIndex = int.Parse(currentNodeConections[1]);
                float weight = float.Parse(currentNodeConections[2]);

                if (AllNodes.Get(fromNodeIndex) != null && AllNodes.Get(toNodeIndex) != null)
                {
                    AllNodes.Get(fromNodeIndex).GetComponent<NodeController>().AddAdjacentNode(AllNodes.Get(toNodeIndex).GetComponent<NodeController>(), weight);
                }
            }
        }
    }


    
    void SetupEnemyPath()
    {

        enemy.InitializePath(AllNodes);
    }
    void Update()
    {

    }
}
