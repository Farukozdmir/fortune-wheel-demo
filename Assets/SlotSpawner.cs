using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    [SerializeField] private ItemDatabaseSO itemDatabase;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private WheelScript _wheelScript;

    void Start()
    {
        SpawnSlots();
    }


    public void SpawnSlots()
    {
        int zoneMultiplier;

        if (_wheelScript.isSuperZone) zoneMultiplier = 5;
        else if (_wheelScript.isSafeZone) zoneMultiplier = 10;
        else zoneMultiplier = 1;
        for (int i = 0; i < 8; i++)
        {
            ItemDataSO item = itemDatabase.GetRandom();

            GameObject slot = Instantiate(_slotPrefab , gameObject.transform);

            slot.GetComponent<UI_Slot>().SetSlot(item , i , zoneMultiplier);
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
