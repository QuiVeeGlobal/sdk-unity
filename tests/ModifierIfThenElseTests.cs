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
 * Test cases for the Modifier If-Then-Else component
 **/

[TestFixture()]
public class ModifierIfThenElseTest
{

  private Mockery mockery = null;

  [SetUp]
  public void TestInitialise()
  {
    this.mockery = new Mockery();
  }
  
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
    
    List<IXMLNode> if_nodes = new List<IXMLNode>();
    IXMLNode if_and_node = mockery.NewMock<IXMLNode>();
    if_nodes.Add(if_and_node);
    IXMLNode if_other_requirement = mockery.NewMock<IXMLNode>();
    if_nodes.Add(if_other_requirement);
    Expect.AtLeastOnce.On(if_node).GetProperty("Children").Will(Return.Value(if_nodes));
    Expect.AtLeastOnce.On(if_and_node).GetProperty("Name").Will(Return.Value("and"));
    Expect.AtLeastOnce.On(if_and_node).GetProperty("Children").Will(Return.Value(new List<IXMLNode>()));
    Expect.AtLeastOnce.On(if_other_requirement).GetProperty("Name").Will(Return.Value("true"));
    
    Expect.AtLeastOnce.On(then_node).GetProperty("Children").Will(Return.Value(new List<IXMLNode>()));
    Expect.AtLeastOnce.On(else_node).GetProperty("Children").Will(Return.Value(new List<IXMLNode>()));
    
    Modifier m = parser.ParseAModifier(ixmlnode);
    Assert.IsNotNull(m as IfThenElse);
  }
  
}
