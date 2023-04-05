using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTimer : MonoBehaviour
{

    [SerializeField] GameObject fireBlock;
    [SerializeField] GameObject embersBlock;
    bool timerFinished = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (timerFinished == true)
        {
            timerFinished = false;
            StartCoroutine(fire());
        }
    }

    IEnumerator fire()
    {
        // Debug.Log("Working?");
        yield return new WaitForSeconds(2f);
        embersBlock.SetActive(true);
        // Debug.Log("Workin2g?");
        yield return new WaitForSeconds(1f);
        // Debug.Log("Working3?");
        fireBlock.SetActive(true);
        yield return new WaitForSeconds(3f);
        fireBlock.SetActive(false);
        embersBlock.SetActive(false);
        timerFinished = true;
    }
}
