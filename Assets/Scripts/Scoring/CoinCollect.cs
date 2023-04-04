using UnityEngine;
using UnityEngine.Tilemaps;


public class CoinCollect : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.gameObject.name == "Player")
        {
            Tilemap tilemap = GetComponent<Tilemap>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                tilemap.SetTile(tilemap.WorldToCell(hit.point), null);
            }
            CoinHandler.AddCoin();
        }

    }
}
