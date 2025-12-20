using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnbankedRewardSlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemAmonutText;


    public void SetReward(ItemDataSO item , int amount)
    {
        _itemImage.sprite = item.icon;
        _itemAmonutText.text = amount.ToString();
    }
}
