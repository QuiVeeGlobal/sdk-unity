using System.Collections.Generic;
using System.Linq;
using Roar.DataConversion;

namespace Roar
{
	namespace DomainObjects
	{
	
		public abstract class Cost
		{
			public bool ok;
			public string reason;
		};
		
		public abstract class Requirement
		{
			public bool ok;
			public string reason;
		};
		
		public abstract class Modifier
		{
		};
		
		public abstract class ItemStat
		{
			public string ikey;
			public int value;
			public ItemStat (string ikey, int value)
			{
				this.ikey = ikey;
				this.value = value;
			}
		};
		
		
		namespace Costs
		{
			public class Multiple : Cost
			{
				public List<Cost> costs;
			}
			
			public class Stat : Cost
			{
				public string ikey;
				public string type;
				public int value;
			}
			
			public class Item : Cost
			{
				public string ikey;
				public int number_required;
			}
		}
		
		namespace Modifiers
		{
			public class RemoveItems : Modifier
			{
				public string ikey;
				public int count;
			}
			
			public class GrantStat : Modifier
			{
				public string ikey;
				public string type;
				public int value;
			}
			
			
			public class GrantStatRange : Modifier
			{
				public string ikey;
				public string type;
				public int min;
				public int max;
			}
			
			public class GrantXp : Modifier
			{
				public int value;
			}
			
			public class GrantXpRange : Modifier
			{
				public int min;
				public int max;
			}
			
			public class GrantItem : Modifier
			{
				public string ikey;
			}
			
			public class Nothing : Modifier
			{
			}
			
			public class NamedReference : Modifier
			{
			}
			
			public class Script : Modifier
			{
			}
			
			public class Multiple : Modifier
			{
				public List<Modifier> modifier;
			}
			
			public class IfThenElse : Modifier
			{
				public IList<Requirement> if_;
				public IList<Modifier> then_;
				public IList<Modifier> else_;
			}
			
			public class RandomChoice : Modifier
			{
				public struct ChoiceEntry
				{
					public int weight;
					public IList<Modifier> modifiers;
					public IList<Requirement> requirements;
				}
				
				public List<ChoiceEntry> choices;
			}
			
		}
		
		namespace Requirements
		{
			public class Level : Requirement
			{
				public int level;
			}
			
			public class Item : Requirement
			{
				public string ikey;
				public int number_required;
			}
			
			public class Stat : Requirement
			{
				public string ikey;
				public string type;
				public int value;
			}
			
			public class Multiple : Requirement
			{
				public List<Requirement> requirements;
			}
			
			public class True : Requirement
			{
			}
			
			public class False : Requirement
			{
			}
			
			public class Friends : Requirement
			{
				public int required;
			}
		}
		
		namespace ItemStats
		{
			public class RegenStat : ItemStat
			{
				public int every;
				
				public RegenStat (string ikey, int value) : base (ikey, value)
				{
				}
			}
			
			public class RegenStatLimited : ItemStat
			{
				public int repeat;
				public int times_used;
				
				public RegenStatLimited (string ikey, int value) : base (ikey, value)
				{
				}
			}
			
			public class GrantStat : ItemStat
			{
				public GrantStat (string ikey, int value) : base (ikey, value)
				{
				}
			}
			
			public class EquipAttribute : ItemStat
			{
				public EquipAttribute (string ikey, int value) : base (ikey, value)
				{
				}
			}
			
			public class CollectStat : ItemStat
			{
				public int every;
				public int window;
				public int collect_at;
				
				public CollectStat (string ikey, int value) : base (ikey, value)
				{
				}
			}
			
			public class UnknownStat : ItemStat {
				public UnknownStat (string ikey, int value) : base (ikey, value)
				{
				}
			}
		}
		
		
		public class ShopEntry
		{
			public string ikey;
			public string label;
			public string description;
			
			public IList<Cost> costs = new List<Cost>();
			public IList<Modifier> modifiers = new List<Modifier>();
			public IList<Requirement> requirements = new List<Requirement>();
			
			public IList<string> tags = new List<string>();
			
			public static ShopEntry CreateFromXml( IXMLNode n, IXCRMParser xcrm_parser )
			{
				ShopEntry retval = new ShopEntry();
				
				Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out retval.ikey);
				kv.TryGetValue("label", out retval.label);
				kv.TryGetValue("description", out retval.description);
				
				
				IXMLNode costs_node = n.GetNode("costs>0");
				if (costs_node != null)
				{
					retval.costs = xcrm_parser.ParseCostList(costs_node);
				}
				// else branches not necessary as costs, modifiers and requirements are INITIALISED by definition
				
