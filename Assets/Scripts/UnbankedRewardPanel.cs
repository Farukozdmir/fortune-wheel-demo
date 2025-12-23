using System.Collections.Generic;
using UnityEngine;

public class UnbankedRewardPanel : MonoBehaviour
{
    Dictionary<string, int> _rewardAmounts = new();
    Dictionary<string, UnbankedRewardSlotController> _rewardSlots = new();
    Dictionary<string, ItemDataSO> _rewardDatas = new();


    [SerializeField] private GameObject _rewardSlotPrefab;

    private ItemDataSO _lastAddedReward;

    public void AddReward(ItemDataSO item, int amount)
    {
        _lastAddedReward = item;

        if (_rewardAmounts.ContainsKey(item.id))
            _rewardAmounts[item.id] += amount;
        else
           {
            _rewardAmounts.Add(item.id, amount);
            _rewardDatas.Add(item.id, item);
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

        if (!_rewardSlots.TryGetValue(_lastAddedReward.id, out var slot))
            return;

        if (!_rewardAmounts.TryGetValue(_lastAddedReward.id, out var amount))
            return;


        slot.SetReward(_lastAddedReward, amount);
    }



    public void ClearRewardSlots()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _rewardAmounts.Clear();
        _rewardSlots.Clear();
        _rewardDatas.Clear();
        _lastAddedReward = null;
    }

    public Dictionary<string, int> GetRewardAmounts()
    {
        return new Dictionary<string, int>(_rewardAmounts);
    }

    public Dictionary<string, ItemDataSO> GetRewardDatas()
    {
        return new Dictionary<string, ItemDataSO>(_rewardDatas);
    }


}
