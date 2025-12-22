using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<ItemDataSO, int> _inventory =
        new Dictionary<ItemDataSO, int>();

    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Transform _inventoryPanelTransform;
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private Button _inventoryExitButton;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _cashText;
    private int _goldAmount;
    private int _cashAmount;

    void OnValidate()
    {
        if (_inventoryExitButton == null) _inventoryExitButton = transform.Find("ui_button_inventory_exit").GetComponent<Button>();

        if (_inventoryPanelTransform == null) _inventoryPanelTransform = _inventoryPanel.GetComponent<Transform>();
    }

    void Start()
    { 
        _inventoryExitButton.onClick.RemoveAllListeners();

        _inventoryExitButton.onClick.AddListener(_mainMenuPanel.OpenMainMenuCanvas);
    }

    public void AddItems(Dictionary<ItemDataSO, int> items)
    {
        foreach (var pair in items)
        {
            if (pair.Key.id == "cash")
            {
                AddCash(pair.Value);
                continue;
            }
            else if(pair.Key.id == "gold")
            {
                AddGold(pair.Value);
                continue;
            }

            if (_inventory.ContainsKey(pair.Key))
                _inventory[pair.Key] += pair.Value;
            else
                _inventory[pair.Key] = pair.Value;
        }
    }

    void OnEnable()
    {
        foreach (Transform child in _inventoryPanelTransform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var pair in _inventory)
        {
            UnbankedRewardSlotController newSlot = Instantiate(_inventorySlotPrefab,_inventoryPanel.transform).GetComponent<UnbankedRewardSlotController>();

            newSlot.SetReward( pair.Key , pair.Value);
        }
    }

    void RefreshHud()
    {
        _goldText.text = _goldAmount.ToString();
        _cashText.text = _cashAmount.ToString();
    }

    public bool AddCash(int cashAddAmount)
    {
        if (_cashAmount + cashAddAmount >= 0)
        {
        _cashAmount += cashAddAmount;
        RefreshHud();
        return true;
        }

        return false;
    }

    public bool AddGold(int coinAddAmount)
    {
        if (_goldAmount + coinAddAmount >= 0)
        {
            _goldAmount += coinAddAmount;
            RefreshHud();
            return true;
        }
        
        return false;
    }
}
