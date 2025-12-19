using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private List<ItemDataSO> _items = new List<ItemDataSO>();

    public IReadOnlyList<ItemDataSO> Items => _items;

    public ItemDataSO GetRandom()
    {
        if (_items == null || _items.Count == 0)
        {
            Debug.LogError("ItemDatabase: Item list is empty!");
            return null;
        }

        return _items[Random.Range(0, _items.Count)];
    }
}
