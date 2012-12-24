using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.WebObjects.Appstore;

namespace Testing
{
	[TestFixture]
	public class XmlToAppstoreText
	{
		[Test()]
		public void TestAppstoreShopListXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<appstore>
					<shop_list>
						<shopitem product_identifier=""someidentifier"" label=""A label"">
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
								<grant_stat ikey=""item_stat"" type=""some type"" value=""7""/>
							</modifiers>
						</shopitem>
						<shopitem product_identifier=""someotheridentifier"" label=""Another label"">
						</shopitem>
					</shop_list>
				</appstore>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Appstore.ShopList shop_list_parser = new Roar.DataConversion.Responses.Appstore.ShopList();
			ShopListResponse response = shop_list_parser.Build(nn);
			
			Assert.IsNotNull(response);
			Assert.AreEqual(response.shop_list.Count, 2);
			Assert.AreEqual(response.shop_list[0].product_identifier, "someidentifier");
			Assert.AreEqual(response.shop_list[0].label, "A label");
			Assert.AreEqual(response.shop_list[0].modifiers.Count, 2);
			Assert.AreEqual((response.shop_list[0].modifiers[0] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "item_ikey_1");
			Assert.AreEqual((response.shop_list[0].modifiers[1] as Roar.DomainObjects.Modifiers.GrantStat).ikey, "item_stat");
			Assert.AreEqual((response.shop_list[0].modifiers[1] as Roar.DomainObjects.Modifiers.GrantStat).type, "some type");
			Assert.AreEqual((response.shop_list[0].modifiers[1] as Roar.DomainObjects.Modifiers.GrantStat).value, 7);
			Assert.AreEqual(response.shop_list[1].product_identifier, "someotheridentifier");
			Assert.AreEqual(response.shop_list[1].label, "Another label");
			Assert.AreEqual(response.shop_list[1].modifiers.Count, 0);
			string representation = "[" + string.Join(",", (string[])response.shop_list.ConvertAll<string>(e => e.product_identifier).ToArray()) + "]";
			Assert.AreEqual(representation, "[someidentifier,someotheridentifier]");
		}
		
		[Test()]
		public void TestAppstoreShopListParseMechanics ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<appstore>
					<shop_list>
						<shopitem product_identifier=""someidentifier"" label=""A label"">
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
								<grant_stat ikey=""item_stat"" type=""some type"" value=""7""/>
							</modifiers>
						</shopitem>
					</shop_list>
				</appstore>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Appstore.ShopList shop_list_parser = new Roar.DataConversion.Responses.Appstore.ShopList();
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			shop_list_parser.ixcrm_parser = ixcrm_parser;
			IList<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.GetNode("roar>0>appstore>0>shop_list>0>shopitem>0>modifiers>0")).Will(Return.Value(modifier_list));
			
			ShopListResponse response = shop_list_parser.Build(nn);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response);
			Assert.AreEqual(response.shop_list.Count, 1);
			Assert.AreEqual(response.shop_list[0].product_identifier, "someidentifier");
			Assert.AreEqual(response.shop_list[0].label, "A label");
			Assert.AreEqual(response.shop_list[0].modifiers, modifier_list);
		}
		
		[Test()]
		public void TestAppstoreBuyXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<appstore>
					<buy status=""ok""\>
				</appstore>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Appstore.Buy buy_parser = new Roar.DataConversion.Responses.Appstore.Buy();
			BuyResponse response = buy_parser.Build(nn);
			
			Assert.IsNotNull(response);
		}
	}
}

