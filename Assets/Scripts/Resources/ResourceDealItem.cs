using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDealItem : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemAmountText;

    public void InitializeItem(Sprite icon, int amount)
    {
        if (_itemIcon != null)
            _itemIcon.sprite = icon;
        if (_itemAmountText != null)
            _itemAmountText.SetText(amount.ToString());
    }
}