				IXMLNode modifiers_node = n.GetNode("modifiers>0");
				if (modifiers_node != null)
				{
					retval.modifiers = xcrm_parser.ParseModifierList(modifiers_node);
				}
				
				IXMLNode requirements_node = n.GetNode("requirements>0");
				if (requirements_node != null)
				{
					retval.requirements = xcrm_parser.ParseRequirementList(requirements_node);
				}
				
				return retval;
			}
		};
		
		public class ModifierResult
		{
			public int add_xp = 0;
			public IDictionary<string, int> stat_change = new Dictionary<string, int>();
			public IDictionary<string, int> removed_items = new Dictionary<string, int>();
			public IDictionary<string, IList<string>> add_item = new Dictionary<string, IList<string>>();
			public IList<string> tags = new List<string>();
			
			public static ModifierResult CreateFromXml (IXMLNode n)
			{
				ModifierResult retval = new ModifierResult();
				List<IXMLNode> cost_or_modifier_nodes = n.GetNodeList("costs>0>cost");
				cost_or_modifier_nodes.AddRange(n.GetNodeList("modifiers>0>modifier"));
				foreach(IXMLNode cost_or_modifier_node in cost_or_modifier_nodes)
				{
					string ikey = cost_or_modifier_node.GetAttribute("ikey");
					int value = 0;
					switch (cost_or_modifier_node.GetAttribute("type"))
					{
					case "stat_change":
						System.Int32.TryParse(cost_or_modifier_node.GetAttribute("value"), out value);
						if (retval.stat_change.ContainsKey(ikey))
						{
							retval.stat_change[ikey] += value;
						}
						else
						{
							retval.stat_change.Add(ikey, value);
						}
						break;
					case "removed_items":
						System.Int32.TryParse(cost_or_modifier_node.GetAttribute("count"), out value);
						if (retval.removed_items.ContainsKey(ikey))
						{
							retval.removed_items[ikey] += value;
						}
						else
						{
							retval.removed_items.Add(ikey, value);
						}
						break;
					case "add_xp":
						System.Int32.TryParse(cost_or_modifier_node.GetAttribute("value"), out value);
						retval.add_xp += value;
						break;
					case "add_item":
						if (retval.add_item.ContainsKey(ikey))
						{
							retval.add_item[ikey].Add(cost_or_modifier_node.GetAttribute("item_id"));
						}
						else
						{
							IList<string> list = new List<string>();
							list.Add(cost_or_modifier_node.GetAttribute("item_id"));
							retval.add_item.Add(ikey, list);
						}
						break;
					default:
						System.Console.WriteLine ("TYPE [" + cost_or_modifier_node.GetAttribute("type") + "]");
						break;
					}
				}
				IList<IXMLNode> tag_nodes = n.GetNodeList("tags>0>tag");
				foreach(IXMLNode tag_node in tag_nodes)
				{
					retval.tags.Add(tag_node.GetAttribute("value"));
				}
				return retval;
			}
		};


		public class Friend
		{
			public string player_id;
			public string name;
			public int level;
			public static Friend CreateFromXml( IXMLNode n )
			{
				Friend f = new Friend();
				f.player_id = n.GetFirstChild("player_id").Text;
				f.name = n.GetFirstChild("name").Text;
				f.level = System.Convert.ToInt32( n.GetFirstChild("level").Text );
				return f;
			}
		};

		public class FriendInviteInfo
		{
			public string message;
			public string player_id;
			public string name;
			public int level;
		}
		
		public class FriendInvite
		{
			public string invite_id;
			public string player_id;
			public string player_name;
			public string message;
		}

		public class XPAttributes
		{
			public int value;
			public int next_level;
			public int level_start;
		};
		
		public class Player
		{
			public string id;
			public string name;
			public int level;
			public XPAttributes xp = new XPAttributes();
			public Dictionary<string, PlayerAttribute> attributes = new Dictionary<string, PlayerAttribute>();
		};

		public class BulkPlayerInfo
		{
			public Dictionary<string, string> stats = new Dictionary<string, string>();
			public Dictionary<string, string> properties = new Dictionary<string, string>();
		};

		public class LeaderboardInfo
		{
			public string board_id;
			public string ikey;
			public string resource_id;
			public string label;
			
