using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    [SerializeField] private ShopController _shopController;

    [SerializeField] private List<ResourceData> _availableResources;
    [SerializeField] private ResourceCounter _resourceCounterPrefab;
    [SerializeField] private Transform _resourceCounterParent;
    [SerializeField] private ResourceBuyWindow _resourceBuyWindowPrefab;

    private List<ResourceCounter> _counters = new List<ResourceCounter>();

    private Action<int> onBuyButtonClicked;

    protected void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_resourceCounterPrefab == null) return;
        onBuyButtonClicked += OpenWindow;
        foreach (var r in _availableResources)
        {
            var resourceCounter = Instantiate(_resourceCounterPrefab, _resourceCounterParent);
            resourceCounter.Initialize(r, onBuyButtonClicked);
            resourceCounter.UpdateCount(LoadFromPrefs(r.ItemId));
            _counters.Add(resourceCounter);
        }
    }

    public void OpenWindow(int resourceId)
    {
        if (_resourceBuyWindowPrefab != null)
        {
            var window = Instantiate(_resourceBuyWindowPrefab, transform);
            window.InitializeWindow(_shopController.GetDeal(resourceId), (i, action) => _shopController.ProcessPurchase(i, 
                delegate(bool b)
                {
                    action(b);
                    ProcessPurchase(resourceId);
                }));
        }
    }

    public void ProcessPurchase(int resourceId)
    {
        var amount = 0;
        foreach (var d in _shopController.GetDeal(resourceId).ResourcesToSell)
        {
            amount += d.amount;
        }
        SaveToPrefs(resourceId, LoadFromPrefs(resourceId)+amount);
        _counters.Find(x => x.GetId == resourceId).UpdateCount(LoadFromPrefs(resourceId));
    }
    
    public void SaveToPrefs(int itemId, int amount)
    {
        PlayerPrefs.SetInt(itemId.ToString(), amount);
    }

    public int LoadFromPrefs(int itemId)
    {
        return PlayerPrefs.GetInt(itemId.ToString());
    }
}
