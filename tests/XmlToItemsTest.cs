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
			Assert.AreEqual(response.items[0].count, 1);
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
			Assert.AreEqual(response.items[1].count, 1);
			Assert.AreEqual(response.items[1].label, "Talisman");
			Assert.AreEqual(response.items[1].type, "item");
			Assert.AreEqual(response.items[1].description, "protects from evil");
			Assert.IsFalse(response.items[1].consumable);
			Assert.IsFalse(response.items[1].sellable);
			
			Assert.IsNull(response.items[2].id);
			Assert.IsNull(response.items[2].ikey);
			Assert.AreEqual(response.items[2].count, 0);
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
	}
}

