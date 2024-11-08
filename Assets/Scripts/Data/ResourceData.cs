using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "ResourceData")]
public class ResourceData : ScriptableObject
{
    //TODO: по хорошему сделать бы автогенерацию айдишника и запрет на редактирование
    [SerializeField] private int _itemId;
    [SerializeField] private Sprite _resourceSprite;
    [SerializeField] private string _itemName;

    public int ItemId => _itemId;
    public Sprite ResourceSprite => _resourceSprite;
    public string ItemName => _itemName;
}
