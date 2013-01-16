using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.Components;
using Roar.DomainObjects;
using Roar.DomainObjects.Modifiers;
using Roar.DataConversion;

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
   * This is what the Remove Items XML should look like:
   * <modifiers>
       <remove_items ikey="christmas_tree" count="15"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestRemoveItems()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement= RoarExtensions.CreateXmlElement("<remove_items ikey=\"christmas_tree\" count=\"15\"/>");

    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as RemoveItems);
    Assert.AreEqual((m as RemoveItems).ikey, "christmas_tree");
    Assert.AreEqual((m as RemoveItems).count, 15);
  }
  
  /*
   * This is what the Grant Stat XML should look like:
   * <modifiers>
       <grant_stat type="attribute" ikey="_energy_regen_amount" value="5"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestGrantStat()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<grant_stat type=\"attribute\" ikey=\"_energy_regen_amount\" value=\"5\"/>");

    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantStat);
    Assert.AreEqual((m as GrantStat).ikey, "_energy_regen_amount");
    Assert.AreEqual((m as GrantStat).type, "attribute");
    Assert.AreEqual((m as GrantStat).value, 5);
  }
  
  /*
   * This is what the Grant Stat Range XML should look like:
   * <modifiers>
       <grant_stat_range type="currency" ikey="premium_web" min="3" max="7"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestGrantStatRange()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<grant_stat_range type=\"currency\" ikey=\"premium_web\" min=\"3\" max=\"7\"/>");
    GrantStatRange grant_stat_range = new GrantStatRange();
    grant_stat_range.type = "currency";
    grant_stat_range.ikey = "premium_web";
    grant_stat_range.min = 3;
    grant_stat_range.max = 7;
    
    
    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantStatRange);
    Assert.AreEqual((m as GrantStatRange).ikey, grant_stat_range.ikey);
    Assert.AreEqual((m as GrantStatRange).type, grant_stat_range.type);
    Assert.AreEqual((m as GrantStatRange).min, grant_stat_range.min);
    Assert.AreEqual((m as GrantStatRange).max, grant_stat_range.max);
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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<grant_item ikey=\"christmas_tree\"/>");
    
    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantItem);
    Assert.AreEqual((m as GrantItem).ikey, "christmas_tree");
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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<grant_xp value=\"25\"/>");
    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantXp);
    Assert.AreEqual((m as GrantXp).value, 25);
  }
  
  /*
   * This is what the Grant XP Range XML should look like:
   * <modifiers>
       <grant_xp_range min="33" max="44"/>
   * </modifiers>
   */
  
  [Test()]
  public void TestGrantXPRange()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<grant_xp_range min=\"33\" max=\"44\"/>");

    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m as GrantXpRange);
    Assert.AreEqual((m as GrantXpRange).min, 33);
    Assert.AreEqual((m as GrantXpRange).max, 44);
  }
  
  /*
   * This is what the Random Choice XML should look like:
   * <modifiers>
   *   <random_choice>
   *     <choice weight="78">
   *       <modifier>
   *         <nothing/>
   *         <grant_xp value="56"/>
   *       </modifier>
   *       <requirement>
   *         <and ok="false" reason="always fails">
   *           <true_requirement ok="true"/>
   *           <false_requirement ok="false" reason="always fails"/>
   *         </and>
   *       </requirement>
   *     </choice>
   *     <choice weight="12">
   *       <modifier>
   *         <nothing/>
   *       </modifier>
   *       <requirement>
   *         <and ok="false" reason="always fails">
   *           <true_requirement ok="true"/>
   *         </and>
   *       </requirement>
   *     </choice>
   *   </random_choice>
   * </modifiers>
   */
  
  [Test()]
  public void TestRandomChoice()
  {
    XCRMParser parser = new XCRMParser();
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
		"<random_choice>" +
			"<choice weight=\"78\">"+
				"<modifier/>"+
				"<requirement/>"+
			"</choice>"+
			"<choice weight=\"12\">"+
				"<modifier/>"+
				"<requirement/>"+
			"</choice>"+
		"</random_choice>"
		);
	Assert.IsNotNull(xmlelement);
		
	List<Modifier> rc1_modifiers = new List<Modifier>();
	System.Xml.XmlNode rc1_modifier_node = xmlelement.SelectSingleNode("./choice[1]/modifier");
	Assert.IsNotNull(rc1_modifier_node);
		
	List<Requirement> rc1_requirements = new List<Requirement>();
	System.Xml.XmlNode rc1_requirement_node = xmlelement.SelectSingleNode("./choice[1]/requirement");
	Assert.IsNotNull(rc1_requirement_node);

	
	List<Modifier> rc2_modifiers = new List<Modifier>();
	System.Xml.XmlNode rc2_modifier_node = xmlelement.SelectSingleNode("./choice[2]/modifier");
	Assert.IsNotNull(rc2_modifier_node);


	List<Requirement> rc2_requirements = new List<Requirement>();
	System.Xml.XmlNode rc2_requirement_node = xmlelement.SelectSingleNode("./choice[2]/requirement");
	Assert.IsNotNull(rc2_requirement_node);

		
	parser.crm = mockery.NewMock<IXCRMParser>();

    Expect.Once.On(parser.crm).Method("ParseModifierList").With(rc1_modifier_node).Will(Return.Value(rc1_modifiers));
    Expect.Once.On(parser.crm).Method("ParseRequirementList").With(rc1_requirement_node).Will(Return.Value(rc1_requirements));
    Expect.Once.On(parser.crm).Method("ParseModifierList").With(rc2_modifier_node).Will(Return.Value(rc2_modifiers));
    Expect.Once.On(parser.crm).Method("ParseRequirementList").With(rc2_requirement_node).Will(Return.Value(rc2_requirements));
		
    RandomChoice m = parser.ParseAModifier(xmlelement) as RandomChoice;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m);
    Assert.AreEqual(2,m.choices.Count);
	Assert.AreEqual(78, m.choices[0].weight);
	Assert.AreSame(rc1_modifiers, m.choices[0].modifiers);
	Assert.AreSame(rc1_requirements, m.choices[0].requirements);
	Assert.AreEqual(12, m.choices[1].weight);
	Assert.AreSame(rc2_modifiers, m.choices[1].modifiers);
	Assert.AreSame(rc2_requirements, m.choices[1].requirements);
		
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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement("<nothing/>");
    Modifier m = parser.ParseAModifier(xmlelement);
    mockery.VerifyAllExpectationsHaveBeenMet();
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
    System.Xml.XmlElement xmlelement = RoarExtensions.CreateXmlElement(
      "<if_then_else>"+
        "<if>"+
        "</if>"+
        "<then>"+
        "</then>"+
        "<else>"+
        "</else>"+
      "</if_then_else>"
    );
		
    System.Xml.XmlNode if_node = xmlelement.SelectSingleNode("./if");
    System.Xml.XmlNode then_node = xmlelement.SelectSingleNode("./then");
    System.Xml.XmlNode else_node = xmlelement.SelectSingleNode("./else");

    parser.crm = mockery.NewMock<IXCRMParser>();
    List<Roar.DomainObjects.Requirement> mock_if_requirement_list = new List<Roar.DomainObjects.Requirement>();
    List<Roar.DomainObjects.Modifier> mock_then_modifier_list = new List<Roar.DomainObjects.Modifier>();
    List<Roar.DomainObjects.Modifier> mock_else_modifier_list = new List<Roar.DomainObjects.Modifier>();
    Expect.AtLeastOnce.On(parser.crm).Method("ParseRequirementList").With(if_node).Will(Return.Value(mock_if_requirement_list));
    Expect.AtLeastOnce.On(parser.crm).Method("ParseModifierList").With(then_node).Will(Return.Value(mock_then_modifier_list));
    Expect.AtLeastOnce.On(parser.crm).Method("ParseModifierList").With(else_node).Will(Return.Value(mock_else_modifier_list));
    
    IfThenElse m = parser.ParseAModifier(xmlelement) as IfThenElse;
    mockery.VerifyAllExpectationsHaveBeenMet();
    Assert.IsNotNull(m);
    Assert.AreSame(m.if_, mock_if_requirement_list);
    Assert.AreSame(m.then_, mock_then_modifier_list);
    Assert.AreSame(m.else_, mock_else_modifier_list);
  }
  
}
