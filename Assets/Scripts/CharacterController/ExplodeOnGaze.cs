using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

[RequireComponent(typeof(GazeAware))]
public class ExplodeOnGaze : MonoBehaviour
{
    public float thrust;
    public float amplitude;
    public float frequency;
    private int radius = 40;
    private Rigidbody rb;
    public LayerMask mask;

    private GazeAware _gazeAware;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EyeTracking.Initialize();
        _gazeAware = GetComponent<GazeAware>();
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            Debug.Log("Looking at him");

            Vector3 center = rb.transform.position;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius, mask);
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
    }
}
