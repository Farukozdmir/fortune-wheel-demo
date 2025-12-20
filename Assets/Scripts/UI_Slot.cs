using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] private Image _slotImage;
    [SerializeField] private TextMeshProUGUI _slotText;
    [SerializeField] private Sprite _deathSprite;
    private ItemDataSO _itemData;
    private bool _isDeath;
    public bool IsDeath {get{return _isDeath;}}
    public ItemDataSO ItemData
    {
        get { return _itemData;}
    }
    private int _amount;
    public int Amount
    {
        get { return _amount;}
    }

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
        _slotText.text = "";
        _isDeath = true;

        gameObject.transform.RotateAround(wheelPosition ,Vector3.forward , slotIndex * 45f);
    }

    public void SelectedAnimation()
{
    float distance = 22f;   // ne kadar dağılacak
    float duration = 0.5f;  // animasyon süresi

    GameObject copy1 = Instantiate(gameObject, transform);
    GameObject copy2 = Instantiate(copy1, transform);   
    GameObject copy3 = Instantiate(copy1, transform);
    GameObject copy4 = Instantiate(copy1, transform);

    copy1.GetComponent<UI_Slot>()._slotText.text = "";
    copy2.GetComponent<UI_Slot>()._slotText.text = "";
    copy3.GetComponent<UI_Slot>()._slotText.text = "";
    copy4.GetComponent<UI_Slot>()._slotText.text = "";

    Vector3 targetScale = Vector3.one * 0.4f;

    copy1.transform.DOLocalMove(new Vector3( distance,  distance, 0), duration).SetEase(Ease.OutBack);
    copy1.transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
    copy1.transform.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.OutBack);

    copy2.transform.DOLocalMove(new Vector3(-distance,  distance, 0), duration).SetEase(Ease.OutBack);
    copy2.transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
    copy2.transform.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.OutBack);

    copy3.transform.DOLocalMove(new Vector3( distance, -distance, 0), duration).SetEase(Ease.OutBack);
    copy3.transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
    copy3.transform.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.OutBack);

    copy4.transform.DOLocalMove(new Vector3(-distance, -distance, 0), duration).SetEase(Ease.OutBack);
    copy4.transform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
    copy4.transform.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.OutBack);



}

}
