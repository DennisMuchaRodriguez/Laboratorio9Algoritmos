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
    public float visionRange = 5f; 
    public float chaseEnergyDrain = 1f; 
    private bool isResting;
    private float restTimer;
    private int currentIndex;
    public GameObject objective;
    private float currentWeight;
    private GameObject player; 
    private bool isChasing;
    private Vector2 originalPosition;

    public void InitializePath(Lista<GameObject> nodes)
    {
        pathNodes = nodes;
        currentIndex = 0;
        objective = pathNodes.Get(currentIndex);
        currentWeight = 0;
        originalPosition = transform.position; 
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
        player = GameObject.FindGameObjectWithTag("Player");
        isChasing = false;
    }

    void Update()
    {
        if (isResting)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= restTime)
            {
                isResting = false;
                energy = maxEnergy;
                restTimer = 0;
            }
        }
        else
        {
            if (isChasing)
            {
                if (player != null)
                {
                    transform.position = Vector2.SmoothDamp(transform.position, player.transform.position, ref speedReference, 0.5f);
                    energy -= chaseEnergyDrain * Time.deltaTime;

                    if (energy <= 0)
                    {
                        isResting = true;
                        isChasing = false;
                    }

                
                    if (Vector2.Distance(transform.position, player.transform.position) > visionRange)
                    {
                        isChasing = false;
                        objective = pathNodes.Get(currentIndex);
                    }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node" && !isResting && !isChasing)
        {
            if (collision.gameObject == objective)
            {
                NodeController nodeController = collision.gameObject.GetComponent<NodeController>();

                currentIndex = (currentIndex + 1) % pathNodes.Length;

                (NodeController nextNode, float weight) = nodeController.SelectRandomAdjacent();
                objective = nextNode.gameObject;
                currentWeight = weight;

                energy -= currentWeight;

                if (energy <= 0)
                {
                    isResting = true;
                }
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            isChasing = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
