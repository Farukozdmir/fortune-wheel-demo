using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public int _currentRound = 1;

    void Start()
    {
        if (instance == null)
            instance = this;
    }
}
