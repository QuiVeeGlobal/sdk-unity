using System;
using NUnit.Framework;
using NMock2;
using System.Collections;
using System.Collections.Generic;


namespace Testing
{
	[TestFixture()]
	public class XmlToObjectTests
	{
	
		private Mockery mockery = null;
		private Roar.DataConversion.XmlToShopEntry converter;

		[SetUp]
		public void TestInitialise()
		{
			this.mockery = new Mockery();
			converter = new Roar.DataConversion.XmlToShopEntry();
		}
	
	
		/* This is what the XML should look like.
		 *  <shopitem ikey="shop_item_ikey_1" label="Shop item 1" description="Lorem Ipsum">
		 *    <costs>
		 *      <stat_cost type="currency" ikey="cash" value="100" ok="false" reason="Insufficient Coins"/>
		 *      <stat_cost type="currency" ikey="premium_currency" value="0" ok="true"/>
		 *    </costs>
		 *    <modifiers>
		 *      <grant_item ikey="item_ikey_1"/>
		 *    </modifiers>
		 *    <requirements>
		 *       <.../>
		 *    </requirements>
		 *    <tags>
		 *      <tag value="a_tag"/>
		 *      <tag value="another_tag"/>
		      </tags>
		 *  </shopitem>
		 */

		[Test()]
		public void TestGetsShopAttributes ()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			
			
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(shopElement);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreEqual("shop_item_ikey_1", shopEntry.ikey );
			Assert.AreEqual("Shop item 1", shopEntry.label );
			Assert.AreEqual("Lorem Ipsum", shopEntry.description );
		}
		
		[Test()]
		public void TestThrowsOnUnexpectedAttribute ()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("unexpected", "foo");
			
			try
			{
				converter.Build(shopElement);
				Assert.Fail("Should have thrown");
			}
			catch( Roar.DataConversion.UnexpectedXMLElementException ue )
			{
				Assert.AreEqual("unexpected attribute, \"unexpected\", on ShopEntry", ue.Message );
			}
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestErrorsOnMissingIkey()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");

			try
			{
				converter.Build(shopElement);
				Assert.Fail("Should have thrown");
			}
			catch( Roar.DataConversion.MissingXMLElementException ue )
			{
				Assert.AreEqual("missing attribute, \"ikey\", on ShopEntry", ue.Message );
			}
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestErrorsOnMissingLabel()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			
			Roar.DomainObjects.ShopEntry shopentry = converter.Build(shopElement);
			
			Assert.AreEqual("", shopentry.label );
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestDeafultOnMissingDescription()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
	
			Roar.DomainObjects.ShopEntry shopentry = converter.Build(shopElement);
			
			Assert.AreEqual("", shopentry.description );
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestGetsCosts()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			System.Xml.XmlElement costsElement = shopElement.OwnerDocument.CreateElement("costs");
			shopElement.AppendChild( costsElement );
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			List<Roar.DomainObjects.Cost> costs = new List<Roar.DomainObjects.Cost>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseCostList").With( costsElement ).Will( Return.Value( costs ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(shopElement);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(costs,shopEntry.costs);
		}
		
		[Test()]
		public void TestGetsModifiers()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			System.Xml.XmlElement modifiersElement = shopElement.OwnerDocument.CreateElement("modifiers");
			shopElement.AppendChild( modifiersElement );
			List<Roar.DomainObjects.Modifier> mods = new List<Roar.DomainObjects.Modifier>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseModifierList").With( modifiersElement ).Will( Return.Value( mods ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(shopElement);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(mods,shopEntry.modifiers);
		}
		
		[Test()]
		public void TestGetsRequirements()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			System.Xml.XmlElement requirementsElement = shopElement.OwnerDocument.CreateElement("requirements");
			shopElement.AppendChild( requirementsElement );
			List<Roar.DomainObjects.Requirement> reqs = new List<Roar.DomainObjects.Requirement>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseRequirementList").With( requirementsElement ).Will( Return.Value( reqs ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(shopElement);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(reqs,shopEntry.requirements);
		}
		
		[Test()]
		public void TestGetsTags()
		{
			System.Xml.XmlElement shopElement = RoarExtensions.CreateXmlElement("shop_entry","");
			shopElement.SetAttribute("ikey", "shop_item_ikey_1");
			shopElement.SetAttribute("label","Shop item 1");
			shopElement.SetAttribute("description","Lorem Ipsum");
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			System.Xml.XmlElement tagsElement = shopElement.OwnerDocument.CreateElement("tags");
			shopElement.AppendChild( tagsElement );
			List<string> tags = new List<string>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseTagList").With( tagsElement ).Will( Return.Value( tags ) );

			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(shopElement);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
			Assert.AreSame(tags,shopEntry.tags);
			
		}
		
	}


	[TestFixture()]
	public class XmlToFriendTests
	{
		[SetUp]
		public void TestInitialise()
		{
		}



		[Test()]
		public void TestXMLNodeGetsAttributes()
		{
			string xml ="<friend>\n"+
				"  <player_id>ABCDEF</player_id>\n" +
				"  <name>some dude</name>\n" +
				"  <level>7</level>" +
				"</friend>";

			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);

			Roar.DomainObjects.Friend friend = Roar.DomainObjects.Friend.CreateFromXml(nn);

			Assert.AreEqual( "ABCDEF", friend.player_id );
			Assert.AreEqual( "some dude", friend.name );
			Assert.AreEqual( 7, friend.level );
		}

		[Test()]
		public void TestSystemXMLNodeGetsAttributes()
		{
			string xml ="<friend>\n"+
				"  <player_id>ABCDEF</player_id>\n" +
				"  <name>some dude</name>\n" +
				"  <level>7</level>" +
				"</friend>";

			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			Roar.DomainObjects.Friend friend = Roar.DomainObjects.Friend.CreateFromXml(nn);

			Assert.AreEqual( "ABCDEF", friend.player_id );
			Assert.AreEqual( "some dude", friend.name );
			Assert.AreEqual( 7, friend.level );
		}
	}

}

