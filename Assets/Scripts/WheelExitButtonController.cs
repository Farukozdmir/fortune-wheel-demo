using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelExitButtonController : MonoBehaviour
{
    [SerializeField] private Button _wheelExitButton;
    [SerializeField] private UnbankedRewardPanel _unbankedRewardPanel;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private WarningManager _warningManager;
    [SerializeField] private ZoneController _zoneController;
    [SerializeField] private WheelController _wheelController;

    void OnValidate()
    {
        if (_wheelExitButton == null) _wheelExitButton = GetComponent<Button>();
    }

    void Start()
    {
        _wheelExitButton.onClick.RemoveAllListeners();

        _wheelExitButton.onClick.AddListener(TryExit);
    }

    void TryExit()
    {
        if ((_zoneController.IsSafeZone || _zoneController.IsSuperZone) && !_wheelController._isSpinning)
        {
            _inventoryManager.AddItems(_unbankedRewardPanel.GetUnbankedItems());
            _mainMenuPanel.OpenMainMenuCanvas();
            _unbankedRewardPanel.ClearRewardSlots();
            GameManager.instance.ResetRound();
        }
        else
        {
            _warningManager.AddWarning("warning_you_cant_leave");
        }
    }


}
