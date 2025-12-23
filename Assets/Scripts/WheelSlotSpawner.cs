using UnityEngine;

public class WheelSlotSpawner : MonoBehaviour
{

    // Databases
    [Header ("Databases")]

    [SerializeField] private WheelSliceListSO _bronzeSpinDatabase;
    [SerializeField] private WheelSliceListSO _silverSpinDatabase;
    [SerializeField] private WheelSliceListSO _goldenSpinDatabase;

    // References
    [Header ("Referances")]

    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private ZoneController _zoneController;

    // Slot Settings

    private int _slotCount;

    // Selected Slot

    private WheelSlotController _selectedSlot;
    public WheelSlotController SelectedSlot { get { return _selectedSlot; } }

    private int _selectedSlotIndex;
    public int SelectedSlotIndex { get { return _selectedSlotIndex; } }

    // Death Slot

    private int _deathSlotIndex;
    private int _zoneMultiplier = 1;


    void Start()
    {
        _slotCount = GameManager.instance.SlotCount;
        SpawnSlots();
    }


    public void SpawnSlots()
    {
        ClearSlots();

        WheelSliceListSO wheelSliceList;

        int specialZoneMultiplier;

        _selectedSlotIndex = Random.Range(0,_slotCount);
        _deathSlotIndex = Random.Range(0,_slotCount);

        Vector3 wheelCenter = _zoneController.gameObject.transform.position;
        

        if (_zoneController.IsSuperZone) 
        {
            _zoneMultiplier += GameManager.instance.NextZoneMultiplayerIncrease;
            wheelSliceList = _goldenSpinDatabase;
            specialZoneMultiplier = GameManager.instance._goldenWheelMultiplier;
        }
        else if (_zoneController.IsSafeZone) 
        {
            _zoneMultiplier += GameManager.instance.NextZoneMultiplayerIncrease;
            wheelSliceList = _silverSpinDatabase;
            specialZoneMultiplier = GameManager.instance._silverWheelMultiplier;
        }
        else 
        {
            wheelSliceList = _bronzeSpinDatabase;
            specialZoneMultiplier = 1;
        }

        for (int i = 0; i < _slotCount; i++)
        {
            ItemDatabaseSO itemDatabase = wheelSliceList.sliceLists[i];
            ItemDataSO item = itemDatabase.GetRandom();
            GameObject slot = Instantiate(_slotPrefab , gameObject.transform);
            WheelSlotController slotController = slot.GetComponent<WheelSlotController>();

            if(_deathSlotIndex == i && !_zoneController.IsSuperZone && !_zoneController.IsSafeZone)
            {
                slotController.SetDeath(wheelCenter , _deathSlotIndex);
            }
            else
            {
                slotController.SetSlot(item , i , _zoneMultiplier + specialZoneMultiplier , wheelCenter);
            }

            if (SelectedSlotIndex == i)
            {
                _selectedSlot = slotController;
            }
        }
    }

    public void ClearSlots()
    {
        _zoneMultiplier = 1;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
