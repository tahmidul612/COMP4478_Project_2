using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class collectCoin : MonoBehaviour
{
    public Vector3 size;
    private SpriteRenderer player_renderer;
    private moneyHandler moneyObj;
    // Start is called before the first frame update
    void Start()
    {
        player_renderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        size = player_renderer.bounds.size;
        moneyObj =  GameObject.Find("MoneyText").GetComponent<moneyHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.gameObject.name == "Player")
        {        
            Tilemap tilemap = GetComponent<Tilemap>();

            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                tilemap.SetTile(tilemap.WorldToCell(hit.point), null);
            }
            moneyObj.moneyCollected++;
        }
 
    }
}