using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxTilemap : MonoBehaviour
{
    private float startPosX, startPosY;
    public GameObject cameraObject;
    public float speedX, speedY;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = cameraObject.transform.position.x * (1 - speedX);
        float distanceX = (cameraObject.transform.position.x * speedX);

        float tempY = cameraObject.transform.position.y * (1 - speedY);
        float distanceY = (cameraObject.transform.position.y * speedY);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    }
}
