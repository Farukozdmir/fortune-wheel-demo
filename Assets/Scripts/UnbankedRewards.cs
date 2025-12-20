using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbankedRewards : MonoBehaviour
{
    Dictionary<ItemDataSO , int> _unbankedItems = new Dictionary<ItemDataSO, int>();
    private Dictionary<string, UI_UnbankedRewardSlot> _rewardSlots 
    = new Dictionary<string, UI_UnbankedRewardSlot>();

    [SerializeField] private GameObject _rewardSlotPrefab;

    public void AddReward(ItemDataSO item, int amount)
    {
        if (_unbankedItems.ContainsKey(item))
        {
            _unbankedItems[item] += amount;
        }
        else
        {
            _unbankedItems.Add(item , amount);
        }

    }

    public void SetRewardIcons()
    {
        UI_UnbankedRewardSlot newSlot;

        foreach (var item in _unbankedItems)
        {
            if(_rewardSlots.TryGetValue(item.Key.id , out UI_UnbankedRewardSlot slot))
            {
                newSlot = slot;
            }
            else
            {
                GameObject newItem = Instantiate(_rewardSlotPrefab , transform);
                newSlot = newItem.GetComponent<UI_UnbankedRewardSlot>();
                _rewardSlots.Add(item.Key.id , newSlot);
            }
            
            newSlot.SetReward(item.Key , item.Value);
        }
    }

    public void ClearRewardSlots()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _unbankedItems.Clear();
        _rewardSlots.Clear();

    }

}
