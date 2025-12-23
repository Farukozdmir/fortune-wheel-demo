using UnityEngine;

public class WheelSlotSpawner : MonoBehaviour
{

    // Databases
    [Header ("Databases")]

    [SerializeField] private WheelSliceListSO _bronzeSpinDatabase;
    [SerializeField] private WheelSliceListSO _silverSpinDatabase;
    [SerializeField] private WheelSliceListSO _goldenSpinDatabase;

    // References
    [Header ("References")]

    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private ZoneController _zoneController;
    [SerializeField] private RoundCounterWheelController _roundCounterWheelController;

    // Slot Settings

    private int _slotCount;

    // Selected Slot

    private WheelSlotController _selectedSlot;
    public WheelSlotController SelectedSlot { get { return _selectedSlot; } }

    private int _selectedSlotIndex;
    public int SelectedSlotIndex { get { return _selectedSlotIndex; } }

    // Death Slot

    private int _deathSlotIndex;

    // Multiplier
    private int _zoneMultiplier = 1;
    [SerializeField]private int _multiplier;
    public int Multiplier {get {return _multiplier;}}


    void Start()
    {
        _slotCount = GameManager.instance.SlotCount;
        SpawnSlots();
    }

    // Spawns slot and select the death index and selected index
    public void SpawnSlots()
    {
        ClearSlots();

        WheelSliceListSO wheelSliceList;

        int specialZoneMultiplier;

        _selectedSlotIndex = Random.Range(0,_slotCount);
        _deathSlotIndex = Random.Range(0,_slotCount);

        Vector3 wheelCenter = _zoneController.gameObject.transform.position;
        
        // Check zone, set multiplier and database
        if (_zoneController.IsSuperZone) 
        {
            _zoneMultiplier += GameManager.instance.NextZoneMultiplierIncrease;
            wheelSliceList = _goldenSpinDatabase;
            specialZoneMultiplier = GameManager.instance.GoldenWheelMultiplier;
        }
        else if (_zoneController.IsSafeZone) 
        {
            _zoneMultiplier += GameManager.instance.NextZoneMultiplierIncrease;
            wheelSliceList = _silverSpinDatabase;
            specialZoneMultiplier = GameManager.instance.SilverWheelMultiplier;
        }
        else 
        {
            wheelSliceList = _bronzeSpinDatabase;
            specialZoneMultiplier = 1;
        }

        _multiplier = _zoneMultiplier * specialZoneMultiplier;

        // Instantiate slots
        for (int i = 0; i < _slotCount; i++)
        {
            ItemDatabaseSO itemDatabase = wheelSliceList.sliceLists[i];
            ItemDataSO item = itemDatabase.GetRandom();
            GameObject slot = Instantiate(_slotPrefab , gameObject.transform);
            WheelSlotController slotController = slot.GetComponent<WheelSlotController>();

            if(_deathSlotIndex == i && !_zoneController.IsSuperZone && !_zoneController.IsSafeZone)
            {
                slotController.SetDeath(wheelCenter , _deathSlotIndex); // Changes isDeath bool inside the slot controller
            }
            else
            {
                slotController.SetSlot(item , i , _multiplier , wheelCenter); // Assign itemData to slot, i for rotating around wheelCenter with index.
            }

            if (SelectedSlotIndex == i)
            {
                _selectedSlot = slotController;
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

    public void ResetMultiplier()
    {
        _zoneMultiplier = 1;
        _multiplier = 1;
        _roundCounterWheelController.SetWheel();
    }
}
