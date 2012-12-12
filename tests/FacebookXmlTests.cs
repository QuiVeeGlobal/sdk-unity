using System;
using NUnit.Framework;

namespace Testing
{
	[TestFixture()]
	public class FacebookXmlTests
	{
		[Test()]
		public void TestParseFacebookBindOauthResponse()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<facebook>
					<bind_oauth status=""ok""/>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.WebObjects.Facebook.BindOauthResponse response = (new Roar.DataConversion.Responses.Facebook.BindOauth()).Build(nn);
			
			Assert.IsNotNull( response );
			
		}
		
		[Test()]
		public void TestParseFacebookBindSignedResponse()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<facebook>
					<bind_signed status=""ok""/>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			
			Roar.WebObjects.Facebook.BindSignedResponse response = (new Roar.DataConversion.Responses.Facebook.BindSigned()).Build(nn);
			
			Assert.IsNotNull( response );
			
		}
		
		[Test()]
		public void TestParseFacebookCreateOautResponse()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<facebook>
					<create_oauth>
						<player_id>ABCDE</player_id>
						<auth_token>PQRS</auth_token>
					</create_oauth>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.CreateOauthResponse response = (new Roar.DataConversion.Responses.Facebook.CreateOauth()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( "ABCDE", response.player_id );
			Assert.AreEqual( "PQRS", response.auth_token );
			
			
		}
		
		[Test()]
		public void TestParseFacebookCreateSignedResponse()
		{
			string xml =
			@"<roar tick=""135510457230"">
				<facebook>
					<create_signed>
						<player_id>ABCDE</player_id>
						<auth_token>PQRS</auth_token>
					</create_signed>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.CreateSignedResponse response = (new Roar.DataConversion.Responses.Facebook.CreateSigned()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( "ABCDE", response.player_id );
			Assert.AreEqual( "PQRS", response.auth_token );
			
			
		}
		
		[Test()]
		public void TestParseFacebookFetchOauthTokenResponse()
		{
			string xml =
			@"<roar tick=""127455369786"">
				<facebook>
					<fetch_oauth_token status=""ok"">
						<oauth_token>104271466587092|1.mYu275YylcGHf6vC ... hxrk63ouytUiBdBc</oauth_token>
					</fetch_oauth_token>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.FetchOauthTokenResponse response = (new Roar.DataConversion.Responses.Facebook.FetchOauthToken()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( "104271466587092|1.mYu275YylcGHf6vC ... hxrk63ouytUiBdBc", response.oauth_token );
		}
		
		[Test()]
		public void TestParseFacebookLoginOauthResponse()
		{
			string xml =
			@"<roar tick=""127055503865""> 
				<facebook> 
					<login_oauth status=""ok""> 
						<auth_token>2144869762</auth_token>
						<player_id>1231231</player_id>
					</login_oauth> 
				</facebook> 
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.LoginOauthResponse response = (new Roar.DataConversion.Responses.Facebook.LoginOauth()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( "2144869762", response.auth_token );
			Assert.AreEqual( "1231231", response.player_id );
		}
		
		[Test()]
		public void TestParseFacebookLoginSignedResponse()
		{
			string xml =
			@"<roar tick=""127055503865""> 
				<facebook> 
					<login_signed status=""ok""> 
						<auth_token>2144869762</auth_token>
						<player_id>1231231</player_id>
					</login_signed> 
				</facebook> 
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.LoginSignedResponse response = (new Roar.DataConversion.Responses.Facebook.LoginSigned()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( "2144869762", response.auth_token );
			Assert.AreEqual( "1231231", response.player_id );
		}
		
		[Test()]
		public void TestParseFacebookFriendsResponse()
		{
			string xml =
			@"<roar tick=""128888053531"">
				<facebook>
					<friends status=""ok"">
						<!-- ""Mashton"" is already playing this game and has an 'id' we can use -->
						<friend fb_name=""Mashton Groober"" fb_id=""51151277315"" name=""Mashton"" id=""7877788777""/>
						<!-- These other two friends are not playing this game (no 'id') -->
						<friend fb_name=""Jumpy Maxton"" fb_id=""529465555""/>
						<friend fb_name=""Ami Jones"" fb_id=""523055555""/>
					</friends>
				</facebook>
			</roar>";
			
			IXMLNode nn = ( new XMLNode.XMLParser() ).Parse(xml);
			Assert.IsNotNull( nn );

			Roar.WebObjects.Facebook.FriendsResponse response = (new Roar.DataConversion.Responses.Facebook.Friends()).Build(nn);
			
			Assert.IsNotNull( response );
			Assert.AreEqual( 3, response.facebook_friends.Count );
			Assert.AreEqual( "Mashton Groober", response.facebook_friends[0].fb_name );
			Assert.AreEqual( "51151277315",     response.facebook_friends[0].fb_id );
			Assert.AreEqual( "Mashton",         response.facebook_friends[0].name );
			Assert.AreEqual( "7877788777",      response.facebook_friends[0].id );
			
			Assert.AreEqual( "Jumpy Maxton", response.facebook_friends[1].fb_name );
			Assert.AreEqual( "529465555",     response.facebook_friends[1].fb_id );
			Assert.IsNull( response.facebook_friends[1].name );
			Assert.IsNull( response.facebook_friends[1].id );
			
			Assert.AreEqual( "Ami Jones", response.facebook_friends[2].fb_name );
			Assert.AreEqual( "523055555",     response.facebook_friends[2].fb_id );
			Assert.IsNull( response.facebook_friends[2].name );
			Assert.IsNull( response.facebook_friends[2].id );
			
		}
		
	}
}

