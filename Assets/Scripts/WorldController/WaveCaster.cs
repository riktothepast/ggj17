using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCaster : MonoBehaviour {
    private int radius = 5;
    public float thrust = 5f;
    private Rigidbody2D rb;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        Debug.Log("onMouseDown!!!!!");
        Vector3 center = rb.transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Collider2D hit = hitColliders[i];
            // float distance = Vector2.Distance(center, hit.transform.position);
            i++;
            // Debug.Log("Hit something!");
            // Debug.Log(distance);
            hit.GetComponent<Rigidbody2D>().AddForce((hit.transform.position - center) /* * distance */ * thrust, ForceMode2D.Impulse);
            // hit.GetComponent<Rigidbody2D>().AddForce((hit.transform.position - center) /* * distance */ * thrust, ForceMode2D.Impulse);

        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = rb.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
