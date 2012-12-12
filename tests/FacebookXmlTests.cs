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
		
	}
}

