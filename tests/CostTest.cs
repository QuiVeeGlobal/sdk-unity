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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<stat_cost ok=\"false\" reason=\"whatever\" ikey=\"gamecoin\" type=\"currency\" value=\"15\"/>"
      );
    
    Stat c = parser.ParseACost(xmlelement) as Stat;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(c);
    Assert.AreEqual(c.ok, false);
    Assert.AreEqual(c.reason, "whatever");
    Assert.AreEqual(c.ikey, "gamecoin");
    Assert.AreEqual(c.type, "currency");
    Assert.AreEqual(c.value, 15);
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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<item_cost ok=\"false\" reason=\"whatever\" ikey=\"Christmas tree\" number_required=\"4\"/>"
      );
    
    Roar.DomainObjects.Costs.Item c = parser.ParseACost(xmlelement) as Roar.DomainObjects.Costs.Item;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(c);
    Assert.AreEqual(c.ok, false);
    Assert.AreEqual(c.reason, "whatever");
    Assert.AreEqual(c.ikey, "Christmas tree");
    Assert.AreEqual(c.number_required, 4);
  }
  
}

