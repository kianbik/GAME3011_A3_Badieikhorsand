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

    private void Start()
    {
        button.onClick.AddListener(()=>BoardController.Instance.Select(this));
    }
}
