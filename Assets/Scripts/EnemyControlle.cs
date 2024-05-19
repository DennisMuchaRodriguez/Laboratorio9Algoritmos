using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlle : MonoBehaviour
{
    public Lista<GameObject> pathNodes;
    public Vector2 speedReference;
    public float energy;
    public float maxEnergy;
    public float restTime;
    private bool isResting;
    private float restTimer;
    private int currentIndex;
    public GameObject objective;
    private float currentWeight;

    public void InitializePath(Lista<GameObject> nodes)
    {
        pathNodes = nodes;
        currentIndex = 0;
        objective = pathNodes.Get(currentIndex);
        currentWeight = 0; 
    }

    void Start()
    {
        energy = maxEnergy;
        isResting = false;
        restTimer = 0;
        currentIndex = 0;
        if (pathNodes != null && pathNodes.Length > 0)
        {
            objective = pathNodes.Get(currentIndex);
        }
    }

    void Update()
    {
        if (isResting)
        {
            restTimer = restTimer + Time.deltaTime;
            if (restTimer >= restTime)
            {
                isResting = false;
                energy = maxEnergy;
                restTimer = 0;
            }
        }
        else
        {
            if (objective != null)
            {
                transform.position = Vector2.SmoothDamp(transform.position, objective.transform.position, ref speedReference, 0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node" && !isResting)
        {
            if (collision.gameObject == objective)
            {
                NodeController nodeController = collision.gameObject.GetComponent<NodeController>();

                currentIndex = currentIndex + 1;
                if (currentIndex >= pathNodes.Length)
                {
                    currentIndex = 0; 
                }

                (NodeController nextNode, float weight) = nodeController.SelectRandomAdjacent();
                objective = nextNode.gameObject;
                currentWeight = weight; 

                energy = energy - currentWeight; 

                if (energy <= 0)
                {
                    isResting = true;
                }
            }
        }
    }
}
