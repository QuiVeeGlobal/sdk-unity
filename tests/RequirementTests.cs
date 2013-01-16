using System;
using NMock2;
using NUnit.Framework;
using Roar.DataConversion;
using Roar.DomainObjects;
using Roar.DomainObjects.Requirements;

/*
 * Test cases for Requirement component
 *
 */

[TestFixture()]
public class RequirementTests
{

  private Mockery mockery = null;
  
  [SetUp]
  public void TestInitialise()
  {
    mockery = new Mockery();
  }
  
  /*
   * This is what the Friends XML should look like
   * <requirements>
   *   <friends_requirement required="5" ok="false" reason="Insufficient friends"/>
   * </requirements>
   */
  
  [Test()]
  public void TestFriends()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<friends_requirement required=\"5\" ok=\"false\" reason=\"Insufficient friends\"/>");
    
    Friends r = parser.ParseARequirement(xmlelement) as Friends;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.required, 5);
    Assert.AreEqual(r.ok, false);
    Assert.AreEqual(r .reason, "Insufficient friends");
  }

  /*
   * This is what the Level XML should look like
   * <requirements>
   *   <level_requirement level="3" ok="false" reason="requires level 3"/>
   * </requirements>
   */
  
  [Test()]
  public void TestLevel()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<level_requirement level=\"3\" ok=\"false\" reason=\"requires level 3\"/>");
    
    Level r = parser.ParseARequirement(xmlelement) as Level;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.level, 3);
    Assert.AreEqual(r.ok, false);
    Assert.AreEqual(r.reason, "requires level 3");
  }
  
  /*
   * This is what the Item XML should look like
   * <requirements>
   *   <item_requirement ikey="christmas_tree" number_required="56" ok="false" reason="requires christmas_tree(56)"/>
   * </requirements>
   */
  
  [Test()]
  public void TestItem()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<item_requirement ikey=\"christmas_tree\" number_required=\"56\" ok=\"false\" reason=\"required christmas_tree(56)\"/>");
    
    Roar.DomainObjects.Requirements.Item r = parser.ParseARequirement(xmlelement) as Roar.DomainObjects.Requirements.Item;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.ikey, "christmas_tree");
    Assert.AreEqual(r.number_required, 56);
    Assert.AreEqual(r.ok, false);
    Assert.AreEqual(r.reason, "required christmas_tree(56)");
  }

  /*
   * This is what the Stat XML should look like
   * <requirements>
   *   <stat_requirement type="currency" ikey="gamecoins" value="45" ok="true" reason="whatever"/>
   * </requirements>
   */
  
  [Test()]
  public void TestStat()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<stat_requirement ikey=\"gamecoins\" type=\"currency\" value=\"45\" ok=\"true\" />"
    );

    
    Roar.DomainObjects.Requirements.Stat r = parser.ParseARequirement(xmlelement) as Roar.DomainObjects.Requirements.Stat;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.ikey, "gamecoins");
    Assert.AreEqual(r.type, "currency");
    Assert.AreEqual(r.value, 45);
    Assert.AreEqual(r.ok, true);
    Assert.IsNull(r.reason);
  }
  
  /*
   * This is what the True XML should look like
   * <requirements>
   *   <true_requirement ok="true"/>
   * </requirements>
   */
  
  [Test()]
  public void TestTrue()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<true_requirement ok=\"true\" />"
    );

    Roar.DomainObjects.Requirements.True r = parser.ParseARequirement(xmlelement) as Roar.DomainObjects.Requirements.True;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.ok, true);
    Assert.IsNull(r.reason);
  }

  /*
   * This is what the False XML should look like
   * <requirements>
   *   <false_requirement ok="false" reason="always fails"/>
   * </requirements>
   */
  
  [Test()]
  public void TestFalse()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<false_requirement ok=\"false\" reason=\"always fails\"/>"
    );
    
    Roar.DomainObjects.Requirements.False r = parser.ParseARequirement(xmlelement) as Roar.DomainObjects.Requirements.False;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r);
    Assert.AreEqual(r.ok, false);
    Assert.AreEqual(r.reason, "always fails");
  }
  
}

