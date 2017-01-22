using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadmanController : MonoBehaviour {
    private Rigidbody2D rb;

    public float maxUpAndDown;
    public float speedUpAndDown;
    private float angleUpAndDown;

    public float maxLeftAndRight;
    public float speedLeftAndRight;
    private float angleLeftAndRight;

    private float toDegrees = Mathf.PI/180;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
 
	void Update () {
        rb.velocity = Camera.main.velocity;

        angleUpAndDown += speedUpAndDown * Time.deltaTime;
        if (angleUpAndDown > 360) {
            angleUpAndDown -= 360;
        }

        angleLeftAndRight += speedLeftAndRight * Time.deltaTime;
        if (angleLeftAndRight > 360)
        {
            angleLeftAndRight -= 360;
        }

        transform.Translate(new Vector3(maxLeftAndRight * Mathf.Cos(angleLeftAndRight * toDegrees), maxUpAndDown * Mathf.Sin(angleUpAndDown * toDegrees), 0));
    }
}
