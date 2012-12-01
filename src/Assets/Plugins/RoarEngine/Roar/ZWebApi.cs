using System;
using System.Collections;
using Roar.WebObjects;



namespace Roar
{

	public class ZWebApi
	{
		public IWebAPI iwebapi_;
		
		public ZWebApi (IWebAPI iwebapi)
		{
			Shop = new ShopActions(iwebapi.shop);
		}
		
		public interface Callback<T>
		{
			void OnError( Roar.RequestResult nn );
			void OnSuccess( Roar.CallbackInfo<T> nn );
		}
		
		public class ShopActions
		{
			public IWebAPI.IShopActions shopActions_;
			
			public ShopActions( IWebAPI.IShopActions shopActions )
			{
				shopActions_ = shopActions;
			}
			
			public void list( WebObjects.Shop.ListArguments args, Callback<WebObjects.Shop.ListResponse> cb)
			{
				shopActions_.list( args.ToHashtable(), new CallbackBridge<WebObjects.Shop.ListResponse>(cb) );
				
			}
			public void buy( WebObjects.Shop.BuyArguments args, Callback<WebObjects.Shop.BuyResponse> cb)
			{
				shopActions_.list( args.ToHashtable(), new CallbackBridge<WebObjects.Shop.BuyResponse>(cb) );
			}
		}
		
		ShopActions Shop;
	}
}

