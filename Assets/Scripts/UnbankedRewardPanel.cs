using System.Collections.Generic;
using UnityEngine;

public class UnbankedRewardPanel : MonoBehaviour
{
    Dictionary<ItemDataSO , int> _unbankedItems = new Dictionary<ItemDataSO, int>();
    private Dictionary<string, UnbankedRewardSlotController> _rewardSlots = new Dictionary<string, UnbankedRewardSlotController>();
    [SerializeField] private GameObject _rewardSlotPrefab;

    private ItemDataSO _lastAddedReward;

    public void AddReward(ItemDataSO item, int amount)
    {
        _lastAddedReward = item;

        if (_unbankedItems.ContainsKey(item))
        {
            _unbankedItems[item] += amount;
        }
        else
        {
            _unbankedItems.Add(item , amount);
        }
    }

    public RectTransform SpawnSlotIfNeeded(ItemDataSO item)
    {
        if (_rewardSlots.TryGetValue(item.id, out var slot)) 
        {
            return slot.gameObject.GetComponent<RectTransform>();
        }

        GameObject slotObj = Instantiate(_rewardSlotPrefab, transform);
        slot = slotObj.GetComponent<UnbankedRewardSlotController>();

        Canvas.ForceUpdateCanvases();

        slot.Clear();
        _rewardSlots.Add(item.id, slot);

        return slot.gameObject.GetComponent<RectTransform>();
    }


    public void SetLastRewardToSlot()
    {
        if (_lastAddedReward == null)
            return;

        UnbankedRewardSlotController slot = _rewardSlots[_lastAddedReward.id];
        slot.SetReward(_lastAddedReward, _unbankedItems[_lastAddedReward]);
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

    public Dictionary<ItemDataSO, int> GetUnbankedItems()
    {
        return new Dictionary<ItemDataSO, int>(_unbankedItems);
    }

}
