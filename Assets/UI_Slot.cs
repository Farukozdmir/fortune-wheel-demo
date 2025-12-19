using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotText;
    public int amount;

    public void SetSlot(ItemDataSO item , int slotIndex, int zoneMultiplier)
    {
        _slotImage.sprite = item.icon;

        int amount = item.baseValue * Random.Range(1 , item.baseMultiplier + 1) * zoneMultiplier;

        _slotText.text = "x" + amount.ToString();

        gameObject.transform.Rotate(Vector3.forward , slotIndex * 45f);
    }
}
