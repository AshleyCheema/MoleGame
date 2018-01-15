using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public float lifetime = 5f; //lifetime of a point on the trail

    public float minimumVertexDistance = 0.1f; //minimum distance moved before a new point is solidified.

    public Vector3 velocity; //direction the points are moving

    private float upDown = 0.12f;
    public Text score;
    public GameObject gameOverText;
    private float scoreNum = 1;
    public bool endGame;
  

    LineRenderer line;
    //position data
    List<Vector3> points;
    Queue<float> spawnTimes = new Queue<float>(); //list of spawn times, to simulate lifetime. Back of the queue is vertex 1, front of the queue is the end of the trail.
                                                  //Length of this queue is one less than the number of positions in the linerenderer, since the linerenderer always has a non-solidified vertex at the object's position.

    // Use this for initialization
    void Awake()
    {
        line = GetComponent<LineRenderer>();
        points = new List<Vector3>() { transform.position }; //indices 1 - end are solidified points, index 0 is always transform.position
        line.SetPositions(points.ToArray());
        endGame = false;
    }

    void AddPoint(Vector3 position)
    {
        points.Insert(1, position);
        spawnTimes.Enqueue(Time.time);
    }

    void RemovePoint()
    {
        spawnTimes.Dequeue();
        points.RemoveAt(points.Count - 1); //remove corresponding oldest point at the end
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            transform.Translate(0, upDown, 0);
        }
        else if(Input.GetKey("s"))
        {
            transform.Translate(0, -upDown, 0);
        }

        score.text = "Score: " + (int)scoreNum;
        scoreNum += Time.deltaTime;
        

        //cull based on lifetime
        while (spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time)
        {
            RemovePoint();
        }

        //move positions
        Vector3 diff = -velocity * Time.deltaTime;
        for (int i = 1; i < points.Count; i++)
        {
            points[i] += diff;
        }

        //add new point
        if (points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance)
        {
            //if we have no solidified points, or we've moved enough for a new point
            AddPoint(transform.position);
        }

        //update index 0;
        points[0] = transform.position;

        //save result
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            gameOverText.SetActive(true);
            endGame = true;
        }
    }
}
