using System;
using NMock2;
using NUnit.Framework;
using Roar.implementation.DataConversion;
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Friends friends = new Friends();
    friends.required = 5;
    friends.ok = false;
    friends.reason = "Insufficient friends";
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("friends_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("required").Will(Return.Value("" + friends.required));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(friends.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(friends.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Friends);
    Assert.AreEqual((r as Friends).required, friends.required);
    Assert.AreEqual((r as Friends).ok, friends.ok);
    Assert.AreEqual((r as Friends).reason, friends.reason);
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Level level = new Level();
    level.level = 3;
    level.ok = false;
    level.reason = "requires level 3";
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("level_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("level").Will(Return.Value("" + level.level));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(level.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(level.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Level);
    Assert.AreEqual((r as Level).level, level.level);
    Assert.AreEqual((r as Level).ok, level.ok);
    Assert.AreEqual((r as Level).reason, level.reason);
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Roar.DomainObjects.Requirements.Item item = new Roar.DomainObjects.Requirements.Item();
    item.ikey = "christmas_tree";
    item.number_required = 56;
    item.ok = false;
    item.reason = "required christmas_tree(56)";
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("item_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value(item.ikey));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("number_required").Will(Return.Value("" + item.number_required));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(item.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(item.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Roar.DomainObjects.Requirements.Item);
    Assert.AreEqual((r as Roar.DomainObjects.Requirements.Item).ikey, item.ikey);
    Assert.AreEqual((r as Roar.DomainObjects.Requirements.Item).number_required, item.number_required);
    Assert.AreEqual((r as Roar.DomainObjects.Requirements.Item).ok, item.ok);
    Assert.AreEqual((r as Roar.DomainObjects.Requirements.Item).reason, item.reason);
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Stat stat = new Stat();
    stat.ikey = "gamecoins";
    stat.type = "currency";
    stat.value = 45;
    stat.ok = true;
    stat.reason = null;
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("stat_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value(stat.ikey));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("type").Will(Return.Value(stat.type));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("value").Will(Return.Value("" + stat.value));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(stat.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(stat.reason));
    System.Console.Out.WriteLine("bool [" + true + "] [" + false + "]");
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Stat);
    Assert.AreEqual((r as Stat).ikey, stat.ikey);
    Assert.AreEqual((r as Stat).type, stat.type);
    Assert.AreEqual((r as Stat).value, stat.value);
    Assert.AreEqual((r as Stat).ok, stat.ok);
    Assert.AreEqual((r as Stat).reason, stat.reason);
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    True true_requirement = new True();
    true_requirement.ok = true;
    true_requirement.reason = null;
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("true_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(true_requirement.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(true_requirement.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as True);
    Assert.AreEqual((r as True).ok, true_requirement.ok);
    Assert.AreEqual((r as True).reason, true_requirement.reason);
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
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    False false_requirement = new False();
    false_requirement.ok = false;
    false_requirement.reason = "always fails";
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("false_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value(false_requirement.ok ? "true" : "false"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(false_requirement.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as False);
    Assert.AreEqual((r as False).ok, false_requirement.ok);
    Assert.AreEqual((r as False).reason, false_requirement.reason);
  }
  
}

