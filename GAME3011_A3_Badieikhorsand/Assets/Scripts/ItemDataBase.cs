using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataBase
{
    public static Item[] items { get; private set; }

   [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Initialize() => items = Resources.LoadAll<Item>("Items/");
}