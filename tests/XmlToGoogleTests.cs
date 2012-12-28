using System;
using NUnit.Framework;
using Roar.WebObjects.Google;

namespace Testing
{
	[TestFixture]
	public class XmlToGoogleTests
	{
		[Test()]
		public void TestGoogleBindUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<bind_user status=""ok""/>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.BindUser bind_user_parser = new Roar.DataConversion.Responses.Google.BindUser();
			BindUserResponse response = bind_user_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestGoogleBindUserTokenXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<bind_user status=""ok""/>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.BindUserToken bind_user_token_parser = new Roar.DataConversion.Responses.Google.BindUserToken();
			BindUserTokenResponse response = bind_user_token_parser.Build(nn);
			Assert.IsNotNull(response);
		}
		
		[Test()]
		public void TestGoogleCreateUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<create_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
					</create_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.CreateUser create_user_parser = new Roar.DataConversion.Responses.Google.CreateUser();
			CreateUserResponse response = create_user_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
		}
		
		[Test()]
		public void TestGoogleCreateUserTokenXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<create_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
					</create_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.CreateUserToken create_user_token_parser = new Roar.DataConversion.Responses.Google.CreateUserToken();
			CreateUserTokenResponse response = create_user_token_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
		}
		
		[Test()]
		public void TestGoogleFriendsXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<friends status=""ok"">
						<!-- ""Mashton"" is already playing this game and has an 'id' we can use -->
						<friend gplus_name=""Mashton Groober"" gplus_id=""51151277315"" name=""Mashton"" id=""7877788777""/>
						<!-- These other two friends are not playing this game (no 'id' or 'name') -->
						<friend gplus_name=""Jumpy Maxton"" gplus_id=""529465555""/>
						<friend gplus_name=""Ami Jones"" gplus_id=""523055555""/>
					</friends>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.Friends friends_parser = new Roar.DataConversion.Responses.Google.Friends();
			FriendsResponse response = friends_parser.Build(nn);
			
			Assert.AreEqual(response.friends.Count, 3);
			Assert.AreEqual(response.friends[0].id, "7877788777");
			Assert.AreEqual(response.friends[0].name, "Mashton");
			Assert.AreEqual(response.friends[0].gplus_id, "51151277315");
			Assert.AreEqual(response.friends[0].gplus_name, "Mashton Groober");
			Assert.IsNull(response.friends[1].id);
			Assert.IsNull(response.friends[1].name);
			Assert.AreEqual(response.friends[1].gplus_id, "529465555");
			Assert.AreEqual(response.friends[1].gplus_name, "Jumpy Maxton");
			Assert.IsNull(response.friends[2].id);
			Assert.IsNull(response.friends[2].name);
			Assert.AreEqual(response.friends[2].gplus_id, "523055555");
			Assert.AreEqual(response.friends[2].gplus_name, "Ami Jones");
		}
		
		[Test()]
		public void TestGoogleLoginOrCreateUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<login_or_create_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
						<mode>create</mode>
					</login_or_create_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.LoginOrCreateUser login_or_create_user_parser = new Roar.DataConversion.Responses.Google.LoginOrCreateUser();
			LoginOrCreateUserResponse response = login_or_create_user_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
			Assert.AreEqual(response.mode, "create");
		}
		
		[Test()]
		public void TestGoogleLoginUserXmlGetAttributes ()
		{
			string xml =
			@"<roar tick=""128455369786"">
				<google>
					<login_user status=""ok"">
						<auth_token>ABCDEF</auth_token>
						<player_id>1231231</player_id>
					</login_user>
				</google>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Roar.DataConversion.Responses.Google.LoginUser login_user_parser = new Roar.DataConversion.Responses.Google.LoginUser();
			LoginUserResponse response = login_user_parser.Build(nn);
			
			Assert.AreEqual(response.auth_token, "ABCDEF");
			Assert.AreEqual(response.player_id, "1231231");
		}
	}
}

