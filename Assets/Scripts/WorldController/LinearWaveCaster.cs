using System.Collections;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDirection, 300, mask);
        Debug.DrawRay(transform.position, lookDirection * 100, Color.red);

        if (hit.collider != null)
        {
            Rigidbody2D rbody = hit.collider.GetComponent<Rigidbody2D>();
            if (rbody)
            {
                rbody.AddForce(lookDirection * thrust, ForceMode2D.Force);
            }
        }

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen)
        {
            transform.Translate(-1, 0, 0);
        }
    }

}