			public static LeaderboardInfo CreateFromXml( IXMLNode n )
			{
				LeaderboardInfo retval = new LeaderboardInfo();
				Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out retval.ikey);
				kv.TryGetValue("board_id",out retval.board_id);
				kv.TryGetValue("label",out retval.label);
				kv.TryGetValue("resource_id",out retval.resource_id);
				return retval;
			}
		}

		public class LeaderboardExtraProperties
		{
			public string ikey;
			public string value;
		}

		public class LeaderboardEntry
		{
			public int rank;
			public string player_id;
			public double value;
			public IList<LeaderboardExtraProperties> properties;
		}

		//TODO: Problem with this is that we only keep one page for each leadeerboard request... we really should merge them somehow.
		/**
		 * Expected XML is like this:
		 *
		 * <leaderboards>
		 *   <view status="ok">
		 *      <ranking ikey="mojo" offset="0" num_results="100" page="1" low_is_high="false">
		 *         <entry rank="1" player_id="612421456098" value="560">
		 *           <custom>
		 *             <property ikey="player_name" value="Monkey"/>
		 *           </custom>
		 *         </entry>
		 *         <entry rank="2" player_id="195104156933" value="514">
		 *           <custom>
		 *             <property ikey="player_name" value="Dragon"/>
		 *           </custom>
		 *        </entry>
		 *        <entry rank="3" player_id="440312985759" value="490"/>
		 *          <custom>
		 *            <property ikey="player_name" value="Fun and Awesome DUUUUUDE"/>
		 *           </custom>
		 *        </entry>
		 *     </ranking>
		 *   </view>
		 * </leaderboards>
		 */

		public class LeaderboardData
		{
			public string board_id;
			public string id;
			public string ikey;
			public string resource_id;
			public string label;
			
			public int offset;
			public int num_results;
			public int page;
			public bool low_is_high;

			public IList<LeaderboardEntry> entries;

		}
		
		public class InventoryItem
		{
			public string id;
			public string ikey;
			public string label;
			public string description;
			public bool sellable;
			public bool consumable;
			public string type;
			public bool equipped;
			public int count;
			
			public IList<ItemStat> stats = new List<ItemStat>();
			public IList<DomainObjects.Modifier> price = new List<DomainObjects.Modifier>();
			public IList<string> tags = new List<string>();
			public IList<ItemArchetypeProperty> properties = new List<ItemArchetypeProperty>();
			
			public static InventoryItem CreateFromXml (IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser)
			{
				DomainObjects.InventoryItem retval = new DomainObjects.InventoryItem();
				Dictionary<string, string> kv = n.Attributes.ToDictionary(v => v.Key, v => v.Value);
				kv.TryGetValue("id", out retval.id);
				kv.TryGetValue("ikey", out retval.ikey);
				kv.TryGetValue("type", out retval.type);
				kv.TryGetValue("label", out retval.label);
				kv.TryGetValue("description", out retval.description);
				if (kv.ContainsKey("count") && ! System.Int32.TryParse(kv["count"], out retval.count))
				{
					throw new InvalidXMLElementException("Unable to parse count to integer");
				}
				if (kv.ContainsKey("consumable"))
				{
					retval.consumable = kv["consumable"].ToLower() == "true";
				}
				if (kv.ContainsKey("sellable"))
				{
					retval.sellable = kv["sellable"].ToLower() == "true";
				}
				if (kv.ContainsKey("equipped"))
				{
					retval.equipped = kv["equipped"].ToLower() == "true";
				}
				retval.stats = ixcrm_parser.ParseItemStatList(n.GetNode("stats>0"));
				retval.price = ixcrm_parser.ParseModifierList(n.GetNode("price>0"));
				retval.tags = ixcrm_parser.ParseTagList(n.GetNode("tags>0"));
				IList<IXMLNode> property_nodes = n.GetNodeList("properties>0>property");
				foreach(IXMLNode property_node in property_nodes)
				{
					Roar.DomainObjects.ItemArchetypeProperty property = new Roar.DomainObjects.ItemArchetypeProperty();
					property.ikey = property_node.GetAttribute("ikey");
					property.value = property_node.GetAttribute("value");
					retval.properties.Add(property);
				}
				return retval;
			}
		}
		
		public class ItemPrototype
		{
			public string ikey;
		}
		
		public class ItemArchetypeProperty
		{
			public string ikey;
			public string value;
		}
		
		public class ItemArchetype
		{
			public string id;
			public string ikey;
			public string type;
			public string label;
			public string description;
			public bool sellable;
			public bool consumable;
			public IList<ItemStat> stats = new List<ItemStat>();
			public IList<DomainObjects.Modifier> price = new List<DomainObjects.Modifier>();
			public IList<string> tags = new List<string>();
			public IList<ItemArchetypeProperty> properties = new List<ItemArchetypeProperty>();
			
			public static ItemArchetype CreateFromXml (IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser)
			{
				DomainObjects.ItemArchetype retval = new DomainObjects.ItemArchetype();
				Dictionary<string, string> kv = n.Attributes.ToDictionary(v => v.Key, v => v.Value);
				kv.TryGetValue("id", out retval.id);
				kv.TryGetValue("ikey", out retval.ikey);
				kv.TryGetValue("type", out retval.type);
				kv.TryGetValue("label", out retval.label);
				kv.TryGetValue("description", out retval.description);
				if (kv.ContainsKey("consumable"))
				{
					retval.consumable = kv["consumable"].ToLower() == "true";
				}
				if (kv.ContainsKey("sellable"))
				{
					retval.sellable = kv["sellable"].ToLower() == "true";
				}
				retval.stats = ixcrm_parser.ParseItemStatList(n.GetNode("stats>0"));
				retval.price = ixcrm_parser.ParseModifierList(n.GetNode("price>0"));
				retval.tags = ixcrm_parser.ParseTagList(n.GetNode("tags>0"));
				IList<IXMLNode> property_nodes = n.GetNodeList("properties>0>property");
				foreach(IXMLNode property_node in property_nodes)
				{
					Roar.DomainObjects.ItemArchetypeProperty property = new Roar.DomainObjects.ItemArchetypeProperty();
					property.ikey = property_node.GetAttribute("ikey");
					property.value = property_node.GetAttribute("value");
					retval.properties.Add(property);
				}
				return retval;
			}
		}
		
		public class MailPackage
		{
			public string id;
			public string type;
			public string sender_id;
			public string sender_name;
			public string message;
			public IList<InventoryItem> items = new List<InventoryItem>();
			public IList<string> tags = new List<string>();
			public IList<DomainObjects.Modifier> modifiers = new List<DomainObjects.Modifier>();
			
			public static MailPackage CreateFromXml (IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser)
			{
				MailPackage retval = new MailPackage();
				Dictionary<string, string> kv = n.Attributes.ToDictionary(v => v.Key, v => v.Value);
				kv.TryGetValue("id", out retval.id);
				kv.TryGetValue("type", out retval.type);
				kv.TryGetValue("sender_id", out retval.sender_id);
				kv.TryGetValue("sender_name", out retval.sender_name);
				kv.TryGetValue("message", out retval.message);
				IList<IXMLNode> item_nodes = n.GetNodeList("item");
				foreach (IXMLNode item_node in item_nodes)
				{
					retval.items.Add(InventoryItem.CreateFromXml(item_node, ixcrm_parser));
				}
				retval.tags = ixcrm_parser.ParseTagList(n);
				retval.modifiers = ixcrm_parser.ParseModifierList(n.GetNode("modifiers>0"));
				return retval;
			}
		}
		
		
		public class PlayerAttribute
		{
			public string ikey = null;
			public string name = null;
			public string label = null;
			public string value = null;
			public string type = null;
			public string min = null;
			public string max = null;
			public string regen_amount = null;
			public string regen_every = null;
			public string level_start = null;
			public string next_level = null;
			
			public void ParseXml( IXMLNode nn )
			{
				Dictionary<string,string> kv = nn.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out ikey);
				kv.TryGetValue("name",out name);
				if( ikey == null ) { ikey = name;} 

				kv.TryGetValue("label",out label);
				kv.TryGetValue("value",out value);
				kv.TryGetValue("type",out type);
				kv.TryGetValue("min",out min);
				kv.TryGetValue("max",out max);
				kv.TryGetValue("regen_amount",out regen_amount);
				kv.TryGetValue("regen_every",out regen_every);
				kv.TryGetValue("level_start",out level_start);
				kv.TryGetValue("next_level",out next_level);
			}
		}
		
		public class FacebookFriendInfo
		{
			public string fb_name;
			public string fb_id;
			public string name; //Only non-null if the player is a user in the current game
			public string id; //Only non-null if the player is a user in the current game
		}
		
		public class FacebookShopEntry
		{
			public string ikey;
			public string description;
			public string label;
			public string price;
			public string product_url;
			public string image_url;
			public IList<Modifier> modifiers;
			
			public static FacebookShopEntry CreateFromXml( IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser )
			{
				FacebookShopEntry retval = new FacebookShopEntry();
				Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out retval.ikey);
				kv.TryGetValue("description",out retval.description);
				kv.TryGetValue("label",out retval.label);
				kv.TryGetValue("price",out retval.price);
				kv.TryGetValue("product_url",out retval.product_url);
				kv.TryGetValue("image_url", out retval.image_url);
				List<IXMLNode> modifier_nodes = n.GetNodeList("modifiers");
				if( modifier_nodes.Count > 0 )
				{
					retval.modifiers = ixcrm_parser.ParseModifierList( modifier_nodes[0] );
				}
				else
				{
					retval.modifiers = new List<Modifier>();
				}
				
				return retval;
			}
		}
		
		public class AppstoreShopEntry
		{
			public string product_identifier;
			public string label;
			public IList<Modifier> modifiers = new List<Modifier>();
			
			public static AppstoreShopEntry CreateFromXml (IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser)
			{
				AppstoreShopEntry retval = new AppstoreShopEntry();
				Dictionary<string, string> kv = n.Attributes.ToDictionary(v => v.Key, v => v.Value);
				kv.TryGetValue("product_identifier", out retval.product_identifier);
				kv.TryGetValue("label", out retval.label);
				retval.modifiers = ixcrm_parser.ParseModifierList(n.GetNode("modifiers>0"));
				return retval;
			}
		}
		
		public class ScriptRunResult
		{
			public IXMLNode resultNode;	
			
		}
		
		public class Task
		{
			public string ikey;
			public string label;
			public string description;
			public string location;
			public int mastery_level;
			public int mastery_progress;
			public IList<DomainObjects.Cost> costs = new List<DomainObjects.Cost>();
			public IList<DomainObjects.Modifier> rewards = new List<DomainObjects.Modifier>();
			public IList<DomainObjects.Requirement> requirements = new List<DomainObjects.Requirement>();
			public IList<string> tags = new List<string>();
			
			public static Task CreateFromXml (IXMLNode n, Roar.DataConversion.IXCRMParser ixcrm_parser)
			{
				DomainObjects.Task retval = new DomainObjects.Task();
				retval.ikey = n.GetAttribute("ikey");
				IXMLNode node = n.GetFirstChild("label");
				if (node != null)
				{
					retval.label = node.Text;
				}
				node = n.GetFirstChild("description");
				if (node != null)
				{
					retval.description = node.Text;
				}
				node = n.GetFirstChild("location");
				if (node != null)
				{
					retval.location = node.Text;
				}
				node = n.GetFirstChild("mastery");
				if (node != null)
				{
					if (! System.Int32.TryParse(node.GetAttribute("level"), out retval.mastery_level))
					{
						throw new InvalidXMLElementException("Unable to parse mastery level to integer");
					}
					if (! System.Int32.TryParse(node.GetAttribute("progress"), out retval.mastery_progress))
					{
						throw new InvalidXMLElementException("Untable to parse mastery progress to integer");
					}
				}
				retval.costs = ixcrm_parser.ParseCostList(n.GetNode("costs>0"));
				retval.rewards = ixcrm_parser.ParseModifierList(n.GetNode("rewards>0"));
				retval.requirements = ixcrm_parser.ParseRequirementList(n.GetNode("requires>0"));
				retval.tags = ixcrm_parser.ParseTagList(n.GetNode("tags>0"));
				return retval;
			}
		}
		
		public class Achievement
		{
			public string ikey;
			public string status;
			public string label;
			public string progress;
			public string description;
			
			public static Achievement CreateFromXml (IXMLNode n)
			{
				Achievement retval = new Achievement();
				IXMLNode node = n.GetNode("ikey>0");
				if (node != null)
				{
					retval.ikey = node.Text;
				}
				node = n.GetNode("status>0");
				if (node != null)
				{
					retval.status = node.Text;
				}
				node = n.GetNode("label>0");
				if (node != null)
				{
					retval.label = node.Text;
				}
				node = n.GetNode("progress>0");
				if (node != null)
				{
					retval.progress = node.Text;
				}
				node = n.GetNode("description>0");
				if (node != null)
				{
					retval.description = node.Text;
				}
				return retval;
			}
		}
		


	}
}
