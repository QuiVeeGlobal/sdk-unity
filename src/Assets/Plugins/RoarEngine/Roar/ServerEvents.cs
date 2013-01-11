using System.Collections.Generic;
using System.Linq;
using Roar.DataConversion;
using Roar.Events;
using UnityEngine;

namespace Roar
{
	namespace Events
	{

		public class UpdateEvent
		{
			public string type;
			public string ikey;
			public string val;
			
			public static UpdateEvent CreateFromXml(IXMLNode n)
			{
				Events.UpdateEvent e = new Events.UpdateEvent();
				
				e.type = n.GetAttribute("type");
				e.ikey = n.GetAttribute("ikey");
				e.val = n.GetAttribute("value");
				
				return e;
			}
		}
		
		public class ItemUseEvent
		{
			public string item_id;
			
			public static ItemUseEvent CreateFromXml(IXMLNode n)
			{
				Events.ItemUseEvent e = new Events.ItemUseEvent();
				e.item_id = n.GetAttribute("item_id");
				return e;
				
			}
		}
		
		public class ItemLoseEvent
		{
			public string item_id;
			public string item_ikey;
				
			public static ItemLoseEvent CreateFromXml(IXMLNode n)
			{
				Events.ItemLoseEvent e = new Events.ItemLoseEvent();
				
				e.item_id = n.GetAttribute("item_id");
				e.item_ikey = n.GetAttribute("item_ikey");
				return e;
				
			}
			
		}
		
		public class InventoryChangedEvent
		{
			public static InventoryChangedEvent CreateFromXml(IXMLNode n)
			{
				return new InventoryChangedEvent();
				
			}
		}
		
		public class RegenEvent
		{
			public string name;
			public string next;
				
			public static RegenEvent CreateFromXml(IXMLNode n)
			{
				Events.RegenEvent e = new Events.RegenEvent();
				
				e.name = n.GetAttribute("name");
				e.next = n.GetAttribute("next");
				return e;
			}
		}
		
		public class ItemAddEvent
		{
			public string item_id;
			public string item_ikey;
				
			public static ItemAddEvent CreateFromXml(IXMLNode n)
			{
				Events.ItemAddEvent e = new Events.ItemAddEvent();

				e.item_id = n.GetAttribute("item_id");
				e.item_ikey = n.GetAttribute("item_ikey");
				return e;
				
			}
		}
		
		public class TaskCompleteEvent
		{
			public DomainObjects.Task task;
			
			public static TaskCompleteEvent CreateFromXml(IXMLNode n)
			{
				Events.TaskCompleteEvent e = new Events.TaskCompleteEvent();
				e.task = DomainObjects.Task.CreateFromXml(n, new XCRMParser());

				return e;
				
			}
			
		}
		
		public class AchievementCompleteEvent
		{
			public string ikey;
			public string label;
			public string progress_count;
			public string steps;
			public string description;
			public string task_ikey;
			public string task_label;
			
			
			public static AchievementCompleteEvent CreateFromXml(IXMLNode n)
			{
				
				Events.AchievementCompleteEvent e = new Events.AchievementCompleteEvent();

				e.ikey = n.GetAttribute("ikey");
				e.steps = n.GetAttribute("steps");
				e.label = n.GetAttribute("label");
				e.progress_count = n.GetAttribute("progress_count");
				e.description = n.GetAttribute("description");
				e.task_ikey = n.GetAttribute("job_ikey");
				e.task_label = n.GetAttribute("job_label");

				return e;
			}
			
		}

		public class LevelUpEvent
		{
			public string val;
			public static LevelUpEvent CreateFromXml(IXMLNode n)
			{
				LevelUpEvent e = new LevelUpEvent();
				
				e.val = n.GetAttribute("value");
				
				return e;
				
			}
			
		}
		
		public class CollectChangedEvent
		{
			public string ikey;
			public string next;
			
			public static CollectChangedEvent CreateFromXml(IXMLNode n)
			{
				CollectChangedEvent e = new CollectChangedEvent();
				
				e.ikey = n.GetAttribute("ikey");
				e.next = n.GetAttribute("next");
				
				return e;
			}
		}
		
		public class InviteAcceptedEvent
		{
			public string name;
			public string player_id;
			public string level;
			
			public static InviteAcceptedEvent CreateFromXml(IXMLNode n)
			{
				InviteAcceptedEvent e = new InviteAcceptedEvent();
				
				e.name = n.GetAttribute("name");
				e.player_id = n.GetAttribute("player_id");
				e.level = n.GetAttribute("level");

				return e;
			}
			
		}
		
		public class FriendRequestEvent
		{
			public string name;
			public string from_player_id;
			public string level;
			public string friend_invite_row_id;
			
			public static FriendRequestEvent CreateFromXml(IXMLNode n)
			{
				FriendRequestEvent e = new FriendRequestEvent();

				e.name = n.GetAttribute("name");
				e.from_player_id = n.GetAttribute("from_player_id");
				e.level = n.GetAttribute("level");
				e.friend_invite_row_id = n.GetAttribute("friend_invite_row_id");

				return e;
			}
		}
		
		public class TransactionEvent
		{
			public string ikey;
			public string val;
			
			public static TransactionEvent CreateFromXml(IXMLNode n)
			{
				TransactionEvent e = new TransactionEvent();

				e.ikey = n.GetAttribute("ikey");
				e.val = n.GetAttribute("value");

				return e;
			}
		}
		
		public class MailInEvent
		{
			public string sender_id;
			public string sender_name;
			
			public static MailInEvent CreateFromXml(IXMLNode n)
			{
				MailInEvent e = new MailInEvent();
				
				e.sender_id = n.GetAttribute("sender_id");
				e.sender_name = n.GetAttribute("sender_name");
				
				return e;
			}
		}
		
		public class EquipEvent
		{
			public string item_id;
			
			public static EquipEvent CreateFromXml(IXMLNode n)
			{
				EquipEvent e = new EquipEvent();
				
				e.item_id = n.GetAttribute("item_id");
				
				return e;
			}
			
		}
		
		public class UnequipEvent
		{
			public string item_id;
			
			public static UnequipEvent CreateFromXml(IXMLNode n)
			{
				UnequipEvent e = new UnequipEvent();
				
				e.item_id = n.GetAttribute("item_id");
				return e;
			}
		}
		
		public class ScriptEvent
		{
			public string key;
			public string val;
			
			public static ScriptEvent CreateFromXml(IXMLNode n)
			{
				ScriptEvent e = new ScriptEvent();
				
				e.key = n.GetAttribute("key");
				e.val = n.GetAttribute("value");
				
				return e;
			}
			
		}
		
		public class ChromeWebStoreEvent
		{
			public string ikey;
			public string transaction_id;
			public List<string> costsList;
			public List<string> modifierList;
			
			public static ChromeWebStoreEvent CreateFromXml(IXMLNode n)
			{
				ChromeWebStoreEvent e = new ChromeWebStoreEvent();
				e.costsList = new List<string>();
				e.modifierList = new List<string>();
				e.ikey = n.GetAttribute("ikey");
				e.transaction_id = n.GetAttribute("transaction_id");

				IXMLNode costsNode = n.GetNode("costs>0");
				
				foreach (IXMLNode nn in costsNode.Children)
				{
					e.costsList.Add (nn.Text);
				}
				
				IXMLNode attributesNode = n.GetNode("modifiers>0");
				
				foreach (IXMLNode nn in attributesNode.Children)
				{
					e.modifierList.Add (nn.Text);
				}
				
				return e;
			}
			
		}
		
		
		
		
	}
}
