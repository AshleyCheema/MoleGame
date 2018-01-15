using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{

    private Rigidbody2D rigidbod;
    private float scrollSpeed = -5f;

	// Use this for initialization
	void Start ()
    {
        rigidbod = GetComponent<Rigidbody2D>();
        rigidbod.velocity = new Vector2(scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
