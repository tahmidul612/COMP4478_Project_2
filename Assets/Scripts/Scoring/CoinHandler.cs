using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private static int CoinCollected = 0;
    private static TextMeshProUGUI CoinText;
    public static int MaxCollectable = 0;
    private Tilemap tilemap;

    void Start()
    {
        TotalCoins();
    }

    private void TotalCoins()
    {
        tilemap = GameObject.Find("Coins").GetComponent<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        MaxCollectable = allTiles.Count(tile => tile != null);
        Debug.Log("Total coins: " + MaxCollectable);

        CoinText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public static void AddCoin()
    {
        CoinCollected++;
        CoinText.text = "Coins: " + (CoinCollected * 100);
    }

    public static int GetCoins()
    {
        return CoinCollected;
    }
}
