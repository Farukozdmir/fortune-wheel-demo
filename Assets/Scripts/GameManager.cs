using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _currentRound = 1;
    public int CurrentRound {get{return _currentRound;}}
    [Header ("Game Settings")]
    [SerializeField] private float _spinTime = 4;
    public float SpinTime {get {return _spinTime;}}
    [SerializeField] private int _slotCount = 8;
    public int SlotCount {get {return _slotCount;}}
    [SerializeField] private int _nextZoneMultiplierIncrease = 1;
    public int NextZoneMultiplayerIncrease {get {return _nextZoneMultiplierIncrease;}}

    [SerializeField] public int _silverWheelMultiplier {get ; private set;}
    [SerializeField] public int _goldenWheelMultiplier {get ; private set;}

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void NextRound()
    {
        _currentRound ++;
    }

    public void ResetRound()
    {
        _currentRound = 1;
    }
}
