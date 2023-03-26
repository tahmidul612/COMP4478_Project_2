using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class collectCoin : MonoBehaviour
{
    private Vector3 size;
    private SpriteRenderer player_renderer;
    // Start is called before the first frame update
    void Start()
    {
        player_renderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        size = player_renderer.bounds.size;
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
            Debug.Log("Player collided with coin");
            Tilemap tilemap = GetComponent<Tilemap>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                tilemap.SetTile(tilemap.WorldToCell(hit.point), null);
                Debug.Log("HitPoint: " + hit.point + "WorldToCell: " + tilemap.WorldToCell(hit.point));
            }
        }
 
    }
}
