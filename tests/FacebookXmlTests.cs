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
		
		
	}
}

