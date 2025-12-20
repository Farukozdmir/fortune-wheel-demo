using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int _currentRound = 1;
    [SerializeField] private int _spinTime = 4;
    public int SpinTime {get {return _spinTime;}}
    [SerializeField] private int _slotCount = 8;
    public int SlotCount {get {return _slotCount;}}

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
