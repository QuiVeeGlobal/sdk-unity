using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.WebObjects.ChromeWebStore;

namespace Testing
{
	[TestFixture]
	public class XmlToChromeWebStoreTest
	{
		[Test()]
		public void TestChromeWebStoreListXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<chrome_web_store>
					<list>
						<shopitem blah=""???"" ikey=""token"" label=""Token"" description=""Chrome Token"" price=""23"" jwt=""????"">
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
							</modifiers>
							<tags>
								<tag value=""a_tag""/>
								<tag value=""another_tag""/>
							</tags>
						</shopitem>
					</list>
				</chrome_web_store>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.ChromeWebStore.List list_parser = new Roar.DataConversion.Responses.ChromeWebStore.List();
			ListResponse response = list_parser.Build(nn);
			
			Assert.IsNotNull(response.shop_items);
			Assert.AreEqual(response.shop_items.Count, 1);
			Assert.AreEqual(response.shop_items[0].ikey, "token");
			Assert.AreEqual(response.shop_items[0].label, "Token");
			Assert.AreEqual(response.shop_items[0].description, "Chrome Token");
			Assert.AreEqual(response.shop_items[0].price, "23");
			Assert.AreEqual(response.shop_items[0].jwt, "????");
			Assert.AreEqual(response.shop_items[0].modifiers.Count, 1);
			Assert.AreEqual((response.shop_items[0].modifiers[0] as Roar.DomainObjects.Modifiers.GrantItem).ikey, "item_ikey_1");
			Assert.AreEqual(response.shop_items[0].tags.Count, 2);
			Assert.AreEqual(response.shop_items[0].tags[0], "a_tag");
			Assert.AreEqual(response.shop_items[0].tags[1], "another_tag");
		}
		
		[Test()]
		public void TestChromeWebStoreListParseMechanics ()
		{
			string xml =
			@"<roar tick=""130695522924"">
				<chrome_web_store>
					<list>
						<shopitem blah=""???"" ikey=""token"" label=""Token"" description=""Chrome Token"" price=""23"" jwt=""????"">
							<modifiers>
								<grant_item ikey=""item_ikey_1""/>
							</modifiers>
							<tags>
								<tag value=""a_tag""/>
								<tag value=""another_tag""/>
							</tags>
						</shopitem>
					</list>
				</chrome_web_store>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.ChromeWebStore.List list_parser = new Roar.DataConversion.Responses.ChromeWebStore.List();
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			list_parser.ixcrm_parser = ixcrm_parser;
			IList<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			IList<string> tag_list = new List<string>();
			
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.SelectSingleNode("./chrome_web_store/list/shopitem/modifiers")).Will(Return.Value(modifier_list));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.SelectSingleNode("./chrome_web_store/list/shopitem/tags")).Will(Return.Value(tag_list));
			
			ListResponse response = list_parser.Build(nn);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.IsNotNull(response.shop_items);
			Assert.AreEqual(response.shop_items.Count, 1);
			Assert.AreEqual(response.shop_items[0].ikey, "token");
			Assert.AreEqual(response.shop_items[0].label, "Token");
			Assert.AreEqual(response.shop_items[0].description, "Chrome Token");
			Assert.AreEqual(response.shop_items[0].price, "23");
			Assert.AreEqual(response.shop_items[0].jwt, "????");
			Assert.AreEqual(response.shop_items[0].modifiers, modifier_list);
			Assert.AreEqual(response.shop_items[0].tags, tag_list);
		}
	}
}

