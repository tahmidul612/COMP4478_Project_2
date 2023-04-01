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

    public float lastXMove;
    public float lastYMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        if(xMove != 0){
            lastXMove = xMove;
        }
        if(yMove != 0){
            lastYMove = yMove;
        }

        Vector2 force = new Vector2(xMove, 0) * speed;
        rb.AddForce(force);

        Vector3 clampVel = rb.velocity;
        clampVel.x =  Mathf.Clamp(clampVel.x,-10f, 10f);
        rb.velocity = clampVel;

        Vector3 clampVelDown = rb.velocity;
        clampVelDown.y =  Mathf.Clamp(clampVel.y,-10f, 10f);
        rb.velocity = clampVelDown;
    }

    void OnCollisionStay2D(Collision2D coll){
        
        if(Input.GetAxisRaw("Vertical") == 1){
            if (coll.gameObject.tag == "Floor" || coll.gameObject.name == "Tilemap"){
                playerJump(coll, 0, jump);
            }
            if (coll.gameObject.tag == "Wall"){
                Debug.Log(lastXMove * -1 * (speed/2));
                playerJump(coll, lastXMove * -1 * (speed/2), jump);
            }
        }
    }

    void playerJump(Collision2D coll, float x, float yMod){
        float height = yMod;
        if(coll.gameObject.GetComponent<customTags>().getBouncy()){
            height = (float) (yMod * 1.5);
        }
        jumpHeight = new Vector2(x,height);
        float yMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        rb.AddForce(jumpHeight * yMove, ForceMode2D.Impulse);
    }
}
