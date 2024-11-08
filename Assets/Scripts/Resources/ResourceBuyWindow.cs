using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceBuyWindow : MonoBehaviour
{
    public static readonly int OpenWindowTrigger = Animator.StringToHash("Open");
    public static readonly int CloseWindowTrigger = Animator.StringToHash("Close");
    public static readonly int OnProcessTrigger = Animator.StringToHash("Process");
    public static readonly int OnSuccessfulBuyTrigger = Animator.StringToHash("Buy");
    public static readonly int OnFailBuyTrigger = Animator.StringToHash("Fail");
    
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _additionalText;
    [SerializeField] private TextMeshProUGUI _currentPriceText;
    [SerializeField] private TextMeshProUGUI _discountText;
    [SerializeField] private TextMeshProUGUI _previousPriceText;

    [SerializeField] private Image bigImage;
    
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _buyButton;

    [SerializeField] private Transform _dealItemsParent;
    [SerializeField] private ResourceDealItem _dealItemPrefab;
    
    private Animator _windowAnimator;
    private bool _opened;

    public void InitializeWindow(DealData data, Action<int, Action<bool>> inAppId)
    {
        _windowAnimator = GetComponent<Animator>();

        if (_closeButton != null)
            _closeButton.onClick.AddListener(OpenCloseBuyWindow);
        
        if (_dealItemPrefab != null)
        {
            var amount = 0;
            foreach (var d in data.ResourcesToSell)
            {
                var dealItem = Instantiate(_dealItemPrefab, _dealItemsParent); 
                dealItem.InitializeItem(d.data.ResourceSprite, d.amount);
                amount += d.amount;
            }
        }
        
        if (_buyButton != null)
            _buyButton.onClick.AddListener(delegate
            {
                OnClickBuyButton();
                inAppId.Invoke(data.InAppId, OnPurchaseCallback);
            });
        if (_titleText != null)
            _titleText.SetText(data.DealTitle);
        if (bigImage != null)
            bigImage.sprite = data.DealImage;
        if (_additionalText != null)
            _additionalText.SetText(data.DealAdditionalText);
        if (_currentPriceText != null)
            _currentPriceText.SetText($"{data.Price:0.00}$");
        if (_discountText != null)
            _discountText.SetText($"-{data.DiscountAmount.ToString()}%");
        if (_previousPriceText != null)
        {
            float prevPrice = (data.Price / 100) * (100 + data.DiscountAmount);
            _previousPriceText.SetText($"{prevPrice:0.00}$");
        }
        OpenCloseBuyWindow();
    }

    private void OpenCloseBuyWindow()
    {
        if (!_opened)
        {
            _windowAnimator.SetTrigger(OpenWindowTrigger);
        }
        else
        {
            StartCoroutine(WaitToDestroy());
            IEnumerator WaitToDestroy()
            {
                _windowAnimator.SetTrigger(CloseWindowTrigger);
                float delay = _windowAnimator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(delay);
                Destroy(gameObject);
            }
        }
       
        _opened = !_opened;
    }

    private void OnClickBuyButton()
    {
        _windowAnimator.SetTrigger(OnProcessTrigger);
    }

    private void OnPurchaseCallback(bool isSuccesful)
    {
        StartCoroutine(WaitToDestroy());
        IEnumerator WaitToDestroy()
        {  
            _windowAnimator.SetTrigger(isSuccesful ? OnSuccessfulBuyTrigger : OnFailBuyTrigger);
            yield return new WaitForEndOfFrame();
            float delay = _windowAnimator.GetCurrentAnimatorStateInfo(0).length; 
            yield return new WaitForSeconds(delay+2); 
            Destroy(gameObject);
        }
    }
}
