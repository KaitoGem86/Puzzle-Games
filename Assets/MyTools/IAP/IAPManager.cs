using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

public class IAPManager : SingletonMonoBehaviour<IAPManager>, IStoreListener, IDetailedStoreListener
{
    #region Instance
    #endregion

    #region Static variables
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    // Product id
    #endregion

    #region Inspector variables
    [SerializeField]
    public IAPPack[] packs;
    public IAPPack FindIAPPack(string _id)
    {
        for (int i = 0; i < packs.Length; i++)
        {
            if (packs[i].google_id_pack == _id)
                return packs[i];
        }
        return null;
    }
    #endregion

    #region Member Variables
    private AdsEvent.TwoParamsEvent callback;
    #endregion

    #region Unity Methods

    public override void Awake()
    {
        Init();
    }
    #endregion

    #region Public Methods

    public void Init()
    {
        if (storeController == null)
            InitializePurchasing();
    }
    public void BuyProductId(string productId, AdsEvent.TwoParamsEvent callback)
    {
#if UNITY_EDITOR || DEBUG_MODE
        this.callback = callback;
        ProcessPurchase(null);
#elif UNITY_ANDROID || UNITY_IOS
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log("[IAPManager]: Purchasing product : " + productId);
                storeController.InitiatePurchase(product);
                this.callback = callback;
            }
            else
            {
                Debug.LogError("[IAPManager]: BuyProductId: " + productId + " fail-> not purchasing or not found");
            }
        }
        else
        {
            Debug.LogError("[IAPManager]: BuyProductId: " + productId + " fail-> IAP not initialized");
        }
#endif
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("[IAPManager]:RestorePurchases fail-> IAP not initialized");
        }
        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = extensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result, restore) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");

            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public Product GetProductInfo(string productId)
    {
        if (IsInitialized())
        {
            return storeController.products.WithID(productId);
        }
        else
        {
            return null;
        }

    }
    public string GetPackIdWithIndex(int index)
    {
        if (index >= packs.Length)
        {
            Debug.Log("[IAPManager]:(GetPackIdWithIndex) " + index + " out of index");
            return null;
        }
#if UNITY_ANDROID
        return packs[index].google_id_pack;
#elif UNITY_IOS
        return packs[index].apple_id_pack;
#else
        return "";
#endif
    }
    #endregion

    #region Private Methods
    private void InitializePurchasing()
    {
        if (IsInitialized())
            return;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (IAPPack pack in packs)
        {
#if UNITY_ANDROID || UNITY_EDITOR
            builder.AddProduct(pack.google_id_pack, pack.type);
#elif UNITY_IOS || UNITY_IPHONE
            builder.AddProduct(pack.apple_id_pack, pack.type);
#endif
        }
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }
    #endregion

    #region Interface Methods
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("[IAPManager]: IAP Initialized!");
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("[IAPManager]: Total product " + storeController.products.all.Length);
        foreach (IAPPack pack in packs)
        {
            Product product = null;
#if UNITY_ANDROID || UNITY_EDITOR
            product = storeController.products.WithID(pack.google_id_pack);
#elif UNITY_IOS
            product = storeController.products.WithID(pack.apple_id_pack);
#endif
            if (product != null)
            {
                pack.packPrice = product.metadata.localizedPrice.ToString();
                pack.currencyCode = product.metadata.isoCurrencyCode;
#if UNITY_ANDROID
                Debug.Log("[IAPManager]: Pack: " + pack.google_id_pack + " avaiable" + "Purchase avaiable " + product.availableToPurchase + " Price: " + pack.packPrice);
#elif UNITY_IOS
                Debug.Log("[IAPManager]: Pack: " + pack.apple_id_pack + " avaiable"+ "Purchase avaiable "+product.availableToPurchase);
#endif
            }
            else
            {
#if UNITY_ANDROID
                Debug.LogError("[IAPManager]: Pack: " + pack.google_id_pack + " unavaiable");
#elif UNITY_IOS
                Debug.LogError("[IAPManager]: Pack: " + pack.apple_id_pack + " unavaiable");
#endif
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("[IAPManager]: IAP initialize failed. Error: " + error);
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("[IAPManager](OnPurchaseFailed) product:" + i.definition.id + " reason: " + p.ToString());
        if (callback != null)
        {
            callback(false, i);
            callback = null;
        }
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true; // Presume valid for platforms with no R.V.
#if UNITY_EDITOR || DEBUG_MODE
        if (validPurchase)
        {
            Debug.Log("Buy success");
            // Unlock the appropriate content here.           
            if (callback != null)
            {
                callback(true, null);
                callback = null;
            }
        }
        else
        {
            if (callback != null)
            {
                callback(false, null);

                callback = null;
            }
        }
        // Unity IAP's validation logic is only included on these platforms.
#elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        // Prepare the validator with the secrets we prepared in the Editor
        // obfuscation window.
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
             AppleTangle.Data(), Application.identifier);

        try
        {
            // On Google Play, result has a single product ID.
            // On Apple stores, receipts contain multiple products.
            var result = validator.Validate(e.purchasedProduct.receipt);
            // For informational purposes, we list the receipt(s)
            Debug.Log("Receipt is valid. Contents:");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
            }
        }
        catch (IAPSecurityException)
        {
            Debug.Log("Invalid receipt, not unlocking content");
            validPurchase = false;
        }

        if (validPurchase)
        {
            Debug.Log("[IAPManager](PurchaseProcessingResult)<success> Product: " + e.purchasedProduct.definition.id);
            Product pro = e.purchasedProduct;
            // Unlock the appropriate content here.
            if (callback != null)
            {
                callback(true, e.purchasedProduct);
                callback = null;
                // Sent event to appflyer
                // AppsflyerManager.Instance.TrackAppflyerPurchase(pro.definition.id, (float)pro.metadata.localizedPrice, pro.metadata.isoCurrencyCode, pro.transactionID);
            }
            else
            {
                // Auto restore non-consumable items
                //if(e.purchasedProduct.definition.id == "td_vip_account")

                //{
                //    AbiModule.AntiCheat.ProtectPrefs.Instance.SetInt(Const.IS_VIP, 1);
                //    AbiModule.AntiCheat.ProtectPrefs.Instance.SetInt(Const.CAN_SHOW_ADS, 0);
                //    GlobalSetting.Instance.isVip = true;
                //    GlobalSetting.Instance.canShowAds = false;
                //}
            }
        }
        else
        {
            Debug.Log("[IAPManager](PurchaseProcessingResult)<fail> Product: " + e.purchasedProduct.definition.id);
            if (callback != null)
            {
                callback(false, e.purchasedProduct);
                callback = null;
            }
        }
#endif
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new NotImplementedException();
    }
    #endregion
}

[Serializable]
public class IAPPack
{
    public string google_id_pack;
    public string apple_id_pack;
    public string packName;
    public string packPrice;
    public string currencyCode;
    public ProductType type;
}
