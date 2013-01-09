using System.Collections.Generic;
using System.Linq;
using Roar.DataConversion;
using Roar.Events;

namespace Roar
{
	namespace Events
	{
//		case "update":
//				OnRoarServerUpdate(info);
//				break;
//			case "item_use":
//				OnRoarServerItemUse(info);
//				break;
//			case "item_lose":
//				OnRoarServerItemLose(info);
//				break;
//			case "inventory_changed":
//				OnRoarServerInventoryChanged(info);
//				break;
//			case "regen":
//				OnRoarServerRegen(info);
//				break;
//			case "item_add":
//				OnRoarServerItemAdd(info);
//				break;
//			case "task_complete":
//				OnRoarServerTaskComplete(info);
//				break;
//			case "achievement_complete":
//				OnRoarServerAchievementComplete(Roar.DomainObjects.Achievement.CreateFromXml(info));
//				break;
//			case "level_up":
//				OnRoarServerLevelUp(info);
//				break;
//			case "collect_changed":
//				OnRoarServerCollectChanged(info);
//				break;
//			case "invite_accepted":
//				OnRoarServerInviteAccepted(info);
//				break;
//			case "friend_request":
//				OnRoarServerFriendRequest(info);
//				break;
//			case "transaction":
//				OnRoarServerTransaction(info);
//				break;
//			case "mail_in":
//				OnRoarServerMailIn(info);
//				break;
//			case "equip":
//				OnRoarServerEquip(info);
//				break;
//			case "unequip":
//				OnRoarServerUnequip(info);
//				break;
//			case "script":
//				OnRoarServerScript(info);
//				break;
//			case "chrome_web_store":
//				OnRoarServerChromeWebStore(info);
//				break;
//		
		public class UpdateEvent
		{
			public string type;
			public string ikey;
			public string val;
			
			public static UpdateEvent CreateFromXml(IXMLNode n)
			{
				Events.UpdateEvent e = new Events.UpdateEvent();
				//System.Console.Out.WriteLine("update create" + n.ToString());
				
				//e.type = n.GetAttribute("type");
				//e.ikey = n.GetAttribute("ikey");
				//e.val = n.GetAttribute("value");
				
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
				IXMLNode node=n.GetNode("item_lose>0");
				
				e.item_id = node.GetAttribute("item_id");
				e.item_ikey = node.GetAttribute("item_ikey");
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
				IXMLNode node=n.GetNode("item_add>0");
				
				e.item_id = node.GetAttribute("item_id");
				e.item_ikey = node.GetAttribute("item_ikey");
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
				
				IXMLNode node = n.GetNode("achievement_complete>0");
				if (node != null)
				{
					e.ikey = node.GetAttribute("ikey");
				}
				if (node != null)
				{
					e.steps = node.GetAttribute("steps");
				}
				if (node != null)
				{
					e.label = node.GetAttribute("label");
				}
				if (node != null)
				{
					e.progress_count = node.GetAttribute("progress_count");
				}
				if (node != null)
				{
					e.description = node.GetAttribute("description");
				}
				if (node != null)
				{
					e.task_ikey = node.GetAttribute("task_ikey");
				}
				if (node != null)
				{
					e.task_label = node.GetAttribute("task_label");
				}
				
				return e;
			}
			
		}
		
		public class LevelUpEvent
		{
			public string level_up;
			public static LevelUpEvent CreateFromXml(IXMLNode n)
			{
				LevelUpEvent e = new LevelUpEvent();
				
				IXMLNode node=n.GetNode("levelup>0");
				if( node != null)
				{
					e.level_up = node.GetAttribute("value");
				}
				
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
				
				IXMLNode node =n.GetNode("collect_changed>0");
				if(node != null)
				{
					e.ikey = node.GetAttribute("ikey");
					e.next = node.GetAttribute("next");
				}
				
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
				
				IXMLNode node =n.GetNode("invite_accepted>0");
				if(node != null)
				{
					e.name = node.GetAttribute("name");
					e.player_id = node.GetAttribute("player_id");
					e.level = node.GetAttribute("level");
				}
				
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
				
				IXMLNode node =n.GetNode("friend_request>0");
				if(node != null)
				{
					e.name = node.GetAttribute("name");
					e.from_player_id = node.GetAttribute("from_player_id");
					e.level = node.GetAttribute("level");
					e.friend_invite_row_id = node.GetAttribute("friend_invite_row_id");
				}
				
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
				
				IXMLNode node =n.GetNode("transaction>0");
				if(node != null)
				{
					e.ikey = node.GetAttribute("ikey");
					e.val = node.GetAttribute("value");
				}
				
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
				
				IXMLNode node =n.GetNode("mail_in>0");
				if(node != null)
				{
					e.sender_id = node.GetAttribute("sender_id");
					e.sender_name = node.GetAttribute("sender_name");
				}
				
				return e;
			}
		}
		
		public class EquipEvent
		{
			public string item_id;
			
			public static EquipEvent CreateFromXml(IXMLNode n)
			{
				EquipEvent e = new EquipEvent();
				
				IXMLNode node =n.GetNode("equip>0");
				if(node != null)
				{
					e.item_id = node.GetAttribute("item_id");
				}
				
				return e;
			}
			
		}
		
		public class UnequipEvent
		{
			public string item_id;
			
			public static UnequipEvent CreateFromXml(IXMLNode n)
			{
				UnequipEvent e = new UnequipEvent();
				
				IXMLNode node =n.GetNode("unequip>0");
				if(node != null)
				{
					e.item_id = node.GetAttribute("item_id");
				}
				
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
				
				IXMLNode node =n.GetNode("script>0");
				if(node != null)
				{
					e.key = node.GetAttribute("key");
					e.val = node.GetAttribute("value");
				}
				
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
				IXMLNode node =n.GetNode("chrome_web_store>0");
				if(node != null)
				{
					e.ikey = node.GetAttribute("ikey");
					e.transaction_id = node.GetAttribute("transaction_id");
				}
				
				IXMLNode costsNode = node.GetNode("costs>0");
				
				foreach (IXMLNode nn in costsNode.Children)
				{
					e.costsList.Add (nn.Text);
				}
				
				IXMLNode attributesNode = node.GetNode("modifiers>0");
				
				foreach (IXMLNode nn in attributesNode.Children)
				{
					e.modifierList.Add (nn.Text);
				}
				
				return e;
			}
			
		}
		
		
		
		
	}
}