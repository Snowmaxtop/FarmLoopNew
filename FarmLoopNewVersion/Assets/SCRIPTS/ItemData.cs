using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon; // Optional: for UI
    // Add other info if needed, like rarity, price, etc.
    public int baseValue;
}
