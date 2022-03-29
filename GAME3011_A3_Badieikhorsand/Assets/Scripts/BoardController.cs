using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using System.Threading.Tasks;
public class BoardController : MonoBehaviour
{public static BoardController Instance { get; private set; }

    public RowController[] rows;
    public TileController[,] tiles { get; private set; }

    public int width => tiles.GetLength(0);
    public int height => tiles.GetLength(1);

    private readonly List<TileController> _selection = new List<TileController>();

    [SerializeField] private float Duration = 0.1f;
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
                tile.Item = ItemDataBase.items[Random.Range(0,ItemDataBase.items.Length)];
            }
        }
    }
    public async void Select(TileController tile)
    {
        if(!_selection.Contains(tile))
            _selection.Add(tile);


        if (_selection.Count < 2)  return;

        await Swap(_selection[0], _selection[1]);
        _selection.Clear();

    }
    public async Task Swap(TileController tile1, TileController tile2)
    {

        var icon1 = tile1.icon;
        var icon1Transform = icon1.transform;
        var icon2 = tile2.icon;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();

        sequence.Join(icon1.transform.DOMove(icon2Transform.position, Duration))
               .Join(icon2Transform.DOMove(icon1Transform.position, Duration));

        await sequence.Play()
                         .AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;


    }
    private bool Canpop()
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private async void Pop()
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var tile = tiles[x, y];

                var connectedTiles = tile.GetConnectedTiles();

                if (connectedTiles.Skip(1).Count() < 2)
                {
                    continue;
                }

                var deafaultSequence = DOTween.Sequence();


                foreach (var connectedTile in connectedTiles)
                {
                    deafaultSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, Duration));

                   

                    await deafaultSequence.Play()
                                          .AsyncWaitForCompletion();
                }



                var inflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDataBase.items[Random.Range(0, ItemDataBase.items.Length)];

                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, Duration));
                }

                await inflateSequence.Play()
                                     .AsyncWaitForCompletion();

                x = 0;
                y = 0;
            }
        }
    }



}
