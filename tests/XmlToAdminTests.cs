using System;
using NUnit.Framework;
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
	}
}

