using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    private Button _giveUpButton, _reviveWithGoldButton, _reviveWithAddButton;
    [SerializeField] private WheelSlotSpawner _slotSpawner;
    [SerializeField] private UnbankedRewardPanel _unbankedRewards;
    [SerializeField] private WheelController _wheelScript;

    void OnValidate()
    {
        if (_giveUpButton == null) _giveUpButton = transform.Find("ui_button_give_up")?.GetComponent<Button>();

        if (_reviveWithGoldButton == null) _reviveWithGoldButton = transform.Find("ui_button_revive_with_gold")?.GetComponent<Button>();

        if (_reviveWithAddButton == null) _reviveWithAddButton = transform.Find("ui_button_revive_with_ad")?.GetComponent<Button>();
    }

    void OnEnable()
    {
        _giveUpButton.onClick.AddListener(Restart);
        _reviveWithAddButton.onClick.AddListener(Revive);
        _reviveWithGoldButton.onClick.AddListener(Revive);
    }

    void OnDisable()
    {
        _giveUpButton.onClick.RemoveListener(Restart);
        _reviveWithAddButton.onClick.RemoveListener(Revive);
        _reviveWithGoldButton.onClick.RemoveListener(Revive);
    }
    public void Restart()
    {
        _slotSpawner.ClearSlots();
        _unbankedRewards.ClearRewardSlots();
        _slotSpawner.SpawnSlots();
        GameManager.instance.ResetRound();
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        _wheelScript.OnSpinComplete();
        gameObject.SetActive(false);
    }
}
