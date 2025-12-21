using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _currentRound = 1;
    public int CurrentRound {get{return _currentRound;}}
    [SerializeField] private int _spinTime = 4;
    public int SpinTime {get {return _spinTime;}}
    [SerializeField] private int _slotCount = 8;
    public int SlotCount {get {return _slotCount;}}

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
