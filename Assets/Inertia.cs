using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inertia : MonoBehaviour {
    public float thrust;
    private Rigidbody2D rb;
    private Vector2 initialPosition;

	void Start () {
        initialPosition = this.GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
    }
	
	void FixedUpdate () {
        Vector2 currentPosition = this.GetComponent<Transform>().position;

        if (currentPosition.x != initialPosition.x || currentPosition.y != initialPosition.y)
        {
            Debug.Log("Object outside of its original position!");
            rb.AddForce((initialPosition - currentPosition) * thrust);
        } 
    }
}
