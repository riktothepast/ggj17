﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearWaveCaster : MonoBehaviour
{
    public float thrust;
    private Rigidbody2D rb;
    public LayerMask mask;
    private Vector3 lookDirection;

    void Start()
    {
        lookDirection = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lookDirection.y += 2 * Time.deltaTime * -1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection,300);
        Debug.DrawRay(transform.position, lookDirection * 100, Color.red);

        if (hit.collider != null)
        {
            Debug.Log("HIT SOMETHING");
            hit.collider.GetComponent<Rigidbody2D>().AddForce(lookDirection * thrust, ForceMode2D.Force);

        }
        
        float x = 0;
        transform.Translate(-1, 0, 0);
    }

}