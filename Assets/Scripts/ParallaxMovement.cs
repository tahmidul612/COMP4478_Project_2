using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    private float length;
    private float startPosX, startPosY;
    public GameObject cameraObject;
    public float speedX, speedY;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = cameraObject.transform.position.x * (1 - speedX);
        float distanceX = (cameraObject.transform.position.x * speedX);

        float tempY = cameraObject.transform.position.y * (1 - speedY);
        float distanceY = (cameraObject.transform.position.y * speedY);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if(tempX > startPosX + length)
        {
            startPosX += length;
        }
        else if(tempX < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
