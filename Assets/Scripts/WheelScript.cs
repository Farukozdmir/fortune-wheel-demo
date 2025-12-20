using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class WheelScript : MonoBehaviour
{

    private RectTransform _spinRectTransform;
    
    
    
    private float _sliceAngle;
    private int _spinTime;
    private int _slotCount;
    private bool _isSpinning;
    [SerializeField] private GameObject _currentItemPanel;
    [SerializeField] private GameObject _inventorySlotPrefab;

    
    private UI_Slot _selectedSlot;

    [Header ("Reward Animation")]
    [SerializeField] private float _rewardAnimScaleTime, _rewardAnimMoveTime;

    [Header ("Slot Spawner Script")]
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

   [Header ("Unbanked Rewards")]
   [SerializeField] private UnbankedRewards _unbankedRewardsScript;

   [SerializeField] private float _tickAngle = 22.5f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tickSound;
    [SerializeField] private GameObject _deathScreen;

    private float _lastTickAngle;


    void OnValidate()
    {
            _spinRectTransform = gameObject.GetComponent<RectTransform>();
            _spinImage = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        _spinTime = GameManager.instance.SpinTime;
        _slotCount = GameManager.instance.SlotCount;
        
        _sliceAngle = 360f / _slotCount;
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0) && _isSpinning)
        {
            Time.timeScale = 4;
        }
    }


    public void StartSpin()
    {
        if (_isSpinning)
            return;

        _isSpinning = true;
        _selectedSlot = slotSpawner.selectedSlot;
        

        _spinRectTransform
            .DORotate(new Vector3(0f, 0f, -1440 - (slotSpawner.SelectedSlotIndex * _sliceAngle)), _spinTime, RotateMode.FastBeyond360)
            .SetEase(Ease.OutExpo)
            .OnUpdate(PlayTick)
            .OnComplete(GetRewards);
    }

    void PlayTick()
{
    float currentAngle = Mathf.Abs(_spinRectTransform.eulerAngles.z);

    if (Mathf.Abs(currentAngle - _lastTickAngle) >= _tickAngle)
    {
        _audioSource.PlayOneShot(_tickSound);
        _lastTickAngle = currentAngle;
    }
}


    void GetRewards()
    {
        Time.timeScale = 1;

        if (_selectedSlot.IsDeath)
        {
            DeathSelected();
            return;
        }

        RectTransform selectedSlotTransform = _selectedSlot.gameObject.GetComponent<RectTransform>();

        _unbankedRewardsScript.AddReward(_selectedSlot.ItemData , _selectedSlot.Amount);

        _selectedSlot.SelectedAnimation();

        Sequence seq = DOTween.Sequence();

        seq.Append(
            selectedSlotTransform
            .DOScale( Vector3.one * 2f, _rewardAnimScaleTime)
            .SetEase(Ease.OutExpo)
            );

        seq.Join(
            selectedSlotTransform
            .DOMove(gameObject.transform.position , _rewardAnimScaleTime)
            .SetEase(Ease.OutExpo)
            );

        seq.Join(
            selectedSlotTransform
            .DORotate(new Vector3(0f,0f,-360) , _rewardAnimScaleTime /3 , RotateMode.FastBeyond360)
            .SetEase(Ease.OutExpo)
        );
        
        seq.Append(
            selectedSlotTransform
            .DOMove(new Vector3 (140,250,0) , _rewardAnimMoveTime)
            .SetEase(Ease.OutExpo)
            .OnComplete(OnSpinComplete)
            );

        seq.Join(
            selectedSlotTransform
            .DORotate(Vector3.zero, _rewardAnimMoveTime, RotateMode.Fast)
            .SetEase(Ease.OutExpo)
        );

        seq.Join(
            selectedSlotTransform
            .DOScale( Vector3.one * 0.25f,_rewardAnimMoveTime)
            .SetEase(Ease.OutExpo)
        );
    }

    public void OnSpinComplete()
    {   
        Time.timeScale = 1;

        _unbankedRewardsScript.SetRewardIcons();
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

        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        slotSpawner.ClearSlots();
        slotSpawner.SpawnSlots();

        _isSpinning = false;
    }

    public void DeathSelected()
    {
        _deathScreen.SetActive(true);
        _isSpinning = false;
    }
}
