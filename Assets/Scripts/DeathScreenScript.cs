using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScript : MonoBehaviour
{
    [SerializeField] private Button _giveUpButton, _reviveWithGoldButton, _reviveWithAddButton;
    [SerializeField] private SlotSpawner _slotSpawner;
    [SerializeField] private UnbankedRewards _unbankedRewards;
    [SerializeField] private WheelScript _wheelScript;

    void OnEnable()
    {
        _giveUpButton.onClick.AddListener(Restart);
        _reviveWithAddButton.onClick.AddListener(Revive);
        _reviveWithGoldButton.onClick.AddListener(Revive);
    }

    void OnDisable()
    {
        _giveUpButton.onClick.RemoveListener(Restart);
    }
    public void Restart()
    {
        _slotSpawner.ClearSlots();
        _unbankedRewards.ClearRewardSlots();
        _slotSpawner.SpawnSlots();
        GameManager.instance._currentRound = 1;
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        _wheelScript.OnSpinComplete();
        gameObject.SetActive(false);
    }
}
