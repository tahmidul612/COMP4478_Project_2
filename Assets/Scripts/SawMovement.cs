using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    private float speed = 2f;
    private bool turned = false;
    private float yVal;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask checkpointLayer;
    [SerializeField] private Transform sawDetector;


    // Update is called once per frame
    void Update()
    {
        if (checkpointTouched() && turned == false)
        {
            speed = speed * -1;
            turned = true;
            StartCoroutine(waitForMove());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private bool checkpointTouched()
    {
        return Physics2D.OverlapCircle(sawDetector.position, 1.5f, checkpointLayer);
    }

    IEnumerator waitForMove()
    {
        yield return new WaitForSeconds(0.2f);
        turned = false;
    }

}
