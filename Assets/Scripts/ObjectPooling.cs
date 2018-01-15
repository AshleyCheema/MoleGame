using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public int objectPoolSize = 5;
    public GameObject objectPrefab;
    public float spawnRate = 4f;

    public float objectMin = -1f;
    public float objectMax = 3.5f;

    private GameObject[] objects;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
    private float timerSinceLastSpawn;
    private float spawnXPosition = 10f;
    private int currentObject = 0;
    private DrawLine drawLine; 

	// Use this for initialization
	void Start ()
    {
        objects = new GameObject[objectPoolSize];
        drawLine = gameObject.GetComponent<DrawLine>();

        for (int i = 0; i < objectPoolSize; i++)
        {
            objects[i] = (GameObject)Instantiate(objectPrefab, objectPoolPosition, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timerSinceLastSpawn += Time.deltaTime;

        if (timerSinceLastSpawn >= spawnRate && drawLine.endGame == false)
        {
            timerSinceLastSpawn = 0;
            float spawnYPosition = Random.Range(objectMin, objectMax);
            objects[currentObject].transform.position = new Vector2(spawnXPosition, spawnYPosition);

            currentObject++;

            if(currentObject >= objectPoolSize)
            {
                currentObject = 0;
            }
        }


	}
}
