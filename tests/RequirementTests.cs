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
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value("" + friends.ok));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(friends.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Friends);
    Assert.AreEqual((r as Friends).required, friends.required);
    Assert.AreEqual((r as Friends).ok, friends.ok);
    Assert.AreEqual((r as Friends).reason, friends.reason);
  }

  /*
   * This is what the Friends XML should look like
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
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value("" + level.ok));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(level.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Level);
    Assert.AreEqual((r as Level).level, level.level);
    Assert.AreEqual((r as Level).ok, level.ok);
    Assert.AreEqual((r as Level).reason, level.reason);
  }
  
  /*
   * This is what the Friends XML should look like
   * <requirements>
   *   <item_requirement ikey="christmas_tree" number_required="56" ok="false" reason="requires christmas_tree(56)"/>
   * </requirements>
   */
  
  [Test()]
  public void TestItem()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Item item = new Item();
    item.ikey = "christmas_tree";
    item.number_required = 56;
    item.ok = false;
    item.reason = "required christmas_tree(56)";
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("item_requirement"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value(item.ikey));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("number_required").Will(Return.Value("" + item.number_required));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ok").Will(Return.Value("" + item.ok));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("reason").Will(Return.Value(item.reason));
    
    Requirement r = parser.ParseARequirement(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(r as Item);
    Assert.AreEqual((r as Item).ikey, item.ikey);
    Assert.AreEqual((r as Item).number_required, item.number_required);
    Assert.AreEqual((r as Item).ok, item.ok);
    Assert.AreEqual((r as Item).reason, item.reason);
  }
}

