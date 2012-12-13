using System;
using NUnit.Framework;
using Roar.WebObjects.Shop;

namespace Testing
{

	[TestFixture()]
	public class XMLToShopTest
	{
	
		[SetUp]
		public void TestInitialise ()
		{
		}
		
		[Test()]
		public void TestShopListXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<shop>
					<list status=""ok"">
						<shopitem ikey=""shop_item_ikey_1"" label=""Shop item 1"" description=""Lorem Ipsum"">
							<costs>
								<stat_cost type=""currency"" ikey=""cash"" value=""100"" ok=""false"" reason=""Insufficient Coins""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""0"" ok=""true""/>
							</costs>
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
							</modifiers>
							<tags/>
						</shopitem>
						<shopitem ikey=""shop_item_ikey_2""/>
						<shopitem ikey=""some ikey"" label=""Shop item 2""/>
						<shopitem ikey=""blah blah"" description=""Blah Blah"">
							<costs>
								<stat_cost type=""currency"" ikey=""cash"" value=""0""/>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""50""/>
							</costs>
							<modifiers>
								<grant_item ikey=""item_ikey_2""/>
							</modifiers>
							<tags>
								<tag value=""a_tag""/>
								<tag value=""another_tag""/>
							</tags>
						</shopitem>
					</list>
				</shop>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.DataConversion.Responses.Shop.List converter = new Roar.DataConversion.Responses.Shop.List();
			ListResponse response = converter.Build(nn);
			
			// Assume that costs, modifiers, requirements and tags conversions are tested someplace else
			// Hence we test only correct conversion from XML to ShopEntry on the higher level
			
			Assert.IsNotNull(response.shop_entries);
			Assert.AreEqual(response.shop_entries.Count, 4);
			Assert.AreEqual(response.shop_entries[0].ikey, "shop_item_ikey_1");
			Assert.AreEqual(response.shop_entries[0].label, "Shop item 1");
			Assert.AreEqual(response.shop_entries[0].description, "Lorem Ipsum");
			Assert.AreEqual(response.shop_entries[0].costs.Count, 2);
			Assert.AreEqual(response.shop_entries[0].modifiers.Count, 1);
			Assert.AreEqual(response.shop_entries[0].requirements.Count, 0);
			Assert.AreEqual(response.shop_entries[0].tags.Count, 0);
			
			Assert.AreEqual(response.shop_entries[1].costs.Count, 0);
			Assert.AreEqual(response.shop_entries[1].modifiers.Count, 0);
			Assert.AreEqual(response.shop_entries[1].requirements.Count, 0);
			Assert.AreEqual(response.shop_entries[1].tags.Count, 0);
			
			Assert.AreEqual(response.shop_entries[3].ikey, "blah blah");
			Assert.AreEqual(response.shop_entries[3].description, "Blah Blah");
		}
		
		[Test()]
		public void TestShopBuyXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""125555445657"">
				<shop>
					<buy status=""ok"">
						<costs>
							<cost type=""stat_change"" ikey=""coins"" value=""-5""/>
							<cost type=""stat_change"" ikey=""premium_currency"" value=""-67""/>
							<cost type=""stat_change"" ikey=""coins"" value=""-4""/>
							<cost type=""removed_items"" ikey=""talisman"" count=""4""/>
							<cost type=""removed_items"" ikey=""talisman"" count=""3""/>
							<cost type=""removed_items"" ikey=""ring_of_power"" count=""1""/>
						</costs>
						<modifiers>
							<modifier type=""add_xp"" value=""100""/>
							<modifier type=""add_item"" ikey=""item_key"" item_id=""16268470388190951200""/>
							<modifier type=""add_xp"" value=""45""/>
							<modifier type=""add_item"" ikey=""talisman"" item_id=""1117206301""/>
							<modifier type=""add_item"" ikey=""talisman"" item_id=""1039149107""/>
							<modifier type=""removed_items"" ikey=""talisman"" count=""2""/>
							<modifier type=""stat_change"" ikey=""premium_currency"" value=""16""/>
							<modifier type=""stat_change"" ikey=""premium_currency"" value=""7""/>
							<modifier type=""add_xp"" value=""9""/>
							<modifier type=""stat_change"" ikey=""premium_currency"" value=""177""/>
							<modifier type=""add_xp"" value=""1567""/>
							<modifier type=""add_xp"" value=""3456""/>
							<modifier type=""stat_change"" ikey=""premium_currency"" value=""9876""/>
						</modifiers>
						<tags>
							<tag value=""special""/>
							<tag value=""magic""/>
						</tags>
					</buy>
				</shop>
				<!--Server block shows server updates below-->
				<server>
					<update type=""currency"" ikey=""coins"" value=""203""/>
					<inventory_changed/>
				</server>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.DataConversion.Responses.Shop.Buy converter = new Roar.DataConversion.Responses.Shop.Buy();
			BuyResponse response = converter.Build(nn);
			
			Assert.IsNotNull(response.buy_response);
			
			Assert.AreEqual(response.buy_response.stat_change.Count, 2);
			Assert.AreEqual(response.buy_response.stat_change["coins"], -9);
			Assert.AreEqual(response.buy_response.stat_change["premium_currency"], 10009);
			
			Assert.AreEqual(response.buy_response.removed_items.Count, 2);
			Assert.AreEqual(response.buy_response.removed_items["talisman"], 9);
			Assert.AreEqual(response.buy_response.removed_items["ring_of_power"], 1);
			
			Assert.AreEqual(response.buy_response.add_xp, 5177);
			
			Assert.AreEqual(response.buy_response.add_item.Count, 2);
			Assert.AreEqual(response.buy_response.add_item["item_key"][0], "16268470388190951200");
			Assert.AreEqual(response.buy_response.add_item["talisman"][0], "1117206301");
			Assert.AreEqual(response.buy_response.add_item["talisman"][1], "1039149107");
			
			Assert.AreEqual(response.buy_response.tags.Count, 2);
			Assert.AreEqual(response.buy_response.tags[0], "special");
			Assert.AreEqual(response.buy_response.tags[1], "magic");
		}
		
	}
}

