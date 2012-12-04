using System.Collections.Generic;

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
		
		
		public interface IDomainObject
		{
			bool MatchesKey( string key );
		}
		
		public class ShopEntry : IDomainObject
		{
			public string as_json() { return "{}"; }
			public bool MatchesKey( string key ) { return ikey==key; } //Should check id, ikey, shop_ikey etc.
			
			public string ikey;
			public string label;
			public string description;
			
			public IList<Cost> costs;
			public IList<Modifier> modifiers;
			public IList<Requirement> requirements;
			
			public IList<string> tags;
		};


		public class Friend : IDomainObject
		{
			public bool MatchesKey( string key )
			{
				return player_id==key;
			}

			public string player_id;
			public string name;
			public int level;
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

		public class Leaderboard : IDomainObject
		{
			public bool MatchesKey( string key )
			{
				return ikey==key;
			}

			public string ikey;
			public int offset;
			public int num_results;
			public int page;
			public bool low_is_high;

			public IList<LeaderboardEntry> entries;

		}
		
		public class Item : IDomainObject
		{
			public bool MatchesKey( string key )
			{
				return ikey==key;
			}

			public string id;
			public string ikey;
			public bool sellable;
			public bool consumable;
			public bool equipped;
			public string label;
			public Foo item_prototype;
		}


	}
}
