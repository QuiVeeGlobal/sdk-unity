using System;
using NUnit.Framework;
using NMock2;
using Roar.DataConversion;
using Roar.DomainObjects;
using Roar.DomainObjects.Costs;


[TestFixture()]
public class ServerEventsTest
{
	[SetUp]
	public void TestInitialise()
	{
	}
	
	[Test()]
	public void TestUpdate()
	{
		string xml =
			@"<update type=""core"" ikey=""health"" value=""120"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
	}
	
	[Test()]
	public void TestItemUse()
	{
		string xml =
			@"<item_use item_id=""1234""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	
	[Test()]
	public void TestItemLose()
	{
		string xml =
			@"<item_lose item_id=""1234"" item_ikey=""somthing""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	
	[Test()]
	public void TestInventoryChanged()
	{
		string xml =
			@"<inventory_changed />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	
	[Test()]
	public void TestRegen()
	{
		
		string xml =
			@"<regen name=""health"" next=""12313231"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
	}
	
	[Test()]
	public void TestItemAdd()
	{
		string xml =
			@"<item_add item_id=""1234"" item_ikey=""somthing""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	
	[Test()]
	public void TestTaskComplete()
	{
		
		string xml =
			@"<task_complete>
	    <ikey>task_unique_ikey</ikey>
	    <label>Label for the task, set by the developer</label>
	    <description>Description of the task, as set by the developer.</description>
	    <location>Location set by the developer.</location>
	    <tags>
	      <tag value=""blah""/>
	    </tags>
	    <costs>
	      <stat_change ikey=""premium_currency"" value=""10"" />
	    </costs>
	    <modifiers>
	      <add_xp value=""3""/>
	      <stat_change ikey=""coins"" value=""1500""/>
	    </modifiers>
	    <mastery level=""3"" progress=""100""/>
	  </task_complete>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
	}
	
	[Test()]
	public void TestAchievementComplete()
	{
		string xml =
			@" <achievement_complete
             ikey=""some_achievement""
             progress_count=""2""
	     steps=""10""
 	     description=""An achievement you need to do 10 times to complete""
	     label=""An example achievement""
	     job_ikey=""job_ikey""
	     job_label=""The job label""
	     />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	
	[Test()]
	public void TestLevelUp()
	{
		
		string xml =
			@"<level_up value=""5"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
	}
	[Test()]
	public void TestCollectChanged()
	{
		string xml =
			@"<collect_changed ikey=""health"" next=""12313231"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestInviteAccepted()
	{
		string xml =
			@"<invite_accepted name=""Lex Luthor"" player_id=""12313231"" level=""123"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
		
	}
	[Test()]
	public void TestFriendRequest()
	{
		string xml =
			@"<friend_request name=""Lex Luthor"" from_player_id=""12313231"" level=""123"" friend_invite_row_id=""12341345"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestTransaction()
	{
		string xml =
			@"<transaction ikey=""diamonds"" value=""120"" />";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestMailIn()
	{
		string xml =
			@"<mail_in/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestEquip()
	{
		string xml =
			@"<equip item_id=""1234""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestUnequip()
	{
		string xml =
			@"<unequip item_id=""1234""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestScript()
	{
		string xml =
			@"<script key=""abc"" value=""blah""/>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		
		RoarManager.OnServerEvent(nn);
		
	}
	[Test()]
	public void TestChromeWebStore()
	{
		
		string xml =
			@"<chrome_web_store ikey=""abc"" transaction_id=""abc"">
	    <costs>
	    </costs>
	  </chrome_web_store>";
	
		IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
		//Not sure what the exact XMLNode is supposed to be.
		RoarManager.OnServerEvent(nn);
	}
	
	
	
	
}
