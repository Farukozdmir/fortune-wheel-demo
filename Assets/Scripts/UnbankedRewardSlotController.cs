using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnbankedRewardSlotController : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _slotFrameImage;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    public void SetReward(ItemDataSO item , int amount)
    {
        _slotFrameImage.enabled = true;
        _itemImage.enabled = true;
        _itemImage.sprite = item.icon;
        _itemAmountText.text = amount.ToString();
    }

        public void Clear()
    {   
        _slotFrameImage.enabled = false;
        _itemImage.sprite = null;
        _itemImage.enabled = false;
        _itemAmountText.text = string.Empty;
    }
}
