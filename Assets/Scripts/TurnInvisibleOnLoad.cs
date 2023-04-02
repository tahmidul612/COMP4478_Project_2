using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInvisibleOnLoad : MonoBehaviour
{
    Renderer objectRenderer;
    private Material currentMat;
    // Start is called before the first frame update
    void Start()
    {
        currentMat = gameObject.GetComponent<Renderer>().material;
        Color oldColor = currentMat.color;
        Color newColor = new(oldColor.r, oldColor.g, oldColor.b, 0.0f);
        currentMat.SetColor("_Color", newColor);
    }
}
