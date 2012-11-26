using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.Components;
using Roar.DomainObjects;
using Roar.DomainObjects.Modifiers;
using Roar.implementation.DataConversion;

/**
 * Test cases for the Modifier components
 **/

[TestFixture()]
public class ModifierTests
{

  private Mockery mockery = null;

  [SetUp]
  public void TestInitialise()
  {
    this.mockery = new Mockery();
  }
  
  /*
   * This is what the Grant Item XML should look like:
   * <modifiers>
       <grant_item ikey="christmas_tree"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestGrantItem()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("grant_item"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("ikey").Will(Return.Value("some ID"));
    
    Modifier m = parser.ParseAModifier(ixmlnode);
    Assert.IsNotNull(m as GrantItem);
    Assert.AreEqual((m as GrantItem).ikey, "some ID");
  }
  
  /*
   * This is what the Grant XP XML should look like:
   * <modifiers>
       <grant_xp value="25"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestGrantXP()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    int xp = 25;
    
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("grant_xp"));
    Expect.AtLeastOnce.On(ixmlnode).Method("GetAttribute").With("value").Will(Return.Value("" + xp));
    
    Modifier m = parser.ParseAModifier(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantXp);
    Assert.AreEqual((m as GrantXp).value, xp);
  }
  
  /*
   * This is what the Nothing XML should look like:
   * <modifiers>
       <nothing/>
   * </modifiers>
   */
  
  [Test()]
  public void TestNothing()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("nothing"));
    Modifier m = parser.ParseAModifier(ixmlnode);
    Assert.IsNotNull(m as Nothing);
  }
  
  /*
   * This is what the If-Then-Else XML should look like:
   * <modifiers>
   *   <if_then_else>
   *     <if>
   *       <friends_requirement required="2" ok="false" reason="Insufficient friends"/>
   *       <true_requirement ok="true"/>
   *     </if>
   *     <then>
   *       <grant_stat type="attribute" ikey="_energy_max" value="0"/>
   *       <grant_xp value="54"/>
   *     </then>
   *     <else>
   *       <grant_stat_range type="currency" ikey="gamecoins" min="2" max="6"/>
   *       <remove_items/>
   *     </else>
   *   </if_then_else>
   * </modifiers>
   */
  
  [Test()]
  public void TestGetsIfThenElse()
  {
    XCRMParser parser = new XCRMParser();
    IXMLNode ixmlnode = mockery.NewMock<IXMLNode>();
    List<IXMLNode> if_then_else_nodes = new List<IXMLNode>();
    IXMLNode if_node = mockery.NewMock<IXMLNode>();
    IXMLNode then_node = mockery.NewMock<IXMLNode>();
    IXMLNode else_node = mockery.NewMock<IXMLNode>();
    Expect.AtLeastOnce.On(if_node).GetProperty("Name").Will(Return.Value("if"));
    Expect.AtLeastOnce.On(then_node).GetProperty("Name").Will(Return.Value("then"));
    Expect.AtLeastOnce.On(else_node).GetProperty("Name").Will(Return.Value("else"));
    if_then_else_nodes.Add(if_node);
    if_then_else_nodes.Add(then_node);
    if_then_else_nodes.Add(else_node);
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Name").Will(Return.Value("if_then_else"));
    Expect.AtLeastOnce.On(ixmlnode).GetProperty("Children").Will(Return.Value(if_then_else_nodes));
    
    parser.crm = mockery.NewMock<IXCRMParser>();
    List<Roar.DomainObjects.Requirement> mock_if_requirement_list = new List<Roar.DomainObjects.Requirement>();
    List<Roar.DomainObjects.Modifier> mock_then_modifier_list = new List<Roar.DomainObjects.Modifier>();
    List<Roar.DomainObjects.Modifier> mock_else_modifier_list = new List<Roar.DomainObjects.Modifier>();
    Expect.AtLeastOnce.On(parser.crm).Method("ParseRequirementList").With(if_node).Will(Return.Value(mock_if_requirement_list));
    Expect.AtLeastOnce.On(parser.crm).Method("ParseModifierList").With(then_node).Will(Return.Value(mock_then_modifier_list));
    Expect.AtLeastOnce.On(parser.crm).Method("ParseModifierList").With(else_node).Will(Return.Value(mock_else_modifier_list));
    
    Modifier m = parser.ParseAModifier(ixmlnode);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as IfThenElse);
    Assert.AreSame((m as IfThenElse).if_, mock_if_requirement_list);
    Assert.AreSame((m as IfThenElse).then_, mock_then_modifier_list);
    Assert.AreSame((m as IfThenElse).else_, mock_else_modifier_list);
  }
  
}
