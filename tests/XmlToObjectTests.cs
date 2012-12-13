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
		private IXMLNode ixmlnode;
		private List< KeyValuePair<string,string> > attributes;
		private List< IXMLNode > children;

		[SetUp]
		public void TestInitialise()
		{
			this.mockery = new Mockery();
			converter = new Roar.DataConversion.XmlToShopEntry();
			ixmlnode = mockery.NewMock<IXMLNode>();
			attributes = new List< KeyValuePair<string,string> >();
			children = new List<IXMLNode>();

			Expect.AtLeast(0).On(ixmlnode).GetProperty("Attributes").Will( Return.Value(attributes) );
			Expect.AtLeast(0).On (ixmlnode).GetProperty("Children").Will ( Return.Value(children) );
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
			attributes.Add( new KeyValuePair<string, string>("ikey","shop_item_ikey_1") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(ixmlnode);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreEqual("shop_item_ikey_1", shopEntry.ikey );
			Assert.AreEqual("Shop item 1", shopEntry.label );
			Assert.AreEqual("Lorem Ipsum", shopEntry.description );
		}
		
		[Test()]
		public void TestThrowsOnUnexpectedAttribute ()
		{
			attributes.Add( new KeyValuePair<string, string>("unexpected","foo") );
			
			try
			{
				converter.Build(ixmlnode);
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
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			try
			{
				converter.Build(ixmlnode);
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
			attributes.Add( new KeyValuePair<string, string>("ikey","an_ikey") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			
			Roar.DomainObjects.ShopEntry shopentry = converter.Build(ixmlnode);
			
			Assert.AreEqual("", shopentry.label );
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestDeafultOnMissingDescription()
		{
			attributes.Add( new KeyValuePair<string, string>("ikey","an_ikey") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			
			Roar.DomainObjects.ShopEntry shopentry = converter.Build(ixmlnode);
			
			Assert.AreEqual("", shopentry.description );
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test()]
		public void TestGetsCosts()
		{
			attributes.Add( new KeyValuePair<string, string>("ikey","shop_item_ikey_1") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			children.Add( mockery.NewMock<IXMLNode>("costs") );
			Expect.AtLeast(1).On( children[0] ).GetProperty("Name").Will( Return.Value("costs") );
			List<Roar.DomainObjects.Cost> costs = new List<Roar.DomainObjects.Cost>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseCostList").With( children[0] ).Will( Return.Value( costs ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(ixmlnode);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(costs,shopEntry.costs);
		}
		
		[Test()]
		public void TestGetsModifiers()
		{
			attributes.Add( new KeyValuePair<string, string>("ikey","shop_item_ikey_1") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			children.Add( mockery.NewMock<IXMLNode>("modifiers") );
			Expect.AtLeast(1).On( children[0] ).GetProperty("Name").Will( Return.Value("modifiers") );
			List<Roar.DomainObjects.Modifier> mods = new List<Roar.DomainObjects.Modifier>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseModifierList").With( children[0] ).Will( Return.Value( mods ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(ixmlnode);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(mods,shopEntry.modifiers);
		}
		
		[Test()]
		public void TestGetsRequirements()
		{
			attributes.Add( new KeyValuePair<string, string>("ikey","shop_item_ikey_1") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			children.Add( mockery.NewMock<IXMLNode>("requirements") );
			Expect.AtLeast(1).On( children[0] ).GetProperty("Name").Will( Return.Value("requirements") );
			List<Roar.DomainObjects.Requirement> reqs = new List<Roar.DomainObjects.Requirement>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseRequirementList").With( children[0] ).Will( Return.Value( reqs ) );
			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(ixmlnode);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreSame(reqs,shopEntry.requirements);
		}
		
		[Test()]
		public void TestGetsTags()
		{
			attributes.Add( new KeyValuePair<string, string>("ikey","shop_item_ikey_1") );
			attributes.Add( new KeyValuePair<string, string>("label","Shop item 1") );
			attributes.Add( new KeyValuePair<string, string>("description","Lorem Ipsum") );
			
			converter.CrmParser_ = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			children.Add( mockery.NewMock<IXMLNode>("tags") );
			Expect.AtLeast(1).On( children[0] ).GetProperty("Name").Will( Return.Value("tags") );
			List<string> tags = new List<string>();
			Expect.Exactly(1).On ( converter.CrmParser_ ).Method("ParseTagList").With( children[0] ).Will( Return.Value( tags ) );

			Roar.DomainObjects.ShopEntry shopEntry = converter.Build(ixmlnode);
			
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

			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse( xml ).GetFirstChild("friend");

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

			IXMLNode nn = ( new SystemXMLNodeFactory() ).Create( xml ).GetFirstChild("friend");
			
			Roar.DomainObjects.Friend friend = Roar.DomainObjects.Friend.CreateFromXml(nn);

			Assert.AreEqual( "ABCDEF", friend.player_id );
			Assert.AreEqual( "some dude", friend.name );
			Assert.AreEqual( 7, friend.level );
		}
	}

}

