using UnityEngine;

public class WheelSlotSpawner : MonoBehaviour
{

    // Databases
    [Header ("Databases")]

    [SerializeField] private ItemDatabaseSO _bronzeSpinDatabase;
    [SerializeField] private ItemDatabaseSO _silverSpinDatabase;
    [SerializeField] private ItemDatabaseSO _goldenSpinDatabase;

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

        Vector3 wheelCenter = _zoneController.gameObject.transform.position;
        

        if (_zoneController.IsSuperZone) 
        {
            zoneMultiplier = 10;
            itemDatabase = _goldenSpinDatabase;
        }
        else if (_zoneController.IsSafeZone) 
        {
            zoneMultiplier = 5;
            itemDatabase = _silverSpinDatabase;
        }
        else 
        {
            zoneMultiplier = 1;
            itemDatabase = _bronzeSpinDatabase;
        }

        for (int i = 0; i < _slotCount; i++)
        {
            
            ItemDataSO item = itemDatabase.GetRandom();
            GameObject slot = Instantiate(_slotPrefab , gameObject.transform);
            WheelSlotController slotController = slot.GetComponent<WheelSlotController>();

            if(_deathSlotIndex == i && !_zoneController.IsSuperZone && !_zoneController.IsSafeZone)
            {
                slotController.SetDeath(wheelCenter , _deathSlotIndex);
            }
            else
            {
                slotController.SetSlot(item , i , zoneMultiplier , wheelCenter);
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
}
