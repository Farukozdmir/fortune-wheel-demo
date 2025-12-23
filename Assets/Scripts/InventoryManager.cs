using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    private Dictionary<string, int> _inventoryAmounts =
        new Dictionary<string, int>();

    private Dictionary<string, ItemDataSO> _inventoryDatas =
        new Dictionary<string, ItemDataSO>();

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

    public void AddItems(Dictionary<string, ItemDataSO> items , Dictionary<string, int> amounts)
    {
        
        foreach (var pair in items)
        {
            string id = pair.Key;
            ItemDataSO item = pair.Value;

            if (!amounts.TryGetValue(id, out int amount))
            continue;

            if (id == "cash")
            {
                AddCash(amount);
                continue;
            }
            else if(id == "gold")
            {
                AddGold(amount);
                continue;
            }

            if (_inventoryAmounts.ContainsKey(id))
                _inventoryAmounts[id] += amount;
            else
                {
                    _inventoryAmounts[id] = amount;

                    if (!_inventoryDatas.ContainsKey(id))
                        _inventoryDatas.Add(id, item);
                }
        }
    }

    void OnEnable()
    {
        foreach (Transform child in _inventoryPanelTransform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var pair in _inventoryAmounts)
        {
            UnbankedRewardSlotController newSlot = Instantiate(_inventorySlotPrefab,_inventoryPanel.transform).GetComponent<UnbankedRewardSlotController>();

            if (!_inventoryDatas.TryGetValue(pair.Key, out ItemDataSO item))
                continue;

            newSlot.SetReward( item , pair.Value);
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
