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
			
			public IList<Cost> costs;
			public IList<Modifier> modifiers;
			public IList<Requirement> requirements;
			
			public IList<string> tags;
			
			public static ShopEntry CreateFromXml( IXMLNode n )
			{
				ShopEntry retval = new ShopEntry();
				
				Dictionary<string,string> kv = n.Attributes.ToDictionary( v => v.Key, v => v.Value );
				kv.TryGetValue("ikey",out retval.ikey);
				kv.TryGetValue("label", out retval.label);
				kv.TryGetValue("description", out retval.description);
				
				//TODO: Handle the costs, modifiers and requirements and tags!
				
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

		public class Leaderboard
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
		


	}
}
