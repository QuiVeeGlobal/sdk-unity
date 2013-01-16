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
	
		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.UpdateEvent> callback = (Roar.Events.UpdateEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.ikey, "health");
			StringAssert.IsMatch(eve.type, "core");
			StringAssert.IsMatch(eve.val, "120");
		};

		RoarManager.roarServerUpdateEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerUpdateEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );

	}
	
	[Test()]
	public void TestItemUse()
	{
		string xml =
			@"<item_use item_id=""1234""/>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.ItemUseEvent> callback = (Roar.Events.ItemUseEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.item_id, "1234");
		};

		RoarManager.roarServerItemUseEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerItemUseEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );

	}
	
	[Test()]
	public void TestItemLose()
	{
		string xml =
			@"<item_lose item_id=""1234"" item_ikey=""somthing""/>";
	
		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.ItemLoseEvent> callback = (Roar.Events.ItemLoseEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.item_id, "1234");
			StringAssert.IsMatch(eve.item_ikey, "somthing");
		};

		RoarManager.roarServerItemLoseEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerItemLoseEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );

	}
	
	[Test()]
	public void TestInventoryChanged()
	{
		string xml =
			@"<inventory_changed />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.InventoryChangedEvent> callback = (Roar.Events.InventoryChangedEvent eve) =>
		{
			eventTriggered = true;
		};

		RoarManager.roarServerInventoryChangedEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerInventoryChangedEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}
	
	[Test()]
	public void TestRegen()
	{
		
		string xml =
			@"<regen name=""health"" next=""12313231"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.RegenEvent> callback = (Roar.Events.RegenEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.name, "health");
			StringAssert.IsMatch(eve.next, "12313231");
		};

		RoarManager.roarServerRegenEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerRegenEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}
	
	[Test()]
	public void TestItemAdd()
	{
		string xml =
			@"<item_add item_id=""1234"" item_ikey=""somthing""/>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.ItemAddEvent> callback = (Roar.Events.ItemAddEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.item_id, "1234");
			StringAssert.IsMatch(eve.item_ikey, "somthing");
		};

		RoarManager.roarServerItemAddEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerItemAddEvent -= callback;
		}

		  Assert.IsTrue( eventTriggered );
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
	      <stat_cost ikey=""premium_currency"" type=""Health"" value=""10"" />
	    </costs>
	    <modifiers>
	      <add_xp value=""3""/>
	      <stat_change ikey=""coins"" value=""1500""/>
	    </modifiers>
	    <mastery level=""3"" progress=""100""/>
	  </task_complete>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.TaskCompleteEvent> callback = (Roar.Events.TaskCompleteEvent eve) =>
		{
			eventTriggered = true;
			//StringAssert.IsMatch(eve.task, "health");
		};

		RoarManager.roarServerTaskCompleteEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerTaskCompleteEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}
	
	[Test()]
	public void TestAchievementComplete()
	{
		string xml =
			@"<achievement_complete ikey=""some_achievement""
				progress_count=""2""
				steps=""10""
				description=""An achievement you need to do 10 times to complete""
				label=""An example achievement""
				job_ikey=""job_ikey""
				job_label=""The job label""
			/>";

		bool eventTriggered = false;

		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);

		Action<Roar.Events.AchievementCompleteEvent> callback = (Roar.Events.AchievementCompleteEvent eve) =>
		{
			Console.Out.Write("test2");
			eventTriggered = true;
			StringAssert.IsMatch(eve.ikey, "some_achievement");
			StringAssert.IsMatch(eve.progress_count, "2");
			StringAssert.IsMatch(eve.steps, "10");
			StringAssert.IsMatch(eve.description, "An achievement you need to do 10 times to complete");
			StringAssert.IsMatch(eve.label, "An example achievement");
			StringAssert.IsMatch(eve.task_ikey, "job_ikey");
			StringAssert.IsMatch(eve.task_label, "The job label");
		};

		RoarManager.roarServerAchievementCompleteEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerAchievementCompleteEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );

	}
	
	[Test()]
	public void TestLevelUp()
	{
		
		string xml =
			@"<level_up value=""5"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.LevelUpEvent> callback = (Roar.Events.LevelUpEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.val, "5");
		};

		 RoarManager.roarServerLevelUpEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerLevelUpEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}
	[Test()]
	public void TestCollectChanged()
	{
		string xml =
			@"<collect_changed ikey=""health"" next=""12313231"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.CollectChangedEvent> callback = (Roar.Events.CollectChangedEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.ikey, "health");
			StringAssert.IsMatch(eve.next, "12313231");
		};

		RoarManager.roarServerCollectChangedEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerCollectChangedEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestInviteAccepted()
	{
		string xml =
			@"<invite_accepted name=""Lex Luthor"" player_id=""12313231"" level=""123"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.InviteAcceptedEvent> callback = (Roar.Events.InviteAcceptedEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.name, "Lex Luthor");
			StringAssert.IsMatch(eve.player_id, "12313231");
			StringAssert.IsMatch(eve.level, "123");
		};

		RoarManager.roarServerInviteAcceptedEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerInviteAcceptedEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestFriendRequest()
	{
		string xml =
			@"<friend_request name=""Lex Luthor"" from_player_id=""12313231"" level=""123"" friend_invite_row_id=""12341345"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.FriendRequestEvent> callback = (Roar.Events.FriendRequestEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.name, "Lex Luthor");
			StringAssert.IsMatch(eve.from_player_id, "12313231");
			StringAssert.IsMatch(eve.level, "123");
			StringAssert.IsMatch(eve.friend_invite_row_id, "12341345");
		};

		RoarManager.roarServerFriendRequestEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerFriendRequestEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}
	[Test()]
	public void TestTransaction()
	{
		string xml =
			@"<transaction ikey=""diamonds"" value=""120"" />";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.TransactionEvent> callback = (Roar.Events.TransactionEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.ikey, "diamonds");
			StringAssert.IsMatch(eve.val, "120");
		};

		 RoarManager.roarServerTransactionEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerTransactionEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestMailIn()
	{
		string xml =
			@"<mail_in/>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);

		Action<Roar.Events.MailInEvent> callback = (Roar.Events.MailInEvent eve) =>
		{
			eventTriggered = true;
		};

		RoarManager.roarServerMailInEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerMailInEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestEquip()
	{
		string xml =
			@"<equip item_id=""1234""/>";

		bool eventTriggered = false;

		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.EquipEvent> callback = (Roar.Events.EquipEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.item_id, "1234");
		};

		RoarManager.roarServerEquipEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerEquipEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestUnequip()
	{
		string xml =
			@"<unequip item_id=""1234""/>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.UnequipEvent> callback = (Roar.Events.UnequipEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.item_id, "1234");
		};

		RoarManager.roarServerUnequipEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerUnequipEvent -= callback;
		}

		Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestScript()
	{
		string xml =
			@"<script key=""abc"" value=""blah""/>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.ScriptEvent> callback = (Roar.Events.ScriptEvent eve) =>
		{
			eventTriggered = true;
			StringAssert.IsMatch(eve.key, "abc");
			StringAssert.IsMatch(eve.val, "blah");
		};

		RoarManager.roarServerScriptEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerScriptEvent -= callback;
		}

		  Assert.IsTrue( eventTriggered );
	}

	[Test()]
	public void TestChromeWebStore()
	{
		string xml =
			@"<chrome_web_store ikey=""abc"" transaction_id=""abc"">
	    <costs>
	    </costs>
		<modifiers>
	    </modifiers>
	  </chrome_web_store>";

		bool eventTriggered = false;
		System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
		Action<Roar.Events.ChromeWebStoreEvent> callback = (Roar.Events.ChromeWebStoreEvent eve) =>
		{
			eventTriggered = true;
		};

		RoarManager.roarServerChromeWebStoreEvent += callback;

		try
		{
			RoarManager.OnServerEvent(nn);
		}
		finally
		{
			RoarManager.roarServerChromeWebStoreEvent -= callback;
		}

		  Assert.IsTrue( eventTriggered );
	}


}
