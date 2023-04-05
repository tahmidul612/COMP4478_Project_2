using UnityEngine;
using UnityEngine.Tilemaps;


public class CoinCollect : MonoBehaviour
{
    public AudioClip coinClip;
    AudioSource audioSource;
    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detecting the Grid Position of Player
        if (collision.gameObject.name == "Player")
        {
            Tilemap tilemap = GetComponent<Tilemap>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                tilemap.SetTile(tilemap.WorldToCell(hit.point), null);
                audioSource.PlayOneShot(coinClip, 0.7f);
            }
            CoinHandler.AddCoin();
        }

    }
}
