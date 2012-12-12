using System.Collections.Generic;
using System.Linq;

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
		
		
		public class ShopEntry
		{
			public string ikey;
			public string label;
			public string description;
			
			public IList<Cost> costs = new List<Cost>();
			public IList<Modifier> modifiers = new List<Modifier>();
			public IList<Requirement> requirements = new List<Requirement>();
			
			public IList<string> tags = new List<string>();
			
			public static ShopEntry CreateFromXml( IXMLNode n )
			{
				ShopEntry retval = new ShopEntry();
				
				Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out retval.ikey);
				kv.TryGetValue("label", out retval.label);
				kv.TryGetValue("description", out retval.description);
				
				Roar.DataConversion.XCRMParser xcrm_parser = new Roar.DataConversion.XCRMParser();
				
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
		
		public class ShopBuyResponse
		{
			public int add_xp = 0;
			public IDictionary<string, int> stat_change = new Dictionary<string, int>();
			public IDictionary<string, int> removed_items = new Dictionary<string, int>();
			public IDictionary<string, IList<string>> add_item = new Dictionary<string, IList<string>>();
			public IList<string> tags = new List<string>();
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
			public bool sellable;
			public bool consumable;
			public bool equipped;
			public string label;
			public ItemPrototype item_prototype;
		}
		
		public class ItemPrototype
		{
			public string ikey;
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
		
		public class ScriptRunResult
		{
			public IXMLNode resultNode;	
			
		}
		


	}
}
