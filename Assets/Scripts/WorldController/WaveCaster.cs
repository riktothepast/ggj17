using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCaster : MonoBehaviour
{
    public float thrust;
    public float amplitude;
    public float frequency;
    private int radius = 200;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        Debug.Log("onMouseDown!!!!!");
        Vector3 center = rb.transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Collider2D hit = hitColliders[i];
            i++;
            Vector3 magnitude = (hit.transform.position - center).normalized;
            magnitude.x = 0;
            magnitude.y = amplitude * Mathf.Cos(magnitude.y * frequency);
            hit.GetComponent<Rigidbody2D>().AddForce(magnitude * thrust, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = rb.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
