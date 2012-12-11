using System;
using NUnit.Framework;
using NMock2;
using Roar.DataConversion;
using Roar.DomainObjects;
using Roar.DomainObjects.Costs;

[TestFixture()]
public class CostTest
{

  private Mockery mockery = null;
  
  [SetUp]
  public void TestInitialise()
  {
    this.mockery = new Mockery();
  }
  
  [Test()]
  public void TestMultiple()
  {
  }
  
  /*
   * This is what the Stat XML should look like:
   * <costs>
   *   <stat_cost type="currency" ikey="gamecoins" value="15" ok="true"/>
   * </costs>
   */
  
  [Test()]
  public void TestStat()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Stat stat = new Stat();
    stat.ok = false;
    stat.reason = "whatever";
    stat.ikey = "gamecoin";
    stat.type = "currency";
    stat.value = 15;
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("stat_cost"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(stat.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(stat.reason));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value(stat.ikey));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("type").Will(Return.Value(stat.type));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("value").Will(Return.Value("" + stat.value));
    
    Cost c = parser.ParseACost(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(c as Stat);
    Assert.AreEqual((c as Stat).ok, stat.ok);
    Assert.AreEqual((c as Stat).reason, stat.reason);
    Assert.AreEqual((c as Stat).ikey, stat.ikey);
    Assert.AreEqual((c as Stat).type, stat.type);
    Assert.AreEqual((c as Stat).value, stat.value);
  }
  
  /*
   * This is what the Item XML should look like:
   * <costs>
   *   <item_cost ikey="christmas_tree" number_required="4" ok="false" reason="requires christmas_tree(4)"/>
   * </costs>
   */
  
  [Test()]
  public void TestItem()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Roar.DomainObjects.Costs.Item item = new Roar.DomainObjects.Costs.Item();
    item.ok = false;
    item.reason = "whatever";
    item.ikey = "Christmas tree";
    item.number_required = 4;
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("item_cost"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(item.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value("" + item.reason));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value(item.ikey));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("number_required").Will(Return.Value("" + item.number_required));
    
    Cost c = parser.ParseACost(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(c as Roar.DomainObjects.Costs.Item);
    Assert.AreEqual((c as Roar.DomainObjects.Costs.Item).ok, item.ok);
    Assert.AreEqual((c as Roar.DomainObjects.Costs.Item).reason, item.reason);
    Assert.AreEqual((c as Roar.DomainObjects.Costs.Item).ikey, item.ikey);
    Assert.AreEqual((c as Roar.DomainObjects.Costs.Item).number_required, item.number_required);
  }
  
}

