using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlle : MonoBehaviour
{
    public GameObject objective;
    public Vector2 speedReference;
    public float energy;
    public float maxEnergy;
    public float restTime;
    private bool isResting;
    private float restTimer; 
    void Start()
    {
        energy = maxEnergy;
        isResting = false;
        restTimer = 0;
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
            transform.position = Vector2.SmoothDamp(transform.position, objective.transform.position, ref speedReference, 0.5f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Node" && !isResting)
        {
            NodeController nodeController = collision.gameObject.GetComponent<NodeController>();
            if (nodeController != null)
            {

                var (nextNode, weight) = nodeController.SelectRandomAdjacent();
                objective = nextNode.gameObject;
                energy = energy - weight;
                if(energy <= 0)
                {
                    isResting =true;
                }
            }
        }
    }
}
