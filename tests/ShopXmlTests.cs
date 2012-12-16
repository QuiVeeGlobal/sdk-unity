using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;


namespace Testing
{
	[TestFixture()]
	public class ShopXmlTests
	{
		[Test()]
		public void TestListShopXmlParsesCorrectly()
		{
			string xml =
			@"<roar tick=""135562028150"">
				<shop>
					<list status=""ok"">
						<shopitem ikey=""rocket_fuel"" label=""Rocket Fuel"" description="""">
							<costs>
								<stat_cost type=""currency"" ikey=""gamecoins"" value=""10"" ok=""true""/>
							</costs>
							<modifiers>
								<grant_stat type=""currency"" ikey=""rocket_fuel"" value=""100""/>
							</modifiers>
							<tags/>
						</shopitem>
						<shopitem ikey=""neil_armstrong"" label=""Neil Armstrong"" description=""Best copilot in the world"">
							<costs>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""15"" ok=""false"" reason=""Insufficient Premium Currency""/>
							</costs>
							<modifiers>
								<grant_item ikey=""npc_armstrong""/>
							</modifiers>
							<tags>
								<tag value=""copilot""/>
							</tags>
						</shopitem>
						<shopitem ikey=""starter_space_pack"" label=""Starter Space Pack"" description=""Get going!"">
							<costs>
								<stat_cost type=""currency"" ikey=""gamecoins"" value=""20"" ok=""true""/>
							</costs>
							<modifiers>
								<grant_stat type=""currency"" ikey=""rocket_fuel"" value=""30""/>
								<grant_item ikey=""regular_space_helmet""/>
								<grant_item ikey=""rocket_ship""/>
							</modifiers>
							<tags>
								<tag value=""pack""/>
							</tags>
						</shopitem>
					</list>
				</shop>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );
			
			IXMLNode c1 = nn.GetNode("roar>0>shop>0>list>0>shopitem>0>costs>0");
			IXMLNode c2 = nn.GetNode("roar>0>shop>0>list>0>shopitem>1>costs>0");
			IXMLNode c3 = nn.GetNode("roar>0>shop>0>list>0>shopitem>2>costs>0");
			Assert.IsNotNull (c1);
			Assert.IsNotNull (c2);
			Assert.IsNotNull (c3);
			IList<Roar.DomainObjects.Cost> cl1 = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Cost> cl2 = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Cost> cl3 = new List<Roar.DomainObjects.Cost>();
			
			IXMLNode m1 = nn.GetNode("roar>0>shop>0>list>0>shopitem>0>modifiers>0");
			IXMLNode m2 = nn.GetNode("roar>0>shop>0>list>0>shopitem>1>modifiers>0");
			IXMLNode m3 = nn.GetNode("roar>0>shop>0>list>0>shopitem>2>modifiers>0");
			Assert.IsNotNull (m1);
			Assert.IsNotNull (m2);
			Assert.IsNotNull (m3);
			IList<Roar.DomainObjects.Modifier> ml1 = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.Modifier> ml2 = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.Modifier> ml3 = new List<Roar.DomainObjects.Modifier>();

			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( c1 ).Will( Return.Value( cl1 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( c2 ).Will( Return.Value( cl2 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( c3 ).Will( Return.Value( cl3 ) );
			
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( m1 ).Will( Return.Value( ml1 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( m2 ).Will( Return.Value( ml2 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( m3 ).Will( Return.Value( ml3 ) );


			
			Roar.DataConversion.Responses.Shop.List shoplist_response_parser = new Roar.DataConversion.Responses.Shop.List();
			shoplist_response_parser.ixcrm_parser = ixcrm_parser;
			
			Roar.WebObjects.Shop.ListResponse response = shoplist_response_parser.Build(nn);
			
			Assert.IsNotNull(response);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
		
		class SystemXmlMatcher : NMock2.Matcher
		{
			public SystemXMLNode node;
			public SystemXmlMatcher( SystemXMLNode n )
			{
				node = n;
			}
			
			// This is not a perfect test, but it will do for now.
			// A better test would dig into the contained System.XmlNode...
			public override bool Matches(object o)
			{
				SystemXMLNode nn = o as SystemXMLNode;
				if( nn == null ) return false;
				return ( nn.DebugAsString() == node.DebugAsString() );
			}
			
			public override void DescribeTo( System.IO.TextWriter writer )
			{
				writer.Write("equal to "+node.DebugAsString() );
			}
		};
		

		[Test()]
		public void TestListShopXmlParsesCorrectly_System()
		{
			string xml =
			@"<roar tick=""135562028150"">
				<shop>
					<list status=""ok"">
						<shopitem ikey=""rocket_fuel"" label=""Rocket Fuel"" description="""">
							<costs>
								<stat_cost type=""currency"" ikey=""gamecoins"" value=""10"" ok=""true""/>
							</costs>
							<modifiers>
								<grant_stat type=""currency"" ikey=""rocket_fuel"" value=""100""/>
							</modifiers>
							<tags/>
						</shopitem>
						<shopitem ikey=""neil_armstrong"" label=""Neil Armstrong"" description=""Best copilot in the world"">
							<costs>
								<stat_cost type=""currency"" ikey=""premium_currency"" value=""15"" ok=""false"" reason=""Insufficient Premium Currency""/>
							</costs>
							<modifiers>
								<grant_item ikey=""npc_armstrong""/>
							</modifiers>
							<tags>
								<tag value=""copilot""/>
							</tags>
						</shopitem>
						<shopitem ikey=""starter_space_pack"" label=""Starter Space Pack"" description=""Get going!"">
							<costs>
								<stat_cost type=""currency"" ikey=""gamecoins"" value=""20"" ok=""true""/>
							</costs>
							<modifiers>
								<grant_stat type=""currency"" ikey=""rocket_fuel"" value=""30""/>
								<grant_item ikey=""regular_space_helmet""/>
								<grant_item ikey=""rocket_ship""/>
							</modifiers>
							<tags>
								<tag value=""pack""/>
							</tags>
						</shopitem>
					</list>
				</shop>
			</roar>";
			
			IXMLNode nn = ( new SystemXMLNodeFactory() ).Create(xml);
			Assert.IsNotNull( nn );
			
			IXMLNode c1 = nn.GetNode("roar>0>shop>0>list>0>shopitem>0>costs>0");
			IXMLNode c2 = nn.GetNode("roar>0>shop>0>list>0>shopitem>1>costs>0");
			IXMLNode c3 = nn.GetNode("roar>0>shop>0>list>0>shopitem>2>costs>0");
			Assert.IsNotNull (c1);
			Assert.IsNotNull (c2);
			Assert.IsNotNull (c3);
			Assert.AreEqual("gamecoins",c1.GetFirstChild("stat_cost").GetAttribute("ikey"));
			Assert.AreEqual("premium_currency",c2.GetFirstChild("stat_cost").GetAttribute("ikey"));
			Assert.AreEqual("gamecoins",c3.GetFirstChild("stat_cost").GetAttribute("ikey"));

			
			IList<Roar.DomainObjects.Cost> cl1 = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Cost> cl2 = new List<Roar.DomainObjects.Cost>();
			IList<Roar.DomainObjects.Cost> cl3 = new List<Roar.DomainObjects.Cost>();
			
			IXMLNode m1 = nn.GetNode("roar>0>shop>0>list>0>shopitem>0>modifiers>0");
			IXMLNode m2 = nn.GetNode("roar>0>shop>0>list>0>shopitem>1>modifiers>0");
			IXMLNode m3 = nn.GetNode("roar>0>shop>0>list>0>shopitem>2>modifiers>0");
			Assert.IsNotNull (m1);
			Assert.IsNotNull (m2);
			Assert.IsNotNull (m3);
			IList<Roar.DomainObjects.Modifier> ml1 = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.Modifier> ml2 = new List<Roar.DomainObjects.Modifier>();
			IList<Roar.DomainObjects.Modifier> ml3 = new List<Roar.DomainObjects.Modifier>();

			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( new SystemXmlMatcher(c1 as SystemXMLNode) ).Will( Return.Value( cl1 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( new SystemXmlMatcher(c2 as SystemXMLNode) ).Will( Return.Value( cl2 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseCostList").With( new SystemXmlMatcher(c3 as SystemXMLNode) ).Will( Return.Value( cl3 ) );
			
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( new SystemXmlMatcher(m1 as SystemXMLNode) ).Will( Return.Value( ml1 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( new SystemXmlMatcher(m2 as SystemXMLNode) ).Will( Return.Value( ml2 ) );
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With( new SystemXmlMatcher(m3 as SystemXMLNode) ).Will( Return.Value( ml3 ) );


			
			Roar.DataConversion.Responses.Shop.List shoplist_response_parser = new Roar.DataConversion.Responses.Shop.List();
			shoplist_response_parser.ixcrm_parser = ixcrm_parser;
			
			Roar.WebObjects.Shop.ListResponse response = shoplist_response_parser.Build(nn);
			
			Assert.IsNotNull(response);
			
			mockery.VerifyAllExpectationsHaveBeenMet();
		}
	}
}

