using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopController : MonoBehaviour
{
        [SerializeField] private List<DealData> _availableDeals;

        public DealData GetDeal(int dealId)
        {
            return _availableDeals.Find(x => x.InAppId == dealId);
        }

        public void ProcessPurchase(int dealId, Action<bool> onPurchaseCallback)
        {
            //псевдообработка
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(1.5f);
                onPurchaseCallback(true);
            }
        }
}
