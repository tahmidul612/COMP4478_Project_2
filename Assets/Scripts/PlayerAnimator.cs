using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator playerAnim;
    float horizontalMovement;
    float verticalMovement;
    bool facingRight;
    bool facingLeft;
    Rigidbody2D rb;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerAnim.ResetTrigger("jumping");
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        if (horizontalMovement != 0)
        {
            playerAnim.SetBool("walking", true);

            //Now to change directions
            if (horizontalMovement > 0.35f && !facingRight)
            {
                //If we're moving right but not facing right, flip the sprite and set facingRight to true.
                Flip();
                facingRight = true;
            }
            else if (horizontalMovement < -0.35f && facingRight)
            {
                //If we're moving left but not facing left, flip the sprite and set facingRight to false.
                Flip();
                facingRight = false;
            }

        }
        else
        {
            playerAnim.SetBool("walking", false);
        }

        if (verticalMovement != 0)
        {
            playerAnim.SetBool("jumping", true);
        }
        else
        {
            playerAnim.SetBool("jumping", false);
        }

        if (Mathf.Abs(rb.velocity.y) >= 0.1f)
        {
            playerAnim.SetBool("falling", true);
        }
        else
        {
            playerAnim.SetBool("falling", false);
        }
    }
    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerAnim.SetBool("standing", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerAnim.SetBool("standing", false);
        }
    }
}
