using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects")]
public class ItemTemplate : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public Sprite itemSprite;
}
