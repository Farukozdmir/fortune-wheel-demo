using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Game State
    private int _currentRound = 1;
    public int CurrentRound {get{return _currentRound;}}


    [Header ("Game Settings")]
    [SerializeField] private float _spinTime = 4;
    public float SpinTime {get {return _spinTime;}}
    [SerializeField] private int _slotCount = 8;
    public int SlotCount {get {return _slotCount;}}
    [SerializeField] private int _nextZoneMultiplierIncrease = 1;
    public int NextZoneMultiplierIncrease {get {return _nextZoneMultiplierIncrease;}}
    [SerializeField] private int _silverWheelMultiplier;
    public int SilverWheelMultiplier {get { return _silverWheelMultiplier;}}
    [SerializeField] private int _goldenWheelMultiplier;
    public int GoldenWheelMultiplier {get { return _goldenWheelMultiplier;}}


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
