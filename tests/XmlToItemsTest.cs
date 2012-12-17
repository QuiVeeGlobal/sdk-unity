using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.WebObjects.Items;

namespace Testing
{
	[TestFixture]
	public class XmlToItemsTest
	{
	
		[SetUp()]
		public void TestInitialise()
		{
		}
		
		[Test()]
		public void TestItemsViewXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""135536803344"">
				<items>
					<view status=""ok"">
						<item id=""1234"" ikey=""ring"" count=""1"" label=""Ring"" type=""item"" description=""Magical ring, which gives you strength."" consumable=""true"" sellable=""true"">
							<stats>
								<regen_stat_limited ikey=""premium_currency"" value=""345"" repeat=""12"" times_used=""4""/>
								<regen_stat ikey=""premium_currency"" value=""44"" every=""600000""/>
								<grant_stat ikey=""premium_currency"" value=""89""/>
								<equip_attribute ikey=""premium_currency"" value=""67""/>
								<collect_stat ikey=""premium_currency"" value=""45"" every=""600000"" window=""234"" collect_at=""5""/>
							</stats>
							<properties>
								<property ikey=""talisman"" value=""sonda""/>
								<property ikey=""mariner"" value=""543""/>
							</properties>
							<tags>
								<tag value=""magic""/>
								<tag value=""strength""/>
								<tag value=""magicitem""/>
							</tags>
							<price>
								<grant_xp value=""56""/>
								<grant_item ikey=""talisman""/>
							</price>
						</item>
					</view_all>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.DataConversion.Responses.Items.View converter = new Roar.DataConversion.Responses.Items.View();
			ViewResponse response = converter.Build(nn);
			
			Assert.IsNotNull(response.items);
			Assert.AreEqual(response.items.Count, 1);
			
