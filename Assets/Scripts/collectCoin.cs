using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class collectCoin : MonoBehaviour
{
    public Vector3 size;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        size = renderer.bounds.size;
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
            //GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Tilemap tilemap = GetComponent<Tilemap>();
            //Vector3 hitPosition = Vector3.zero;
            //ContactPoint2D hit = collision.GetContact(0);
            //Vector2 tileNorm = hit.normal;

            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = (hit.point.x) * (hit.normal.x >= 0 ? 1 : -1);
                hitPosition.y = (hit.point.y) * (hit.normal.y >= 0 ? 1 : -1);
                Debug.Log("HitPoint: " + hit.point + ", HitPosition: " + hitPosition + ", WorldToCell: " + tilemap.WorldToCell(hitPosition) + ", TileNorm: " + hit.normal);
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }

            //Debug.Log("HitPoint: " + hit.point + "HitPosition: " + hitPosition + "WorldToCell: " + world + "TileNorm: " + tileNorm);
            //tilemap.SetTile(world, null);
            //}
        }
 
    }
}
