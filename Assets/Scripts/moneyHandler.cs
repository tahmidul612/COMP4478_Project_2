using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class moneyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int moneyCollected = 0;
    private TextMeshProUGUI self;
    public int maxCollected;
    private Tilemap tilemap;

    void Start()
    {
        self = GetComponent<TextMeshProUGUI>();
        tilemap = GameObject.Find("Coins").GetComponent<Tilemap>();
        maxCollected = 0;

        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        // loop through all of the tiles        
        Debug.Log(allTiles);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    maxCollected++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        self.text = "Money: $" + (moneyCollected * 100);
    }
}
