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
		
	}
}

