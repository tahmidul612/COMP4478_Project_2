using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customTags : MonoBehaviour
{
    [SerializeField]
    private bool isSticky = false;
    [SerializeField]
    private bool isBouncy = false;
    [SerializeField]
    private bool isSlidey = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSticky(bool v){
        isSticky = v;
    }
    public bool getSticky(){
        return isSticky;
    }
    public void setBouncy(bool v){
        isBouncy = v;
    }
    public bool getBouncy(){
        return isBouncy;
    }
    public void setSlidey(bool v){
        isSlidey = v;
    }
    public bool getSlidey(){
        return isSlidey;
    }
}
