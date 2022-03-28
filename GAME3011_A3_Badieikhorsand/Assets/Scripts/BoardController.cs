using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BoardController : MonoBehaviour
{public static BoardController Instance { get; private set; }

    public RowController[] rows;
    public TileController[,] tiles { get; private set; }

    public int width => tiles.GetLength(0);
    public int height => tiles.GetLength(1);

    private void Awake() => Instance = this;

    private void Start()
    {
        tiles = new TileController[rows.Max(row => row.tile.Length), rows.Length];

        for(var y  = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tile = rows[y].tile[x];
                tiles[x, y] = tile;
                tile.posX = x;
                tile.posY = y;
                tile.item = ItemDataBase.items[Random.Range(0,ItemDataBase.items.Length)];
            }
        }
    }

}
