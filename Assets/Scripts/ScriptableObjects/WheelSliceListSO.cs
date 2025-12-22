using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wheel Slice List")]
public class WheelSliceListSO : ScriptableObject
{
    [Header("Slice Item Lists")]
    public List<ItemDatabaseSO> sliceLists = new();
}
