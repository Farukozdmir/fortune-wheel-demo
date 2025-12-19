using UnityEngine;

[CreateAssetMenu(
    fileName = "ItemData",
    menuName = "Game/Item Data"
)]
public class ItemDataSO : ScriptableObject
{
    public string id;
    public Sprite icon;
    public int baseValue;
    public int baseMultiplier;
}

