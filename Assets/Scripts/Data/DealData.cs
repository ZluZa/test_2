using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DealData", menuName = "Shop/DealData")]

public class DealData : ScriptableObject
{
    //Для тестового задания нет смысла делать целую систему инаппов, но по хорошему сюда бы айдишник покупки
    //А вообще по хорошему все не через скриптабл обджекты делать, а через конфигирируемые извне конфиги, но мы упрощаем
    //Поэтому и цену я прописываю сюда, хотя она должна идти из апп стора
    [SerializeField] private int _inAppId;
    [SerializeField] private int _discountAmount;
    [SerializeField] private float _price;
    [SerializeField] private List<DealItem> _resourcesToSell;
    [SerializeField] private Sprite _dealImage;
    [SerializeField] private string _dealTitle;
    [SerializeField] private string _dealAdditionalText;
    
    public int InAppId => _inAppId;
    public int DiscountAmount => _discountAmount;
    public float Price => _price;
    public List<DealItem> ResourcesToSell => _resourcesToSell;
    public Sprite DealImage => _dealImage;
    public string DealTitle => _dealTitle;
    public string DealAdditionalText => _dealAdditionalText;

    [Serializable]
    public struct DealItem
    {
        public ResourceData data;
        public int amount;
    }
}
