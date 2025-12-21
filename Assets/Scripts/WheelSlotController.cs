using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WheelSlotController : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotText;
    [SerializeField] private Sprite _deathSprite;

    private ItemDataSO _itemData;
    public ItemDataSO ItemData {get { return _itemData;}}

    private bool _isDeath;
    public bool IsDeath {get{return _isDeath;}}

    private int _amount;
    public int Amount {get { return _amount;}}

    public void SetSlot(ItemDataSO item , int slotIndex, int zoneMultiplier, Vector3 wheelPosition)
    {
        _itemData = item;
        _slotImage.sprite = item.icon;

        _amount = item.baseValue * Random.Range(1 , item.baseMultiplier + 1) * zoneMultiplier;

        _slotText.text = "x" + _amount.ToString();

        gameObject.transform.RotateAround(wheelPosition ,Vector3.forward , slotIndex * 45f);
    }

    public void SetDeath(Vector3 wheelPosition , int slotIndex)
    {
        _slotImage.sprite = _deathSprite;
        _slotText.text = string.Empty;
        _isDeath = true;

        gameObject.transform.RotateAround(wheelPosition ,Vector3.forward , slotIndex * 45f);
    }
}
