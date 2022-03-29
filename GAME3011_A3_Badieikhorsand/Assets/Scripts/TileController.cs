using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileController : MonoBehaviour
{
    public int posX;
    public int posY;

    private Item _item;

    public Item Item
    {
        get => _item;

        set
        {
            if (_item == value)
            {
                return;
            }
            else
            {
                _item = value;

                icon.sprite = _item.sprite;
            }
        }
    }

    public Image icon;

    public Button button;

    public TileController Left => posX > 0 ? BoardController.Instance.tiles[posX - 1, posY] : null;
    public TileController Top => posY > 0 ? BoardController.Instance.tiles[posX, posY - 1] : null;
    public TileController Right => posX < BoardController.Instance.width - 1 ? BoardController.Instance.tiles[posX + 1, posY] : null;
    public TileController Bottom => posY < BoardController.Instance.height - 1 ? BoardController.Instance.tiles[posX, posY + 1] : null;

    public TileController[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom
    };

    private void Start()
    {
        button.onClick.AddListener(()=>BoardController.Instance.Select(this));
    }
    public List<TileController> GetConnectedTiles(List<TileController> exclude = null)
    {
        var result = new List<TileController> { this, };

        if (exclude == null)
        {
            exclude = new List<TileController> { this, };
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }

        return result;
    }

}
