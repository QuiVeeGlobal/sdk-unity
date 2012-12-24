using System;
using System.Collections;
using System.Collections.Generic;
using Roar.Components;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Roar.implementation.Components
{
	public class InAppPurchase : IInAppPurchase
	{
		protected ILogger logger;
		protected IDataStore dataStore;
		protected IWebAPI.IAppstoreActions actions;
		protected bool isSandbox;
		protected bool hasDataFromAppstore;
		protected bool isServerCalling;
		protected IDictionary<string, Hashtable> productsMap;
		protected IList<Hashtable> productsList;
		
		//TODO: Ugly that we need two of these for now.
		protected Roar.Callback<string> purchaseCallback;
		protected Roar.Callback<Roar.WebObjects.Appstore.BuyResponse> purchaseCallbackX;

		public InAppPurchase (IWebAPI.IAppstoreActions actions, string nativeCallbackGameObject, ILogger logger, bool isSandbox)
		{
			this.logger = logger;
			this.actions = actions;
			this.isSandbox = isSandbox;
			hasDataFromAppstore = false;
			isServerCalling = false;
			productsMap = new Dictionary<string, Hashtable> ();
			productsList = new List<Hashtable> ();
    		#if UNITY_IOS && !UNITY_EDITOR
      		_StoreKitInit(nativeCallbackGameObject);
    		#else
			logger.DebugLog (string.Format ("Can't call _StoreKitInit({0}) from Unity Editor", nativeCallbackGameObject));
    		#endif
		}

		/**
	     * Fetch has two stages:
	     * 1. Retrieve the appstore product ids from roar.
	     * 2. Use the product ids to retrieve the product details from the appstore.
	     **/
		public void Fetch (Roar.Callback<WebObjects.Appstore.ShopListResponse> callback)
		{
			if (isServerCalling) {
				return;
			}
			isServerCalling = false;
			productsMap.Clear ();
			productsList.Clear ();
			actions.shop_list ( new Roar.WebObjects.Appstore.ShopListArguments(), new AppstoreListCallback (callback, this));
		}

		class AppstoreListCallback : ZWebAPI.Callback<WebObjects.Appstore.ShopListResponse>
		{
			InAppPurchase appstore;
			Roar.Callback<WebObjects.Appstore.ShopListResponse> cb_;

			public AppstoreListCallback (Roar.Callback<WebObjects.Appstore.ShopListResponse> in_cb, InAppPurchase in_appstore)
			{
				appstore = in_appstore;
				cb_ = in_cb;
			}

			public void OnSuccess (Roar.CallbackInfo<WebObjects.Appstore.ShopListResponse> info)
			{
				appstore.isServerCalling = false;
				appstore.logger.DebugLog (string.Format ("onAppstoreList.onSuccess() called with: {0}", info.data.ToString()));
				string combinedProductIdentifiers = string.Join (",", (string[])info.data.shop_list.ConvertAll<string>(e => e.product_identifier).ToArray());
#if UNITY_IOS && !UNITY_EDITOR
				_StoreKitRequestProductData(combinedProductIdentifiers);
#else
				appstore.logger.DebugLog (string.Format ("Can't call _StoreKitRequestProductData({0}) from Unity Editor", combinedProductIdentifiers));
#endif
				cb_(info);
			}
			
			public void OnError( RequestResult info )
			{
				appstore.isServerCalling = false;
				cb_( new CallbackInfo<WebObjects.Appstore.ShopListResponse>( null, info.code, info.msg) );

			}
		}

		public IList<Hashtable> List ()
		{
			return  productsList;
		}

		public Hashtable GetShopItem (string productIdentifier)
		{
			return productsMap [productIdentifier];
		}



		public bool HasDataFromServer { get { return hasDataFromAppstore; } }

		protected void ValidateReceipt (string receiptId, Roar.Callback<WebObjects.Appstore.BuyResponse> callback)
		{


			
			WebObjects.Appstore.BuyArguments args = new Roar.WebObjects.Appstore.BuyArguments();
			args.receipt = receiptId;
			args.sandbox = isSandbox;

			actions.buy (args, new OnReceiptValidation (callback, this, receiptId));
		}

		class OnReceiptValidation : ZWebAPI.Callback<WebObjects.Appstore.BuyResponse>
		{
			InAppPurchase appstore;
			string receiptId;
			Roar.Callback<WebObjects.Appstore.BuyResponse> cb;

			public OnReceiptValidation (Roar.Callback<WebObjects.Appstore.BuyResponse> in_cb, InAppPurchase in_appstore, string in_receiptId) 
			{
				appstore = in_appstore;
				receiptId = in_receiptId;
				cb = in_cb;
			}

			public void OnSuccess (CallbackInfo<WebObjects.Appstore.BuyResponse> info)
			{
				appstore.logger.DebugLog (string.Format ("onReceiptValidation() called with: {0}", info.data.ToString()));
			}
			
			public void OnError (RequestResult info)
			{
				appstore.logger.DebugLog (string.Format ("onReceiptValidation() called with: {0}", info.data.DebugAsString ()));
			}
		}


		//TODO: Ugly that we need two of these
		public void Purchase (string productId, Roar.Callback<string> cb, Roar.Callback<Roar.WebObjects.Appstore.BuyResponse> cbx)
		{
			purchaseCallback = cb;
			purchaseCallbackX = cbx;
    		#if UNITY_IOS && !UNITY_EDITOR
      		_StoreKitPurchase(productId);
    		#else
			logger.DebugLog (string.Format ("Can't call _StoreKitPurchase({0}) from Unity Editor", productId));
   			#endif
		}

		public void Purchase (string productId, int quantity, Roar.Callback<string> cb, Roar.Callback<Roar.WebObjects.Appstore.BuyResponse> cbx)
		{
			purchaseCallback = cb;
			purchaseCallbackX = cbx;

    		#if UNITY_IOS && !UNITY_EDITOR
      		_StoreKitPurchaseQuantity(productId, quantity);
    		#else
			logger.DebugLog (string.Format ("Can't call _StoreKitPurchase({0}) from Unity Editor", productId));
    		#endif
		}

		public bool PurchasesEnabled ()
		{
    		#if UNITY_IOS && !UNITY_EDITOR
      		return _StoreKitPurchasesEnabled();
    		#else
			logger.DebugLog (string.Format ("Can't call _StoreKitPurchasesEnabled() from Unity Editor"));
			return false;
    		#endif
		}

		// Store Kit wrapper to roar Unity client communication methods

		public void OnProductData (string productDataXml)
		{

			hasDataFromAppstore = true;
			productsList.Clear ();
			productsMap.Clear ();
			logger.DebugLog (string.Format ("OnProductData() called with: {0}", productDataXml));
			IXMLNode root = IXMLNodeFactory.instance.Create (productDataXml);
			IXMLNode appstoreNode = root.GetFirstChild ("appstore");
			IEnumerable<IXMLNode> children = appstoreNode.Children;

			if (children != null) {
				foreach (IXMLNode shopItemXml in children) {
					string pid = shopItemXml.GetAttribute ("product_identifier");
					Hashtable shopItemHashtable = new Hashtable ();
					foreach (KeyValuePair<string, string> attribute in shopItemXml.Attributes) {
						logger.DebugLog (string.Format ("Adding product {0} property {1}:{2}", pid, attribute.Key, attribute.Value));
						shopItemHashtable [attribute.Key] = attribute.Value;
					}
					logger.DebugLog (string.Format ("Adding {0} to productsList_:", pid));
					logger.DebugLog (Roar.Json.HashToJSON (shopItemHashtable));
					productsList.Add (shopItemHashtable);
					productsMap.Add (pid, shopItemHashtable);
				}
			} else {
				logger.DebugLog ("No products passed to OnProductData()");
			}
		}

		public void OnInvalidProductId (string invalidProductId)
		{
			logger.DebugLog (string.Format ("OnInvalidProductId() called with: {0}", invalidProductId));
		}

		public void OnPurchaseComplete (string purchaseXml)
		{
			logger.DebugLog (string.Format ("OnPurchaseComplete() called with: {0}", purchaseXml));
			IXMLNode root = IXMLNodeFactory.instance.Create (purchaseXml);
			IXMLNode purchaseNode = root.GetFirstChild ("shop_item_purchase_success");
			string transactionIdentifier = purchaseNode.GetAttribute ("transaction_identifier");
			ValidateReceipt (transactionIdentifier, purchaseCallbackX);
		}

		public void OnPurchaseCancelled (string productIdentifier)
		{
			logger.DebugLog (string.Format ("OnPurchaseCancelled() called with: {0}", productIdentifier));
			if (purchaseCallback != null) {
				purchaseCallback (new CallbackInfo<string> (productIdentifier, IWebAPI.DISALLOWED, "Purchase cancelled by user"));
			}
		}

		public void OnPurchaseFailed (string errorXml)
		{
			logger.DebugLog (string.Format ("OnPurchaseFailed() called with: {0}", errorXml));
			if (purchaseCallback != null) {
				purchaseCallback (new CallbackInfo<string> (errorXml, IWebAPI.UNKNOWN_ERR, "Purchase failed"));
			}
		}

		// roar Unity client to Store Kit wrapper communication methods

    #region DllImports
    #if UNITY_IOS
        [DllImport ("__Internal", CharSet = CharSet.Auto)]
        static extern void _StoreKitInit(string unityGameObject);
        [DllImport ("__Internal", CharSet = CharSet.Auto)]
        static extern bool _StoreKitPurchasesEnabled();
        [DllImport ("__Internal", CharSet = CharSet.Auto)]
        static extern void _StoreKitRequestProductData(string productIdentifiers);
        [DllImport ("__Internal", CharSet = CharSet.Auto)]
        static extern void _StoreKitPurchase(string productIdentifier);
        [DllImport ("__Internal", CharSet = CharSet.Auto)]
        static extern void _StoreKitPurchaseQuantity(string productIdentifier, int quantity);
    #endif
    #endregion
	}
}
