using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WheelController : MonoBehaviour
{
    // Core References
    
    [SerializeField] private RectTransform _spinRectTransform;
    [SerializeField] private Button _spinButton;

    // Spin Settings

    private float _sliceAngle;
    private int _spinTime;
    private int _slotCount;
    public bool _isSpinning {get ; private set;}

    // Reward Animation Settings
    [Header("Reward Animation")]

    [SerializeField] private float _rewardAnimScaleTime;
    [SerializeField] private float _rewardAnimMoveTime;
    private WheelSlotController _selectedSlot;

    // Scripts
    [Header("Scripts")] 

    [SerializeField] private WheelSlotSpawner _wheelSlotSpawner;
    [SerializeField] private UnbankedRewardPanel _unbankedRewardPanel;
    [SerializeField] private ZoneController _zoneController;
    [SerializeField] private RoundCounterWheelController _roundCounterWheelController;

    // Audio
    [Header("Audio")]

    [SerializeField] private float _tickAngle = 22.5f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tickSound;
    [SerializeField] private AudioClip _moveSound;
    private float _lastTickAngle;


    // UI Panels

    [SerializeField] private GameObject _deathScreen;


    void OnValidate()
    {
        if (_spinRectTransform == null)
            _spinRectTransform = GetComponent<RectTransform>();

        if (_spinButton == null)
            _spinButton = transform.Find("ui_button_spin").GetComponent<Button>();
    }

    void OnEnable()
    {
        _zoneController.UpdateZone();
        Restart();
    }

    void Start()
    {
        _spinTime = GameManager.instance.SpinTime;
        _slotCount = GameManager.instance.SlotCount;
        
        _sliceAngle = 360f / _slotCount;

        _spinButton.onClick.RemoveAllListeners();
        _spinButton.onClick.AddListener(StartSpin);
    }

    public void Restart()
    {
        _wheelSlotSpawner.ClearSlots();
        _unbankedRewardPanel.ClearRewardSlots();
        _wheelSlotSpawner.SpawnSlots();
        GameManager.instance.ResetRound();
        _roundCounterWheelController.SetWheel();
    }

    public void StartSpin()
    {
        DOTween.timeScale = 1f;
        if (_isSpinning)
        { 
            DOTween.timeScale = 4f;
            return;
        }

        _isSpinning = true;
        _selectedSlot = _wheelSlotSpawner.SelectedSlot;
        

        _spinRectTransform
            .DORotate(new Vector3(0f, 0f, -1440 - (_wheelSlotSpawner.SelectedSlotIndex * _sliceAngle)), _spinTime, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic)
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
        if (_selectedSlot.IsDeath)
        {
            DeathSelected();
            return;
        }

        RectTransform selectedSlotTransform = _selectedSlot.gameObject.GetComponent<RectTransform>();

        _unbankedRewardPanel.AddReward(_selectedSlot.ItemData , _selectedSlot.Amount);
        RectTransform lastSlotRectTransform = _unbankedRewardPanel.SpawnSlotIfNeeded(_selectedSlot.ItemData);

        Sequence seq = DOTween.Sequence();

        seq.Append(
            selectedSlotTransform
            .DOScale( Vector3.one * 2f, _rewardAnimScaleTime)
            .SetEase(Ease.OutCubic)
            .OnComplete(PlayMoveSound)
            );

        seq.Join(
            selectedSlotTransform
            .DOMove(gameObject.transform.position , _rewardAnimScaleTime)
            .SetEase(Ease.OutCubic)
            );

        seq.Join(
            selectedSlotTransform
            .DORotate(new Vector3(0f,0f,-360) , _rewardAnimScaleTime /3 , RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic)
        );
        
        seq.Append(
            selectedSlotTransform
            .DOMove( lastSlotRectTransform.position, _rewardAnimMoveTime)
            .SetEase(Ease.OutCubic)
            .OnComplete(OnSpinComplete)
            );

        seq.Join(
            selectedSlotTransform
            .DORotate(Vector3.zero, _rewardAnimMoveTime, RotateMode.Fast)
            .SetEase(Ease.OutCubic)
        );

        seq.Join(
            selectedSlotTransform
            .DOScale( Vector3.one * 0.25f,_rewardAnimMoveTime)
            .SetEase(Ease.OutCubic)
        );
    }

    void PlayMoveSound()
    {
        _audioSource.PlayOneShot(_moveSound , 0.5f);
    }

    public void OnSpinComplete()
    {   
        

        _unbankedRewardPanel.SetLastRewardToSlot();
        GameManager.instance.NextRound();
        _zoneController.UpdateZone();

        gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

        _wheelSlotSpawner.ClearSlots();
        _wheelSlotSpawner.SpawnSlots();

        _isSpinning = false;
        _roundCounterWheelController.NextRound();
        DOTween.timeScale = 1;
        
    }

    public void DeathSelected()
    {   
        DOTween.timeScale = 1f;
        _deathScreen.SetActive(true);
        _isSpinning = false;
    }
}
