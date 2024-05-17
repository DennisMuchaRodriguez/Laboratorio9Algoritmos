using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    public GameObject groundPrefab;
    public TextAsset mapTxt;
    public string[] allMapString;
    private string[] currentLineString;
    public Vector2 initialPosition;
    public float posSeparation;
    private void Awake()
    {
        if(mapTxt != null)
        {
            allMapString = mapTxt.text.Split('\n');
            for(int i = 0; i < allMapString.Length; i++)
            {
                currentLineString = allMapString[i].Split(",");
                for(int j = 0; j < currentLineString.Length; j++)
                {
                    Vector2 position = new Vector2(initialPosition.x + posSeparation * j, initialPosition.y - posSeparation * i);
                    GameObject tmp = Instantiate(groundPrefab,position, transform.rotation);
                    tmp.transform.SetParent(this.gameObject.transform);
                }
            }
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
