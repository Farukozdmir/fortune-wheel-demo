using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private Button _giveUpButton, _reviveWithGoldButton, _reviveWithAddButton;
    [SerializeField] private WheelSlotSpawner _slotSpawner;
    [SerializeField] private UnbankedRewardPanel _unbankedRewards;
    [SerializeField] private WheelController _wheelScript;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private WarningManager _warningManager;
    [SerializeField] private RoundCounterWheelController _roundCounterWheelController;

    void OnValidate()
    {
        if (_giveUpButton == null) _giveUpButton = transform.Find("ui_button_give_up")?.GetComponent<Button>();

        if (_reviveWithGoldButton == null) _reviveWithGoldButton = transform.Find("ui_button_revive_with_gold")?.GetComponent<Button>();

        if (_reviveWithAddButton == null) _reviveWithAddButton = transform.Find("ui_button_revive_with_ad")?.GetComponent<Button>();

        
    }

    void Start()
    {
        _giveUpButton.onClick.RemoveAllListeners();
        _reviveWithAddButton.onClick.RemoveAllListeners();
        _reviveWithGoldButton.onClick.RemoveAllListeners();

        _giveUpButton.onClick.AddListener(Restart);
        _reviveWithAddButton.onClick.AddListener(ReviveWithAd);
        _reviveWithGoldButton.onClick.AddListener(ReviveWithGold);

        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        _audioSource.Play();
    }

    void OnDisable()
    {
        _audioSource.Stop();
    }
    public void Restart()
    {
        _wheelScript.Restart();
        gameObject.SetActive(false);
    }

    public void ReviveWithAd()
    {
        _wheelScript.OnSpinComplete();
        gameObject.SetActive(false);
    }

    public void ReviveWithGold()
    {
        if (_inventoryManager.AddGold(-25))
        {
            _wheelScript.OnSpinComplete();
            gameObject.SetActive(false);
        }
        else
        {
            _warningManager.AddWarning("warning_you_dont_have_gold");
        }
    }
}
