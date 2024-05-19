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
        SelectInitialNode();
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
                for (int j = 0; j < currentNodeConections.Length; j += 2)
                {
                    int currentIndex = int.Parse(currentNodeConections[j]);
                    float weight = float.Parse(currentNodeConections[j + 1]);

                    if (AllNodes.Get(i) != null && AllNodes.Get(currentIndex) != null)
                    {
                        AllNodes.Get(i).GetComponent<NodeController>().AddAdjacentNode(AllNodes.Get(currentIndex).GetComponent<NodeController>(), weight);
                    }
                }
            }
        }
    }

    void SelectInitialNode()
    {
        if (AllNodes.Length > 0)
        {
            int index = Random.Range(0, AllNodes.Length);
            enemy.objective = AllNodes.Get(index);
        }
    }

    void Update()
    {

    }
}