			Assert.AreEqual(response.items[0].id, "1234");
			Assert.AreEqual(response.items[0].ikey, "ring");
			Assert.AreEqual(response.items[0].label, "Ring");
			Assert.AreEqual(response.items[0].type, "item");
			Assert.AreEqual(response.items[0].description, "Magical ring, which gives you strength.");
			Assert.IsTrue(response.items[0].consumable);
			Assert.IsTrue(response.items[0].sellable);
			Assert.AreEqual(response.items[0].stats.Count, 5);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).value, 345);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).repeat, 12);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).times_used, 4);
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).value, 44);
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).every, 600000);
			Assert.AreEqual((response.items[0].stats[2] as Roar.DomainObjects.ItemStats.GrantStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[2] as Roar.DomainObjects.ItemStats.GrantStat).value, 89);
			Assert.AreEqual((response.items[0].stats[3] as Roar.DomainObjects.ItemStats.EquipAttribute).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[3] as Roar.DomainObjects.ItemStats.EquipAttribute).value, 67);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).value, 45);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).every, 600000);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).window, 234);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).collect_at, 5);
			Assert.AreEqual(response.items[0].tags.Count, 3);
			Assert.AreEqual(response.items[0].tags[0], "magic");
			Assert.AreEqual(response.items[0].tags[1], "strength");
			Assert.AreEqual(response.items[0].tags[2], "magicitem");
			Assert.AreEqual(response.items[0].price.Count, 2);
			Assert.AreEqual((response.items[0].price[0] as Roar.DomainObjects.Modifiers.GrantXp).value, 56);
			Assert.AreEqual((response.items[0].price[1] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "talisman");
			Assert.AreEqual(response.items[0].properties.Count, 2);
			Assert.AreEqual(response.items[0].properties[0].ikey, "talisman");
			Assert.AreEqual(response.items[0].properties[0].value, "sonda");
			Assert.AreEqual(response.items[0].properties[1].ikey, "mariner");
			Assert.AreEqual(response.items[0].properties[1].value, "543");
		}
		
		[Test()]
		public void TestItemsViewXmlParseMechanics()
		{
			string xml =
			@"<roar tick=""135536803344"">
				<items>
					<view status=""ok"">
						<item>
							<stats>
								<regen_stat_limited ikey=""premium_currency"" value=""345"" repeat=""12"" times_used=""4""/>
							</stats>
							<properties>
								<property ikey=""mariner"" value=""543""/>
							</properties>
							<tags>
								<tag value=""magicitem""/>
							</tags>
							<price>
								<grant_item ikey=""talisman""/>
							</price>
						</item>
					</view_all>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			List<Roar.DomainObjects.ItemStat> item_stat_list = new List<Roar.DomainObjects.ItemStat>();
			List<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			List<string> tag_list = new List<string>();
			
			Roar.DataConversion.Responses.Items.View items_view_response_parser = new Roar.DataConversion.Responses.Items.View();
			items_view_response_parser.ixcrm_parser = ixcrm_parser;
			
			IXMLNode stat_node = nn.GetNode("roar>0>items>0>view>0>item>0>stats>0");
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(stat_node).Will(Return.Value(item_stat_list));
			IXMLNode modifiers_node = nn.GetNode("roar>0>items>0>view>0>item>0>price>0");
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(modifiers_node).Will(Return.Value(modifier_list));
			IXMLNode tag_node = nn.GetNode("roar>0>items>0>view>0>item>0>tags>0");
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(tag_node).Will(Return.Value(tag_list));
			
			ViewResponse response = items_view_response_parser.Build(nn);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.items);
			Assert.AreEqual(response.items.Count, 1);
			Assert.AreEqual(response.items[0].stats, item_stat_list);
			Assert.AreEqual(response.items[0].price, modifier_list);
			Assert.AreEqual(response.items[0].tags, tag_list);
		}
		
		[Test()]
		public void TestItemsViewAllXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""135536803344"">
				<items>
					<view_all status=""ok"">
						<item id=""1234"" ikey=""ring"" count=""1"" label=""Ring"" type=""item"" description=""Magical ring, which gives you strength."" consumable=""true"" sellable=""true"">
							<stats>
								<regen_stat_limited ikey=""premium_currency"" value=""345"" repeat=""12"" times_used=""4""/>
								<regen_stat ikey=""premium_currency"" value=""44"" every=""600000""/>
								<grant_stat ikey=""premium_currency"" value=""89""/>
								<equip_attribute ikey=""premium_currency"" value=""67""/>
								<collect_stat ikey=""premium_currency"" value=""45"" every=""600000"" window=""234"" collect_at=""5""/>
							</stats>
							<properties>
								<property ikey=""talisman"" value=""sonda""/>
								<property ikey=""mariner"" value=""543""/>
							</properties>
							<tags>
								<tag value=""magic""/>
								<tag value=""strength""/>
								<tag value=""magicitem""/>
							</tags>
							<price>
								<grant_xp value=""56""/>
								<grant_item ikey=""talisman""/>
							</price>
						</item>
						<item id=""2345"" ikey=""talisman"" count=""1"" label=""Talisman"" type=""item"" description=""protects from evil"" consumable=""false"" sellable=""false"">
							<tags>
								<tag value=""magicitem""/>
								<tag value=""protection""/>
							</tags>
						</item>
						<item/>
					</view_all>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.DataConversion.Responses.Items.ViewAll converter = new Roar.DataConversion.Responses.Items.ViewAll();
			ViewAllResponse response = converter.Build(nn);
			
			Assert.IsNotNull(response.items);
			Assert.AreEqual(response.items.Count, 3);
			
			Assert.AreEqual(response.items[0].id, "1234");
			Assert.AreEqual(response.items[0].ikey, "ring");
			Assert.AreEqual(response.items[0].label, "Ring");
			Assert.AreEqual(response.items[0].type, "item");
			Assert.AreEqual(response.items[0].description, "Magical ring, which gives you strength.");
			Assert.IsTrue(response.items[0].consumable);
			Assert.IsTrue(response.items[0].sellable);
			Assert.AreEqual(response.items[0].stats.Count, 5);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).value, 345);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).repeat, 12);
			Assert.AreEqual((response.items[0].stats[0] as Roar.DomainObjects.ItemStats.RegenStatLimited).times_used, 4);
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).value, 44);
			Assert.AreEqual((response.items[0].stats[1] as Roar.DomainObjects.ItemStats.RegenStat).every, 600000);
			Assert.AreEqual((response.items[0].stats[2] as Roar.DomainObjects.ItemStats.GrantStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[2] as Roar.DomainObjects.ItemStats.GrantStat).value, 89);
			Assert.AreEqual((response.items[0].stats[3] as Roar.DomainObjects.ItemStats.EquipAttribute).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[3] as Roar.DomainObjects.ItemStats.EquipAttribute).value, 67);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).ikey, "premium_currency");
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).value, 45);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).every, 600000);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).window, 234);
			Assert.AreEqual((response.items[0].stats[4] as Roar.DomainObjects.ItemStats.CollectStat).collect_at, 5);
			Assert.AreEqual(response.items[0].tags.Count, 3);
			Assert.AreEqual(response.items[0].tags[0], "magic");
			Assert.AreEqual(response.items[0].tags[1], "strength");
			Assert.AreEqual(response.items[0].tags[2], "magicitem");
			Assert.AreEqual(response.items[0].price.Count, 2);
			Assert.AreEqual((response.items[0].price[0] as Roar.DomainObjects.Modifiers.GrantXp).value, 56);
			Assert.AreEqual((response.items[0].price[1] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "talisman");
			Assert.AreEqual(response.items[0].properties.Count, 2);
			Assert.AreEqual(response.items[0].properties[0].ikey, "talisman");
			Assert.AreEqual(response.items[0].properties[0].value, "sonda");
			Assert.AreEqual(response.items[0].properties[1].ikey, "mariner");
			Assert.AreEqual(response.items[0].properties[1].value, "543");
			
			Assert.AreEqual(response.items[1].id, "2345");
			Assert.AreEqual(response.items[1].ikey, "talisman");
			Assert.AreEqual(response.items[1].label, "Talisman");
			Assert.AreEqual(response.items[1].type, "item");
			Assert.AreEqual(response.items[1].description, "protects from evil");
			Assert.IsFalse(response.items[1].consumable);
			Assert.IsFalse(response.items[1].sellable);
			
			Assert.IsNull(response.items[2].id);
			Assert.IsNull(response.items[2].ikey);
			Assert.IsNull(response.items[2].label);
			Assert.IsNull(response.items[2].type);
			Assert.IsNull(response.items[2].description);
			Assert.IsFalse(response.items[2].sellable);
			Assert.IsFalse(response.items[2].consumable);
			Assert.AreEqual(response.items[2].stats.Count, 0);
			Assert.AreEqual(response.items[2].tags.Count, 0);
			Assert.AreEqual(response.items[2].price.Count, 0);
			Assert.AreEqual(response.items[2].properties.Count, 0);
		}
		
		[Test()]
		public void TestItemsViewAllParserMechanics ()
		{
			
			string xml =
			@"<roar tick=""135536803344"">
				<items>
					<view_all status=""ok"">
						<item>
							<stats>
								<regen_stat_limited ikey=""premium_currency"" value=""345"" repeat=""12"" times_used=""4""/>
							</stats>
							<properties>
								<property ikey=""talisman"" value=""sonda""/>
							</properties>
							<tags>
								<tag value=""magicitem""/>
							</tags>
							<price>
								<grant_item ikey=""talisman""/>
							</price>
						</item>
					</view_all>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			List<Roar.DomainObjects.ItemStat> item_stat_list = new List<Roar.DomainObjects.ItemStat>();
			List<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			List<string> tag_list = new List<string>();
			
			Roar.DataConversion.Responses.Items.ViewAll items_view_all_response_parser = new Roar.DataConversion.Responses.Items.ViewAll();
			items_view_all_response_parser.ixcrm_parser = ixcrm_parser;
			
			IXMLNode stat_node = nn.GetNode("roar>0>items>0>view_all>0>item>0>stats>0");
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(stat_node).Will(Return.Value(item_stat_list));
			IXMLNode modifiers_node = nn.GetNode("roar>0>items>0>view_all>0>item>0>price>0");
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(modifiers_node).Will(Return.Value(modifier_list));
			IXMLNode tag_node = nn.GetNode("roar>0>items>0>view_all>0>item>0>tags>0");
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(tag_node).Will(Return.Value(tag_list));
			
			ViewAllResponse response = items_view_all_response_parser.Build(nn);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.items);
			Assert.AreEqual(response.items.Count, 1);
			Assert.AreEqual(response.items[0].stats, item_stat_list);
			Assert.AreEqual(response.items[0].price, modifier_list);
			Assert.AreEqual(response.items[0].tags, tag_list);
		}
		
		[Test()]
		public void TestItemsListXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""135546049121"">
				<items>
					<list status=""ok"">
						<item id=""1039149107"" ikey=""talisman"" count=""1"" label=""Talisman"" type=""item"" description=""protects from evil"" consumable=""false"" sellable=""false"" equipped=""true"">
							<tags>
								<tag value=""magicitem""/>
							</tags>
						</item>
					</list>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			List<Roar.DomainObjects.ItemStat> item_stat_list = new List<Roar.DomainObjects.ItemStat>();
			List<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			List<string> tag_list = new List<string>();
			
			Roar.DataConversion.Responses.Items.List list_parser = new Roar.DataConversion.Responses.Items.List();
			list_parser.ixcrm_parser = ixcrm_parser;
			
			IXMLNode stat_node = nn.GetNode("roar>0>items>0>list>0>item>0>stats>0");
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(stat_node).Will(Return.Value(item_stat_list));
			IXMLNode modifiers_node = nn.GetNode("roar>0>items>0>list>0>item>0>price>0");
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(modifiers_node).Will(Return.Value(modifier_list));
			IXMLNode tag_node = nn.GetNode("roar>0>items>0>list>0>item>0>tags>0");
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(tag_node).Will(Return.Value(tag_list));
			
			ListResponse response = list_parser.Build(nn);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.items);
			Assert.AreEqual(response.items.Count, 1);
			Assert.IsTrue(response.items[0].equipped);
			Assert.AreEqual(response.items[0].stats, item_stat_list);
			Assert.AreEqual(response.items[0].price, modifier_list);
			Assert.AreEqual(response.items[0].tags, tag_list);
		}
		
		[Test()]
		public void TestItemsSellXmlParseMechanics()
		{
			string xml =
			@"<roar tick=""135572003492"">
				<items>
					<sell status=""ok"">
						<effect>
							<costs/>
							<modifiers>
								<modifier type=""removed_items"" ikey=""talisman"" count=""34""/>
								<modifier type=""stat_change"" ikey=""premium_currency"" value=""67""/>
								<modifier type=""stat_change"" ikey=""premium_currency"" value=""9""/>
								<modifier type=""add_xp"" value=""876""/>
								<modifier type=""add_xp"" value=""6""/>
								<modifier type=""add_item"" ikey=""talisman"" item_id=""1458454945""/>
								<modifier type=""stat_change"" ikey=""premium_currency"" value=""88""/>
								<modifier type=""add_xp"" value=""66""/>
								<modifier type=""add_xp"" value=""77""/>
							</modifiers>
						</effect>
						<item id=""275012935"" ikey=""ring"" count=""1"" label=""Ring"" type=""item"" description=""Magic ring for protection and strength"" consumable=""true"" sellable=""true"">
							<stats>
								<equip_attribute ikey=""premium_currency"" value=""67""/>
								<collect_stat ikey=""premium_currency"" value=""45"" every=""600000"" window=""234"" collect_at=""0""/>
								<regen_stat_limited ikey=""premium_currency"" value=""345"" repeat=""12"" times_used=""0""/>
								<regen_stat ikey=""premium_currency"" value=""44"" every=""600000""/>
								<grant_stat ikey=""premium_currency"" value=""566""/>
							</stats>
							<properties>
								<property ikey=""sonda"" value=""extra""/>
								<property ikey=""mariner"" value=""505""/>
							</properties>
							<tags>
								<tag value=""magic""/>
								<tag value=""magicitem""/>
								<tag value=""protection""/>
							</tags>
							<price>
								<remove_items/>
								<grant_stat type=""currency"" ikey=""premium_currency"" value=""67""/>
								<grant_stat_range type=""currency"" ikey=""premium_currency"" min=""8"" max=""9""/>
								<grant_xp value=""876""/>
								<grant_xp_range min=""6"" max=""7""/>
								<grant_item ikey=""talisman""/>
								<random_choice>
									<choice weight=""67"">
										<modifier>
											<grant_stat type=""currency"" ikey=""premium_currency"" value=""88""/>
										</modifier>
										<requirement>
											<true_requirement ok=""true""/>
										</requirement>
									</choice>
								</random_choice>
								<grant_xp value=""66""/>
								<if_then_else>
									<if>
										<true_requirement ok=""true""/>
									</if>
									<then>
										<grant_xp value=""77""/>
									</then>
									<else>
										<grant_xp value=""88""/>
									</else>
								</if_then_else>
							</price>
						</item>
					</sell>
				</items>
				<server>
					<item_lose item_id=""180846839"" item_ikey=""talisman""/>
					<item_add item_id=""1458454945"" item_ikey=""talisman""/>
					<item_lose item_id=""275012935"" item_ikey=""ring""/>
					<update type=""currency"" ikey=""premium_currency"" value=""32987""/>
					<update type=""xp"" ikey=""xp"" value=""16807""/>
					<inventory_changed/>
				</server>
			</roar>";
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			Roar.DomainObjects.InventoryItem item_data = new Roar.DomainObjects.InventoryItem();
			IList<Roar.DomainObjects.Cost> cost_data = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Modifier> modifier_data = new List<Roar.DomainObjects.Modifier>();
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Items.Sell sell_parser = new Roar.DataConversion.Responses.Items.Sell();
			sell_parser.ixcrm_parser = ixcrm_parser;
			
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(nn.GetNode("roar>0>items>0>sell>0>item>0>stats>0")).Will(Return.Value(item_data.stats));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>items>0>sell>0>item>0>price>0")).Will(Return.Value(item_data.price));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.GetNode("roar>0>items>0>sell>0>item>0>tags>0")).Will(Return.Value(item_data.tags));
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With(nn.GetNode("roar>0>items>0>sell>0>costs>0")).Will(Return.Value(cost_data));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>items>0>sell>0>modifiers>0")).Will(Return.Value(modifier_data));
			
			SellResponse response = sell_parser.Build(nn);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.item);
			Assert.AreEqual(response.item.id, "275012935");
			Assert.AreEqual(response.item.ikey, "ring");
			Assert.AreEqual(response.item.count, 1);
			Assert.AreEqual(response.item.label, "Ring");
			Assert.AreEqual(response.item.type, "item");
			Assert.AreEqual(response.item.description, "Magic ring for protection and strength");
			Assert.IsTrue(response.item.consumable);
			Assert.IsTrue(response.item.sellable);
			Assert.AreEqual(response.item.stats, item_data.stats);
			Assert.AreEqual(response.item.price, item_data.price);
			Assert.AreEqual(response.item.tags, item_data.tags);
			
			Assert.IsNotNull(response.costs);
			Assert.AreEqual(response.costs, cost_data);
			
			Assert.IsNotNull(response.modifiers);
			Assert.AreEqual(response.modifiers, modifier_data);
		}
		
		[Test()]
		public void TestItemsEquipXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""128779477951"">
				<items>
					<equip status=""ok""/>
				</items>
				<!--The server flags that the user inventory status has changed-->
				<server>
					<inventory_changed/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Items.Equip equip_parser = new Roar.DataConversion.Responses.Items.Equip();
			EquipResponse response = equip_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestItemsUnequipXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""128779477951"">
				<items>
					<unequip status=""ok""/>
				</items>
				<!--The server flags that the user inventory status has changed-->
				<server>
					<inventory_changed/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Items.Unequip unequip_parser = new Roar.DataConversion.Responses.Items.Unequip();
			UnequipResponse response = unequip_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestItemsUseXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""135571857913"">
				<items>
					<use status=""ok""/>
				</items>
				<server>
					<item_use item_id=""232839631""/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Items.Use use_parser = new Roar.DataConversion.Responses.Items.Use();
			UseResponse response = use_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestItemsSetXmlGetAttributes()
		{
			string xml =
			@"<roar tick=""128555540202"">
				<items>
					<set status=""ok""/>
				</items>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Items.Set set_parser = new Roar.DataConversion.Responses.Items.Set();
			SetResponse response = set_parser.Build(nn);
			Assert.IsNotNull(response);
		}
	}
}

