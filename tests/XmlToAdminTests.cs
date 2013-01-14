using System;
using System.Collections.Generic;
using NUnit.Framework;
using NMock2;
using Roar.WebObjects.Admin;

namespace Testing
{
	[TestFixture]
	public class XmlToAdminTests
	{
		[Test()]
		public void TestAdminCreateUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<admin>
					<create_user status=""ok"">
						<!-- Used to identify this session in subsequent calls -->
						<auth_token>2034623793</auth_token>
						<player_id>12312312312</player_id>
					</create_user>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.CreatePlayer create_player_parser = new Roar.DataConversion.Responses.Admin.CreatePlayer();
			CreatePlayerResponse response = create_player_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "2034623793");
			Assert.AreEqual(response.player_id, "12312312312");
		}
		
		[Test()]
		public void TestAdminDeletePlayerXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<admin>
					<delete_player status=""ok"">
					</delete_player>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.DeletePlayer delete_player_parser = new Roar.DataConversion.Responses.Admin.DeletePlayer();
			DeletePlayerResponse response = delete_player_parser.Build(nn);
			
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestAdminIncrementStatXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<admin>
					<delete_player status=""ok"">
					</delete_player>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.IncrementStat increment_stat_parser = new Roar.DataConversion.Responses.Admin.IncrementStat();
			IncrementStatResponse response = increment_stat_parser.Build(nn);
			
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestLoginUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455461333"">
				<admin>
					<login_user status=""ok"">
						<!-- Used to identify this session in subsequent calls -->
						<auth_token>2034623793</auth_token>
						<player_id>12312312312</player_id>
					</login_user>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.LoginUser login_user_parser = new Roar.DataConversion.Responses.Admin.LoginUser();
			LoginUserResponse response = login_user_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "2034623793");
			Assert.AreEqual(response.player_id, "12312312312");
		}
		
		[Test()]
		public void TestSetXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""125555206993"">
				<admin>
					<set status=""ok""/>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.Set set_parser = new Roar.DataConversion.Responses.Admin.Set();
			SetResponse response = set_parser.Build(nn);
			
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestSetCustomXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""125555206993"">
				<admin>
					<set_custom status=""ok""/>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.SetCustom set_custom_parser = new Roar.DataConversion.Responses.Admin.SetCustom();
			SetCustomResponse response = set_custom_parser.Build(nn);
			
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestViewPlayerXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""125555206993"">
				<admin>
					<view_player status=""ok"">
						<attribute ikey=""id"" value=""2059428086"" type=""special""/>
						<attribute ikey=""xp"" value=""0"" type=""special""/>
						<attribute ikey=""level"" value=""1"" type=""special""/>
						<attribute ikey=""facebook_uid"" value=""0"" type=""special""/>
						<attribute ikey=""name"" value=""foo"" type=""special""/>
						<attribute ikey=""attack"" value=""10"" type=""core"" label=""Attack""/>
						<attribute ikey=""defence"" value=""10"" type=""core"" label=""Core Defence""/>
						<attribute ikey=""hit"" value=""10"" type=""core"" label=""Hit Power""/>
						<attribute ikey=""avoid"" value=""10"" type=""core"" label=""avoid""/>
						<attribute ikey=""health"" value=""100"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Health""/>
						<attribute ikey=""energy"" value=""20"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Energy""/>
						<attribute ikey=""stamina"" value=""5"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Stamina""/>
						<attribute ikey=""profile_points"" value=""0"" type=""currency"" label=""Monkey Power Points""/>
						<attribute ikey=""cash"" value=""100"" type=""currency"" lable=""cash""/>
						<attribute ikey=""premium_currency"" value=""5"" type=""currency"" label=""Bear Dollars""/>
						<items>
							<item id=""1001"" ikey=""item_ikey"" count=""1"" label=""A Label"" type=""thing"" description=""A thing"" consumable=""false"" sellable=""true"" equipped=""false"">
								<stats>
									<equip_attribute ikey=""health_max"" value=""100""/>
									<grant_stat ikey=""cash"" value=""100""/>
									<grant_stat ikey=""energy"" value=""-5""/>
								</stats>
								<properties>
									<property ikey=""size"" value=""3""/>
								</properties>
								<tags>
									<tag value=""weapon""/>
								</tags>
							</item>
						</items>
					</view_player>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			Roar.DataConversion.Responses.Admin.ViewPlayer view_player_parser = new Roar.DataConversion.Responses.Admin.ViewPlayer();
			ViewPlayerResponse response = view_player_parser.Build(nn);
			
			Assert.AreEqual(response.player.id, "2059428086");
			Assert.AreEqual(response.player.name, "foo");
			Assert.AreEqual(response.player.xp.value, 0);
			Assert.AreEqual(response.player.level, 1);
			Assert.AreEqual(response.player.attributes.Count, 11);
			Assert.AreEqual(response.player.attributes["facebook_uid"].value, "0");
			Assert.AreEqual(response.player.attributes["facebook_uid"].type, "special");
			Assert.AreEqual(response.player.attributes["hit"].label, "Hit Power");
			Assert.AreEqual(response.items.Count, 1);
			Assert.AreEqual(response.items[0].stats.Count, 3);
			Assert.AreEqual(response.items[0].stats[0].ikey, "health_max");
			Assert.AreEqual(response.items[0].stats[1].ikey, "cash");
			Assert.AreEqual(response.items[0].stats[2].ikey, "energy");
			Assert.AreEqual(response.items[0].properties.Count, 1);
			Assert.AreEqual(response.items[0].properties[0].ikey, "size");
			Assert.AreEqual(response.items[0].tags.Count, 1);
			Assert.AreEqual(response.items[0].tags[0], "weapon");
		}
		
		[Test()]
		public void TestViewPlayerParseMechanics ()
		{
			string xml =
			@"<roar tick=""125555206993"">
				<admin>
					<view_player status=""ok"">
						<attribute ikey=""id"" value=""2059428086"" type=""special""/>
						<attribute ikey=""xp"" value=""0"" type=""special""/>
						<attribute ikey=""level"" value=""1"" type=""special""/>
						<attribute ikey=""facebook_uid"" value=""0"" type=""special""/>
						<attribute ikey=""name"" value=""foo"" type=""special""/>
						<attribute ikey=""attack"" value=""10"" type=""core"" label=""Attack""/>
						<attribute ikey=""defence"" value=""10"" type=""core"" label=""Core Defence""/>
						<attribute ikey=""hit"" value=""10"" type=""core"" label=""Hit Power""/>
						<attribute ikey=""avoid"" value=""10"" type=""core"" label=""avoid""/>
						<attribute ikey=""health"" value=""100"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Health""/>
						<attribute ikey=""energy"" value=""20"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Energy""/>
						<attribute ikey=""stamina"" value=""5"" type=""resource"" max=""123"" min=""0"" regen_every=""1000"" label=""Stamina""/>
						<attribute ikey=""profile_points"" value=""0"" type=""currency"" label=""Monkey Power Points""/>
						<attribute ikey=""cash"" value=""100"" type=""currency"" lable=""cash""/>
						<attribute ikey=""premium_currency"" value=""5"" type=""currency"" label=""Bear Dollars""/>
						<items>
							<item id=""1001"" ikey=""item_ikey"" count=""1"" label=""A Label"" type=""thing"" description=""A thing"" consumable=""false"" sellable=""true"" equipped=""false"">
								<stats>
									<equip_attribute ikey=""health_max"" value=""100""/>
									<grant_stat ikey=""cash"" value=""100""/>
									<grant_stat ikey=""energy"" value=""-5""/>
								</stats>
								<properties>
									<property ikey=""size"" value=""3""/>
								</properties>
								<tags>
									<tag value=""weapon""/>
								</tags>
							</item>
						</items>
					</view_player>
				</admin>
			</roar>";
			
			System.Xml.XmlElement nn = RoarExtensions.CreateXmlElement(xml);
			
			Mockery mockery = new Mockery();
			Roar.DataConversion.IXCRMParser ixcrm_parser = mockery.NewMock<Roar.DataConversion.IXCRMParser>();
			
			Roar.DataConversion.Responses.Admin.ViewPlayer view_player_parser = new Roar.DataConversion.Responses.Admin.ViewPlayer();
			view_player_parser.ixcrm_parser = ixcrm_parser;
			
			List<Roar.DomainObjects.ItemStat> stat_list = new List<Roar.DomainObjects.ItemStat>();
			List<Roar.DomainObjects.Modifier> modifier_list = new List<Roar.DomainObjects.Modifier>();
			List<string> tag_list = new List<string>();
			Expect.Once.On(ixcrm_parser).Method("ParseItemStatList").With(nn.SelectSingleNode("./admin/view_player/items/item/stats")).Will(Return.Value(stat_list));
			Expect.Once.On(ixcrm_parser).Method("ParseModifierList").With(nn.SelectSingleNode("./amdin/view_player/items/item/price")).Will(Return.Value(modifier_list));
			Expect.Once.On(ixcrm_parser).Method("ParseTagList").With(nn.SelectSingleNode("./admin/view_player/items/item/tags")).Will(Return.Value(tag_list));
			
			ViewPlayerResponse response = view_player_parser.Build(nn);
			mockery.VerifyAllExpectationsHaveBeenMet();
			
			Assert.AreEqual(response.player.id, "2059428086");
			Assert.AreEqual(response.player.name, "foo");
			Assert.AreEqual(response.player.xp.value, 0);
			Assert.AreEqual(response.player.level, 1);
			Assert.AreEqual(response.player.attributes.Count, 11);
			Assert.AreEqual(response.player.attributes["facebook_uid"].value, "0");
			Assert.AreEqual(response.player.attributes["facebook_uid"].type, "special");
			Assert.AreEqual(response.player.attributes["hit"].label, "Hit Power");
			Assert.AreEqual(response.items.Count, 1);
			Assert.AreEqual(response.items[0].stats, stat_list);
			Assert.AreEqual(response.items[0].price, modifier_list);
			Assert.AreEqual(response.items[0].tags, tag_list);
		}
	}
}

