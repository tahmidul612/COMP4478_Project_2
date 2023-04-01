using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public float speed = 10f;
    public int jump = 3;
    private Vector2 jumpHeight;
    Rigidbody2D rb;
    float lastDirection;

    Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        //rb.velocity = new Vector3(xMove, rb.velocity.y, zMove) * speed;
        Vector2 force = new Vector2(xMove, 0) * speed;
        //Vector2 move = Vector2.ClampMagnitude(force, 10);
        rb.AddForce(force);

        Vector3 clampVel = rb.velocity;
        clampVel.x =  Mathf.Clamp(clampVel.x,-10f, 10f);
        rb.velocity = clampVel;

        if(rb.velocity.x < 0){
            lastDirection = -1;
        }else if(rb.velocity.x > 0){
            lastDirection = 1;
        }

        if(xMove != 0){
            lastDirection = xMove;
        }

        
    }

    void OnCollisionStay2D(Collision2D coll){
        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "CollisionMap"){
            playerJump(coll, 0, jump);
        }
        if (coll.gameObject.tag == "Wall"){
            Debug.Log(lastDirection * -1 * (speed/2));
            playerJump(coll, lastDirection * -1 * (speed/2), jump);
        }
    }

    void playerJump(Collision2D coll, float x, float yMod){
        float height = yMod;
        if(coll.gameObject.GetComponent<customTags>().getBouncy()){
            height = (float) (yMod * 1.5);
        }
        jumpHeight = new Vector2(x,height);
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        if(yMove == 1){
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }


}
