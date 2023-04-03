using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : MonoBehaviour
{
    private float speed = 2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask ceilingLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform ceilingDetector;


    // Update is called once per frame
    void Update()
    {
        if (ceilingTouched())
        {
            speed = -4f;
        }
        if (groundTouched())
        {
            speed = 2f;
        }
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    private bool ceilingTouched()
    {
        return Physics2D.OverlapCircle(ceilingDetector.position, 1.51f, ceilingLayer);
    }

    private bool groundTouched()
    {
        return Physics2D.OverlapCircle(ceilingDetector.position, 1.01f, groundLayer);
    }
}
