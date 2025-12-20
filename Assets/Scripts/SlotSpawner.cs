using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    [SerializeField] private ItemDatabaseSO _bronzeSpinDatabase, _silverSpinDatabase, _goldenSpinDatabase;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private WheelScript _wheelScript;
    private int _slotCount;
    public UI_Slot selectedSlot;
    // Selected Slot
    private int _selectedSlotIndex;
    public int SelectedSlotIndex
    {
        get { return _selectedSlotIndex; }
    }

    private int _deathSlotIndex;

    void Start()
    {
        _slotCount = GameManager.instance.SlotCount;
        SpawnSlots();
    }


    public void SpawnSlots()
    {
        ClearSlots();

        ItemDatabaseSO itemDatabase;
        int zoneMultiplier;
        _selectedSlotIndex = Random.Range(0,_slotCount);
        _deathSlotIndex = Random.Range(0,_slotCount);
        

        if (_wheelScript.isSuperZone) 
        {
            zoneMultiplier = 10;
            itemDatabase = _goldenSpinDatabase;
        }
        else if (_wheelScript.isSafeZone) 
        {
            zoneMultiplier = 5;
            itemDatabase = _silverSpinDatabase;
        }
        else 
        {
            zoneMultiplier = 1;
            itemDatabase = _bronzeSpinDatabase;
        }

        for (int i = 0; i < 8; i++)
        {
            
            ItemDataSO item = itemDatabase.GetRandom();

            GameObject slot = Instantiate(_slotPrefab , gameObject.transform);

            if(_deathSlotIndex == i && !_wheelScript.isSuperZone && !_wheelScript.isSafeZone)
            {
                slot.GetComponent<UI_Slot>().SetDeath(_wheelScript.gameObject.transform.position , _deathSlotIndex);
            }
            else
            {
                slot.GetComponent<UI_Slot>().SetSlot(item , i , zoneMultiplier , _wheelScript.gameObject.transform.position);
            }

            if (SelectedSlotIndex == i)
            {
                selectedSlot = slot.GetComponent<UI_Slot>();
            }
        }
    }

    public void ClearSlots()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

    }
}
