using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPPacks : MonoBehaviour
{
    public string packId;

    private Product pd;
    void Start()
    {
        pd = IAPManager.Instance.GetProductInfo(packId);

        if (pd != null)
        {
            //[!] Get product thành công
        }

    }

    public void OnBuyClick()
    {
        if (pd == null)
        {
            pd = IAPManager.Instance.GetProductInfo(packId);
            //[!] Reload product nếu product không có data
            return;
        }

        if (pd != null)
        {
            IAPManager.Instance.BuyProductId(packId, delegate (object res, object pr)
            {
                if ((bool)res)
                {
                    //[!] Mua thành công

                }
            });
        }
    }
}
