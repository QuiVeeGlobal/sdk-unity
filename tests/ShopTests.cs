using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.Components;


[TestFixture]
public class LowLevelShopTests
{
	Mockery mock;
	[SetUp]
	public void SetUp()
	{
		mock = new Mockery();
		
	}

	[Test]
	public void testListForwardsToDataModel()
	{
		WebAPI.IShopActions shop_actions = mock.NewMock<WebAPI.IShopActions>();
		Roar.implementation.IDataStore datastore = mock.NewMock<Roar.implementation.IDataStore>();
		Roar.ILogger logger = mock.NewMock<Roar.ILogger>();
		Roar.implementation.Components.Shop shop = new Roar.implementation.Components.Shop(shop_actions,datastore,logger);
		
		IDataModel<Roar.DomainObjects.ShopEntry,Roar.WebObjects.Shop.ListResponse> shop_datamodel = mock.NewMock<IDataModel<Roar.DomainObjects.ShopEntry,Roar.WebObjects.Shop.ListResponse>>();
		
		List<Roar.DomainObjects.ShopEntry> retval = new List<Roar.DomainObjects.ShopEntry>();
		retval.Add( new Roar.DomainObjects.ShopEntry() );
		retval.Add( new Roar.DomainObjects.ShopEntry() );
		
		Expect.AtLeast(1).On(datastore)
			.GetProperty("shop")
			.Will(Return.Value( shop_datamodel ) );
			
		Expect.AtLeast(1).On(shop_datamodel)
			.Method("List")
			.Will (Return.Value( retval ) );
		
		IList<Roar.DomainObjects.ShopEntry> l = shop.List ();
		
		Assert.AreEqual(2, l.Count );
		
		mock.VerifyAllExpectationsHaveBeenMet();
		
	}
}


/**
 * Test cases for the Shop component.
 **/
[TestFixture]
public class ShopTests : ComponentTests
{
  protected IShop shop;
  
  // returned by server when shop items are fetched.
  public static string shopList = 
@"<roar tick='130695522924'>
  <shop>
    <list status='ok'>
      <shopitem ikey='shop_item_ikey_1' label='Shop item 1' description='Lorem Ipsum'>
        <costs>
          <stat_cost type='currency' ikey='cash' value='100' ok='false' reason='Insufficient Coins'/>
          <stat_cost type='currency' ikey='premium_currency' value='0' ok='true'/>
        </costs>
        <modifiers>
          <grant_item ikey='item_ikey_1'/>
        </modifiers>
        <tags/>
      </shopitem>
      <shopitem ikey='shop_item_ikey_2'/>
      <shopitem ikey='shop_item_ikey_3' label='Shop item 2'/>
      <shopitem ikey='shop_item_ikey_4' description='Blah Blah'>
        <costs>
          <stat_cost type='currency' ikey='cash' value='0'/>
          <stat_cost type='currency' ikey='premium_currency' value='50'/>
        </costs>
        <modifiers>
          <grant_item ikey='item_ikey_2'/>
        </modifiers>
        <tags>
          <tag value='a_tag'/>
          <tag value='another_tag'/>
        </tags>
      </shopitem>
    </list>
  </shop>
</roar>";

  [SetUp]
  new public void TestInitialise()
  {
    base.TestInitialise();
    shop = roar.Shop;
    Assert.IsNotNull(shop);
    Assert.IsFalse(shop.HasDataFromServer);
  }
  
  protected void mockFetch(string mockResponse, Roar.Callback< IDictionary<string, Roar.DomainObjects.ShopEntry> > cb) {
    requestSender.addMockResponse("shop/list", mockResponse);
    // todo: mock a response from items/view for testing the item cache
    requestSender.addMockResponse("items/view", " ");
    shop.Fetch(cb);
  }
  
  [Test]
  public void testFetchSuccess() {
    bool callbackExecuted = false;
    Roar.Callback< IDictionary<string, Roar.DomainObjects.ShopEntry> > roarCallback = (Roar.CallbackInfo< IDictionary<string, Roar.DomainObjects.ShopEntry> > callbackInfo) => { 
      callbackExecuted=true;
      Assert.AreEqual(IWebAPI.OK, callbackInfo.code);
      Assert.IsNotNull(callbackInfo.data);
    };
    mockFetch(shopList, roarCallback);
    Assert.IsTrue(callbackExecuted);
    Assert.IsTrue(shop.HasDataFromServer);
  }

  [Test]
  [Ignore]
  public void testFetchFailureServerDown() {
    //assertions:
    //callback called with expected error code
    //HasDataFromServer == false
  }
  


  [Test]
  public void testList() {

    mockFetch(shopList, null);
    //Assert.IsTrue(shop.HasDataFromServer);
    
    //returns a list of shop items with the expected data structure
    int expectedItemCount = 4;
    IList<Roar.DomainObjects.ShopEntry> shopEntries = shop.List();
    Assert.AreEqual(expectedItemCount, shopEntries.Count);
    
    //invokes callback with parameter *data* containing the list of Hashtable shop items

    shopEntries = shop.List();
    Assert.AreEqual(expectedItemCount, shopEntries.Count);
  }

  [Test]
  [Ignore]
  // TODO: modifiers is returning null - does this not get translated from the xml?
  public void testGetShopItem() {
    
    //returns null on no data from server
    Assert.IsNull(shop.GetShopItem("shop_item_ikey_1"));
    
    mockFetch(shopList, null);
    
    //returns Hashtable of property if exists
    Roar.DomainObjects.ShopEntry shopItem = shop.GetShopItem("shop_item_ikey_1");
    IList<Roar.DomainObjects.Cost> costs= shopItem.costs;
    Roar.DomainObjects.Costs.Stat costA = costs[0] as Roar.DomainObjects.Costs.Stat;
    Roar.DomainObjects.Costs.Stat costB = costs[1] as Roar.DomainObjects.Costs.Stat;
    StringAssert.IsMatch("cash", costA.ikey);
    StringAssert.IsMatch("premium_currency", costB.ikey);
    Assert.AreEqual(false, costA.ok);
    Assert.AreEqual(true, costB.ok);
    
    IList<Roar.DomainObjects.Modifier> modifiers = shopItem.modifiers;
    Roar.DomainObjects.Modifiers.GrantItem modifier = modifiers[0] as Roar.DomainObjects.Modifiers.GrantItem;
    StringAssert.IsMatch("item_ikey_1", modifier.ikey );

    //returns null on property not existing
    Assert.IsNull(shop.GetShopItem("doesnotexist"));
  }
        
  [Test]
  [Ignore]
  public void testBuy() {
    //assertions:
    //correct event triggered with expected arguments (GoodInfo)
    //callback executed with expected arguments (GoodInfo)
  }
 
  [Test]
  [Ignore]
  public void testBuyFailureServerDown() {
    //assertions:
    //callback called with expected error code
  }
}

