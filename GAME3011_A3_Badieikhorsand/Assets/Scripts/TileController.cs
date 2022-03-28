using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileController : MonoBehaviour
{
    public int posX;
    public int posY;

    private Item _item;

    public Item item
    {
        get => item;
        set
        {
            if (_item == value) return;
            _item = value;
            icon.sprite = _item.sprite;
        }
    }

    public Image icon;

    public Button button;
}
