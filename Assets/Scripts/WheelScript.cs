using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class WheelScript : MonoBehaviour
{

    private RectTransform _spinRectTransform;
    
    
    [SerializeField] private float _spinTime = 4;
    [SerializeField] private int _slotCount = 8;
    private float _sliceAngle;
    private bool _isSpinning;
    [SerializeField] private SlotSpawner slotSpawner;

    // Safe and SuperZone Bools
    private bool _isSafeZone;
    public bool isSafeZone
    {
        get { return _isSafeZone; }
    }
    private bool _isSuperZone;
    public bool isSuperZone
    {
        get { return _isSuperZone; }
    }

    [Header ("Spin Sprites")]
    private Image _spinImage;
   [SerializeField] private Sprite _bronzeSpinSprite, _silverSpinSprite, _goldenSpinSprite;

   [Header ("Spin Indicator")]
   [SerializeField] private Image _spinIndicatorImage;
   [SerializeField] private Sprite _bronzeSpinIndicatorSprite,_silverSpinIndicatorSprite, _goldenSpinIndicatorSprite;

    void OnValidate()
    {
            _spinRectTransform = gameObject.GetComponent<RectTransform>();
            _spinImage = gameObject.GetComponent<Image>();
    }

    void Awake()
    {
        _sliceAngle = 360f / _slotCount;
    }

    public void StartSpin()
    {
        if (_isSpinning)
            return;

        _isSpinning = true;
        
        int targetSlot = Random.Range(0,_slotCount);

        _spinRectTransform
            .DORotate(new Vector3(0f, 0f, -1440 + (targetSlot * _sliceAngle)), _spinTime, RotateMode.FastBeyond360)
            .SetEase(Ease.OutExpo)
            .OnComplete(OnSpinComplete);

        
        
    }

    void OnSpinComplete()
    {
        GameManager.instance._currentRound++;
        int spinCount = GameManager.instance._currentRound;
        if (spinCount % 30 == 0)
        { 
            _spinImage.sprite = _goldenSpinSprite;
            _spinIndicatorImage.sprite = _goldenSpinIndicatorSprite;
            _isSuperZone = true;
        }
        else if (spinCount % 5 == 0) 
        {
            _spinImage.sprite = _silverSpinSprite;
            _spinIndicatorImage.sprite = _silverSpinIndicatorSprite;
            _isSafeZone = true;
        }    
        else if (spinCount % 5 == 1) 
        {
            _spinImage.sprite = _bronzeSpinSprite;
            _spinIndicatorImage.sprite = _bronzeSpinIndicatorSprite;
            _isSafeZone = false;
            _isSuperZone = false;
        }

        slotSpawner.ClearSlots();
        slotSpawner.SpawnSlots();

        _isSpinning = false;
    }
}
